using System;
using System.Collections.Generic;
using System.Linq;

using FishingReports.DataAccess;
using FishingReports.Model;

namespace FishingReportsServices
{
	public class ReportDataService : IReportDataService
	{
		#region Constructors

		public ReportDataService()
		{
			mDataAccess = DALFactory.CreateReportDAL();
		}

		#endregion Constructors

		#region ReportDataService Members

		public void DeleteImages(IEnumerable<ImageEntity> images)
		{
			mDataAccess.DeleteImages(images);
		}

		public int[] GetAllYears()
		{
			return mDataAccess.QueryYears().ToArray();
		}

		public IEnumerable<ImageEntity> GetImages(int reportId)
		{
			return mDataAccess.QueryImages(reportId);
		}

		public IEnumerable<ImageEntity> GetImagesByLocation(Guid locationId)
		{
			return mDataAccess.QueryImages(locationId);
		}

		public ReportEntity GetReport(int reportId)
		{
			ReportEntity report = mDataAccess.QueryReports(reportId);

			return report;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public ReportSearchResult GetReports(ReportFilter filter)
		{
			var reports = mDataAccess.QueryReports(filter).ToArray();

			ReportSearchResult searchResult = new ReportSearchResult
			{
				NumberOfReports = reports.Count(),
				TotalFish = reports.Sum(report => report.TotalFish ?? 0),
				AverageFish = reports.Average(report => report.TotalFish ?? 0)
			};

			searchResult.Results = _TranslateToReportListItems(reports);

			return searchResult;
		}

		public void SaveNewImages(IEnumerable<ImageEntity> images)
		{
			mDataAccess.SaveNewImages(images);
		}

		/// <summary>
		/// Saves a new report.
		/// </summary>
		/// <param name="report"></param>
		public int SaveReport(ReportEntity report)
		{
			return mDataAccess.CreateReport(report);
		}

		public void SynchronizeImagesForReport(int reportId, IEnumerable<ImageEntity> images)
		{
			mDataAccess.SynchronizeImagesForReport(reportId, images);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="report"></param>
		/// <returns></returns>
		public void UpdateReport(ReportEntity report)
		{
			mDataAccess.UpdateReport(report);
		}

		/// <summary>
		/// Validates a new report
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public IDictionary<string, string> ValidateNewReport(ReportEntity entity)
		{
			return mDataAccess.ValidateNewReport(entity);
		}

		#endregion ReportDataService Members

		#region Fields

		private IReportDAL mDataAccess;

		#endregion Fields

		#region Private Members

		private ReportListItem _TranslateToReportListItem(ReportEntity entity)
		{
			return new ReportListItem
			{
				TotalFish = entity.TotalFish,
				State = entity.State,
				NumberOfImages = entity.NumberOfImages,
				Access = entity.Access,
				Location = entity.Location,
				AccessId = entity.AccessID,
				FlowRate = entity.FlowRate,
				ReportDate = entity.ReportDate,
				ReportDescription = entity.ReportDescription,
				ReportId = entity.ReportID,
				ReportType = entity.ReportType,
				Username = entity.Username,
				WaterConditions = entity.WaterConditions,
				WeatherConditions = entity.WeatherConditions
			};
		}

		private ReportListItem[] _TranslateToReportListItems(ReportEntity[] reports)
		{
			List<ReportListItem> listItems = new List<ReportListItem>();

			foreach (var entity in reports)
			{
				listItems.Add(_TranslateToReportListItem(entity));
			}

			return listItems.ToArray();
		}

		#endregion Private Members

	}
}
