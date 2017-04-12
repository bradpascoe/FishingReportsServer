CREATE    PROCEDURE [dbo].[sp_UpdateReport] 
	@reportid int,
	@reporttype int,
	@date datetime,
	@totalfish int,
	@flow int,
	@water varchar(50),
	@weather varchar(50),
	@description text
As

begin transaction

UPDATE Reports 
	SET 	
		ReportType = @reportType,
		[Date] = @Date, 
		TotalFish = @TotalFish,
		Flow = @Flow, 
		Water = @Water, 
		Weather = @Weather, 
		[Description] = @Description 
	WHERE 
		(ReportID = @reportid)

	-- Did someone else delete or modify the row?
	IF ( @@ROWCOUNT <> 1 )
		BEGIN
			ROLLBACK TRANSACTION
			RETURN 0
		END

	-- Groovy, our update made it. Return the new version number to the client.
	ELSE
		BEGIN
			COMMIT TRANSACTION
	--		PRINT @version
			RETURN 1
		END
GO