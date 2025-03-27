Use Duolingo;
Go

CREATE OR ALTER PROCEDURE GetTopUsersForCourse
    @CourseId INT
AS
BEGIN
    SELECT TOP 10 
        UC.UserId,
        U.Username,
		U.ProfileImage,
        CAST(UC.LessonsCompleted AS DECIMAL(5,2)) / 
        CASE 
            WHEN C.TotalNumberOfLessons > 0 
            THEN C.TotalNumberOfLessons 
            ELSE 1 
        END * 100 AS CompletionPercentage
    FROM 
        UserCourses UC
    INNER JOIN 
        Courses C ON UC.CourseId = C.CourseId
    INNER JOIN 
        Users U ON U.UserId = UC.UserId
    WHERE 
        UC.CourseId = @CourseId
    ORDER BY 
        CompletionPercentage DESC
END
