CREATE OR ALTER PROCEDURE GetTopFriendsByAccuracy
    @userId INT
AS
BEGIN
    SELECT TOP 10 *
    FROM
        Users u
    WHERE
        u.UserId IN (
            SELECT @userId
            UNION
            SELECT
                f.FriendId
            FROM
                Friendships f
            WHERE
                f.UserId = @userId
        )
    ORDER BY
        u.Accuracy DESC;
END