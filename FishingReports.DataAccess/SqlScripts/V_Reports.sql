
--Drop View V_Reports
Create View dbo.V_Reports
AS
SELECT     
	reports.AccessID,
	reports.Date,
	reports.Description,
	reports.Flow,
	reports.ReportID,
	reports.ReportType,
	reports.Revision,
	ISNULL(reports.TotalFish, 0) as TotalFish,
	reports.UserName,
	reports.Water,
	reports.Weather, 
	access.Description AS Access, 
	Location.Description AS Location, 
	State.State AS State,
	ISNULL((SELECT count(*) 
		from dbo.Images images 
		where images.ReportID = reports.ReportID), 0) 
		AS NumberOfImages
FROM         
	dbo.Reports reports 
	JOIN dbo.Access as access ON access.AccessID = reports.AccessID 
	JOIN dbo.Location as location ON Location.LocationID = access.locationid 
	JOIN dbo.State ON State.StateID = Location.StateID
	

