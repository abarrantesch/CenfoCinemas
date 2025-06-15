--Retrieve Movie by ID
CREATE PROCEDURE RET_MOVIE_ID @MovieId INT
AS
BEGIN

		SELECT Id, Created, Updated, Title, Description, ReleaseDate, Genre, Director
		FROM TBL_Movie where Id=@MovieId

END
GO