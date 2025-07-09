--UPDATE USER
CREATE PROCEDURE UPDATE_MOVIE_PR
	@P_Id Int,
	@P_Updated DATETIME,
	@P_Title Nvarchar(75),
	@P_Description nvarchar(250),
	@P_ReleaseDate DATETIME,
	@P_Genre nvarchar(20),
	@P_Director Nvarchar(30)
AS
BEGIN
	UPDATE TBL_Movie
	SET
		Updated= @P_Updated,
		Title = @P_Title,
        Description = @P_Description,
        ReleaseDate = @P_ReleaseDate,
        Genre = @P_Genre,
        Director = @P_Director
    WHERE Id = @P_Id
END
GO
