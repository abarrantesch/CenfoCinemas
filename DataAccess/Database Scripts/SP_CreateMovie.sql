--SP para crear peliculas
CREATE PROCEDURE CRE_MOVIE_PR
	@P_Title nvarchar(75),
	@P_Description nvarchar(250),
	@P_ReleaseDate datetime,
	@P_Genre nvarchar(20),
	@P_Director nvarchar(30)

AS
BEGIN
	INSERT INTO TBL_Movie(Created, Title, Description, ReleaseDate, Genre, Director)
	VALUES (GETDATE(),@P_Title,@P_Description, @P_ReleaseDate, @P_Genre, @P_Director);
END
GO
