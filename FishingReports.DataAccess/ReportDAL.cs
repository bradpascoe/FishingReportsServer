using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FishingReports.Model;

namespace FishingReports.DataAccess
{
	/// <summary>
	/// Report data access logic provider
	/// </summary>
	public class ReportDAL : IReportDAL
	{
		#region Constructors

		internal ReportDAL()
			: this( new ConnectionStringProvider() )
		{
		}

		/// <summary>
		/// Creates a new report DAL object.
		/// </summary>
		/// <param name="provider"></param>
		internal ReportDAL( ConnectionStringProvider provider )
		{
			mProvider = provider;
		}

		#endregion Constructors

		#region ReportDAL Members

		public int CreateReport( ReportEntity report )
		{
			using( FishingReportsDBDataContext context = new FishingReportsDBDataContext(
			   mProvider.GetConnectionString() ) )
			{
				int? reportID = null;

				context.sp_InsertReport(
					(int)report.ReportType,
					report.AccessID,
					report.ReportDate,
					report.TotalFish,
					report.FlowRate.HasValue ?
						report.FlowRate.Value : -1,
					report.WaterConditions,
					report.WeatherConditions,
					report.ReportDescription,
					report.Username,
					ref reportID );

				return reportID.Value;
			}
		}

		public void DeleteImages( IEnumerable<ImageEntity> imageEntities )
		{
			using( FishingReportsDBDataContext context = new FishingReportsDBDataContext( mProvider.GetConnectionString() ) )
			{
				foreach( ImageEntity imageEntity in imageEntities )
				{
					var image = (from ImageTable in context.Images
								 where ImageTable.ImageID == imageEntity.ImageID
								 select ImageTable).Single();

					context.Images.DeleteOnSubmit( image );
				}

				context.SubmitChanges();
			}
		}

		public IEnumerable<ImageEntity> QueryImages( int reportID )
		{
			List<ImageEntity> imageEntities = new List<ImageEntity>();

			using( FishingReportsDBDataContext context = new FishingReportsDBDataContext( mProvider.GetConnectionString() ) )
			{
				var images = from Images in context.Images
							 where Images.ReportID == reportID
							 select Images;

				foreach( Image image in images )
				{
					imageEntities.Add( _CopyToImageEntity( image ) );
				}
			}

			return imageEntities;
		}

		public IEnumerable<ImageEntity> QueryImages( Guid locationID )
		{
			List<ImageEntity> imageEntities = new List<ImageEntity>();

			using( FishingReportsDBDataContext context = new FishingReportsDBDataContext( mProvider.GetConnectionString() ) )
			{
				var images = from Images in context.Images
							 join Reports in context.Reports on Images.ReportID equals Reports.ReportID
							 join Accesses in context.Accesses on Reports.AccessID equals Accesses.AccessID
							 where Accesses.LocationID == locationID
							 orderby Reports.Date descending
							 select Images;

				foreach( Image image in images )
				{
					imageEntities.Add( _CopyToImageEntity( image ) );
				}
			}

			return imageEntities;
		}

		public ReportEntity QueryReports( int reportID )
		{
			using( FishingReportsDBDataContext context = new FishingReportsDBDataContext(
			   mProvider.GetConnectionString() ) )
			{
				V_Report report = (from ReportRecs in context.V_Reports
								   where ReportRecs.ReportID == reportID
								   select ReportRecs).Single();

				return _CreateReportEntity( report );
			}
		}

		/// <summary>
		/// Gets all reports from the DB.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<ReportEntity> QueryReports( ReportFilter filter )
		{
			List<ReportEntity> reportEntities = new List<ReportEntity>();

			using( FishingReportsDBDataContext context = new FishingReportsDBDataContext(
				mProvider.GetConnectionString() ) )
			{
				var reports = from ReportRecs in context.V_Reports
							  select ReportRecs;

				if( filter.DateFrom.HasValue )
				{
					reports = from reportsTable in reports
							  where reportsTable.Date >= filter.DateFrom.Value
							  select reportsTable;
				}

				if( filter.DateTo.HasValue )
				{
					reports = from reportsTable in reports
							  where reportsTable.Date <= filter.DateTo.Value
							  select reportsTable;
				}

				if( filter.Year.HasValue )
				{
					reports = from reportsTable in reports
							  where reportsTable.Date.Year == filter.Year.Value
							  select reportsTable;
				}

				if( !string.IsNullOrEmpty( filter.Location ) )
				{
					reports = from reportsTable in reports
							  where reportsTable.Location == filter.Location
							  select reportsTable;
				}

				if( filter.MonthFrom.HasValue )
				{
					reports = from reportsTable in reports
							  where reportsTable.Date.Month >= filter.MonthFrom.Value
							  select reportsTable;
				}

				if( filter.MonthTo.HasValue )
				{
					reports = from reportsTable in reports
							  where reportsTable.Date.Month <= filter.MonthTo.Value
							  select reportsTable;
				}

				if( filter.Images.HasValue && filter.Images.Value )
				{
					reports = from reportsTable in reports
							  where reportsTable.NumberOfImages > 0
							  select reportsTable;
				}

				reports = from reportsTable in reports
						  orderby reportsTable.Date descending
						  select reportsTable;

				_CopyToList( reports, reportEntities );
			}

			return reportEntities;
		}

		public IEnumerable<int> QueryYears()
		{
			using( FishingReportsDBDataContext context = new FishingReportsDBDataContext(
				mProvider.GetConnectionString() ) )
			{
				var years = from Reports in context.Reports
							group Reports by Reports.Date.Year into yearGroup
							orderby yearGroup.Key ascending
							select new
							{
								Year = yearGroup
							};

				List<int> allYears = new List<int>();

				foreach( var year in years )
				{
					allYears.Add( year.Year.Key );
				}

				return allYears;
			}
		}

		public void SaveNewImages( IEnumerable<ImageEntity> images )
		{
			using( FishingReportsDBDataContext context = new FishingReportsDBDataContext( mProvider.GetConnectionString() ) )
			{
				foreach( ImageEntity imageEntity in images )
				{
					Image image = new Image();

					image.ReportID = imageEntity.ReportID.Value;
					image.ThumbPath = imageEntity.ThumbNailName;
					image.ImagePath = imageEntity.ImageName;

					context.Images.InsertOnSubmit( image );
				}

				context.SubmitChanges();
			}
		}

		public void SynchronizeImagesForReport( int reportID, IEnumerable<ImageEntity> currentImages )
		{
			IEnumerable<ImageEntity> newImages =
				currentImages.Where( image => !image.ReportID.HasValue );
			ImageEntity[] newImagesArray = newImages.ToArray();

			var existingImages = QueryImages( reportID );
			IList<ImageEntity> imagesToDelete = new List<ImageEntity>();

			foreach( ImageEntity existingImage in existingImages )
			{
				if( currentImages.Where( currentImage => currentImage.ImageID == existingImage.ImageID ).Count() == 0 )
				{
					imagesToDelete.Add( existingImage );
				}
			}

			foreach( ImageEntity entity in newImagesArray )
			{
				entity.ReportID = reportID;
			}

			SaveNewImages( newImagesArray );
			DeleteImages( imagesToDelete );
		}

		public void UpdateReport( ReportEntity report )
		{
			using( FishingReportsDBDataContext context = new FishingReportsDBDataContext(
			   mProvider.GetConnectionString() ) )
			{
				context.sp_UpdateReport(
					report.ReportID,
					(int)report.ReportType,
					report.ReportDate,
					report.TotalFish,
					report.FlowRate.HasValue ?
						report.FlowRate.Value : -1,
					report.WaterConditions,
					report.WeatherConditions,
					report.ReportDescription );
			}
		}

		public IDictionary<string, string> ValidateNewReport( ReportEntity report )
		{
			Dictionary<string, string> errors = new Dictionary<string, string>();

			if( report.ReportDate == new DateTime() )
			{
				errors.Add( "ReportDate", "Please select a Date." );
			}

			if( !report.AccessID.HasValue )
			{
				errors.Add( "Access", "Please select an access." );
			}

			if( !report.TotalFish.HasValue )
			{
				errors.Add( "TotalFish", "Please enter a fish count." );
			}

			return errors;
		}

		#endregion ReportDAL Members

		#region Fields

		private ConnectionStringProvider mProvider;

		#endregion Fields

		#region Private Members

		private ImageEntity _CopyToImageEntity( Image image )
		{
			return new ImageEntity
			{
				ImageID = image.ImageID,
				ImageName = image.ImagePath,
				ReportID = image.ReportID,
				ThumbNailName = image.ThumbPath
			};
		}

		private void _CopyToList( IEnumerable<V_Report> reportViews, List<ReportEntity> reportEntities )
		{
			foreach( V_Report report in reportViews )
			{
				reportEntities.Add( _CreateReportEntity( report ) );
			}
		}

		private ReportEntity _CreateReportEntity( V_Report report )
		{
			ReportEntity entity = new ReportEntity
			{
				Access = report.Access,
				AccessID = report.AccessID,
				FlowRate = report.Flow,
				Location = report.Location,
				ReportDate = report.Date,
				ReportDescription = report.Description,
				ReportID = report.ReportID,
				State = report.State,
				TotalFish = report.TotalFish,
				Username = report.UserName,
				WaterConditions = report.Water,
				WeatherConditions = report.Weather,
				NumberOfImages = report.NumberOfImages,
				ReportType = (ReportType)report.ReportType,
			};

			if( report.Flow < 0 )
			{
				entity.FlowRate = null;
			}

			return entity;
		}

		#endregion Private Members

	}
}