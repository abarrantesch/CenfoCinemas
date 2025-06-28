--UPDATE USER
CREATE PROCEDURE UPDATE_USER_PR
    @P_Id INT,
    @P_Created DATETIME,
    @P_Updated DATETIME,
    @P_UserCode NVARCHAR(30),
    @P_Name NVARCHAR(50),
    @P_Email NCHAR(30),
    @P_Password NCHAR(50),
    @P_BirthDate DATETIME,
    @P_Status NVARCHAR(10)
AS
BEGIN
	UPDATE TBL_User
	SET
        [Created] = @P_Created,
        [Updated] = @P_Updated,
        [UserCode] = @P_UserCode,
        [Name] = @P_Name,
        [Email] = @P_Email,
        [Password] = @P_Password,
        [BirthDate] = @P_BirthDate,
        [Status] = @P_Status
    WHERE Id = @P_Id;
END
GO
