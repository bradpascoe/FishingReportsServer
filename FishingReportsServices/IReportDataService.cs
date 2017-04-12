using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using FishingReports.Model;

namespace FishingReportsServices
{
    /// <summary>
    /// Service for providing report data
    /// </summary>
    [ServiceContract]
    public interface IReportDataService
    {
        [OperationContract]
        IEnumerable<ImageEntity> GetImages(int reportID);

        [OperationContract]
        IEnumerable<ImageEntity> GetImagesByLocation(Guid locationID);

        [OperationContract]
        void SynchronizeImagesForReport(int reportID, IEnumerable<ImageEntity> images);

        [OperationContract]
        void SaveNewImages(IEnumerable<ImageEntity> images);

        [OperationContract]
        void DeleteImages(IEnumerable<ImageEntity> images);

        /// <summary>
        /// Gets all reports for the specified date range.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [OperationContract]
        ReportSearchResult GetReports(ReportFilter filter);

        /// <summary>
        /// Gets a single report.
        /// </summary>
        /// <param name="reportID"></param>
        /// <returns></returns>
        [OperationContract]
        ReportEntity GetReport(int reportID);

        /// <summary>
        /// Updates the provided report.
        /// </summary>
        /// <param name="report"></param>
        [OperationContract]
        void UpdateReport(ReportEntity report);

        /// <summary>
        /// Gets all years that reports have been submitted under.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int[] GetAllYears();

        /// <summary>
        /// Saves a new report.
        /// </summary>
        /// <param name="report"></param>
        [OperationContract]
        int SaveReport(ReportEntity report);

        /// <summary>
        /// Validates a new report
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [OperationContract]
        IDictionary<string, string> ValidateNewReport(ReportEntity entity);
    }
}
