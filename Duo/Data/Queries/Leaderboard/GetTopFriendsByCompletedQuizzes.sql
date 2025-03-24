Use Duolingo
Go

CREATE or ALTER PROCEDURE GetTopFriendsByCompletedQuizzes
    @userId INT
AS
BEGIN
    SELECT TOP 10 *
    FROM
        Users u
    WHERE
        u.UserId IN (
            SELECT
                f.FriendId
            FROM
                Friendships f
            WHERE
                f.UserId = @userId
        )
    ORDER BY
        u.QuizzesCompleted DESC;
END