SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE GetInfoRequestFromMasterName
	-- Add the parameters for the stored procedure here
	@IdMaster int = 105
AS
BEGIN
    -- Insert statements for procedure here
	Select * from Request
	where RequestMasterId = @IdMaster and RequestStatusId = 3
END
GO

