
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[RET_MOVIE_BY_TITLE_PR] @P_Title NVARCHAR(75)
AS
BEGIN

    SELECT Id, Created, Updated, Title, Description, ReleaseDate, Genre, Director
	FROM TBL_Movie where Title = @P_Title

END