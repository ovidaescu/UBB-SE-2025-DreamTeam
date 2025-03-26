Use Duolingo;
go

CREATE OR ALTER PROCEDURE GetTopFriendsForCourse
    @UserId INT,
    @CourseId INT
AS
BEGIN
    WITH RelatedUsers AS (
        SELECT @UserId AS UserId
        UNION
        SELECT 
            CASE 
                WHEN UserId1 = @UserId THEN UserId2 
                ELSE UserId1 
            END
        FROM Friends
        WHERE UserId1 = @UserId OR UserId2 = @UserId
    )

    SELECT DISTINCT
        RU.UserId,
        U.Username, -- Added Username column
		U.ProfileImage,
        UC.LessonsCompleted,
        C.TotalNumberOfLessons,
        CAST(UC.LessonsCompleted AS DECIMAL(5,2)) / 
        CASE WHEN C.TotalNumberOfLessons > 0 THEN C.TotalNumberOfLessons ELSE 1 END * 100 AS CompletionPercentage
    FROM 
        RelatedUsers RU
    INNER JOIN 
        Users U ON U.UserId = RU.UserId -- Join with Users table
    INNER JOIN 
        UserCourses UC ON UC.UserId = RU.UserId
    INNER JOIN 
        Courses C ON C.CourseId = UC.CourseId
    WHERE 
        UC.CourseId = @CourseId
    ORDER BY 
        CompletionPercentage DESC
END

