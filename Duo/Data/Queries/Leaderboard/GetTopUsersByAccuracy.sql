CREATE or ALTER PROCEDURE GetTopUsersByAccuracy
AS
Begin
	Select TOP 10 *
	FROM Users u
	ORDER By u.Accuracy DESC
End