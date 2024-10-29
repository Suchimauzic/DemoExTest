-- Процедура выдаёт базу о выполненных заявках мастера по его ФИО
-- ==============================================================

CREATE PROCEDURE GetRequestMasterName
	@MasterName nvarchar(250) = NULL
AS
BEGIN
	select *
	from Request INNER JOIN
	[User] ON Request.RequestMasterId = [User].UserId
	where [User].UserFullName = @MasterName AND Request.RequestStatusId=3
END
GO

exec GetRequestMasterName N'Архипов Варлам Мэлорович'