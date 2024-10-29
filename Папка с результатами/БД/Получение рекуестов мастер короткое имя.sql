-- Процедура выдает информаци о заявказ мастера по его частичному ФИО
-- ==================================================================

CREATE PROCEDURE GetRequestMasterShortName
	@MasterName NVARCHAR(150) = NULL
AS
BEGIN
	SELECT *
	FROM Request Inner Join
	[User] ON Request.RequestMasterId = [User].UserId
	WHERE [User].UserFullName LIKE ('%' + @MasterName + '%')
END
GO

exec GetRequestMasterShortName N'Архи'