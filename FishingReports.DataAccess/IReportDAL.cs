using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishingReports.Model;

namespace FishingReports.DataAccess
{
    public interface IReportDAL
    {
        IEnumerable<int> QueryYears();

        ReportEntity QueryReports(int reportID);

        void SaveNewImages(IEnumerable<ImageEntity> images);

        void DeleteImages(IEnumerable<ImageEntity> imageEntities);

        IEnumerable<ImageEntity> QueryImages(int reportID);

        IEnumerable<ImageEntity> QueryImages(Guid locationID);

        IEnumerable<ReportEntity> QueryReports(ReportFilter filter);

        void UpdateReport(ReportEntity report);

        int CreateReport(ReportEntity entity);

        IDictionary<string, string> ValidateNewReport(ReportEntity entity);

        void SynchronizeImagesForReport(int reportID, IEnumerable<ImageEntity> images);
    }
}
