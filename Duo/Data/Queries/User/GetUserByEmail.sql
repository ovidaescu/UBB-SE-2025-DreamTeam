CREATE OR ALTER PROCEDURE GetUserByEmail
    @Email NVARCHAR(100)
AS
BEGIN
    SELECT *
    FROM Users
    WHERE UserName = @Email;
END;
GO