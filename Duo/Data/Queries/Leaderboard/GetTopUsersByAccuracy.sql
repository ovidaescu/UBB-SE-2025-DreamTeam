CREATE or ALTER PROCEDURE GetTopUsersByCompletedQuizzes
AS
Begin
	Select TOP 10 *
	FROM Users u
	ORDER By u.Accuracy DESC
End