CREATE   PROCEDURE dbo.sp_InsertReport 
	@reporttype int,
	@accessid uniqueidentifier,
	@date datetime,
	@totalfish int,
	@flow int,
	@water varchar(50),
	@weather varchar(50),
	@description varchar(8000),
	@username varchar(16),
	@reportid int out	
AS
BEGIN
INSERT INTO Reports(ReportType, accessid, Date, TotalFish, Flow, Water, Weather, Description, username) 
VALUES (@reporttype, @accessid, @Date, @TotalFish, @Flow, @Water, @Weather, @Description, @username); 

SELECT ReportID, accessid, Date, TotalFish, Flow, Water, Weather, Description, username FROM Reports WHERE (ReportID = @@IDENTITY)

select @reportid = @@identity
END