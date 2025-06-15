--Retrieve User by ID
CREATE PROCEDURE RET_USER_ID @UserId INT
AS
BEGIN

    SELECT Id, Created, Updated, UserCode, Name, Email, Password, BirthDate, Status
	FROM TBL_User where Id = @UserId

END
GO

