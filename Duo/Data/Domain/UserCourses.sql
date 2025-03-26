Use Duolingo;
Go

DROP TABLE IF EXISTS UserCourses

CREATE TABLE UserCourses(
    UserId INT NOT NULL REFERENCES Users(UserId),
    CourseId INT NOT NULL REFERENCES Courses(CourseId) ,
    LessonsCompleted INT NOT NULL DEFAULT 0,
	PRIMARY KEY (UserId, CourseId)
)

INSERT INTO UserCourses (UserId, CourseId, LessonsCompleted)
VALUES 
    (1, 1, 7),   -- User 1 progress in Programming
    (1, 2, 5),   -- Same user in Web Development
    (2, 1, 10),  -- User 2 completed Programming course
    (2, 3, 6),   -- User 2 partially through Data Science
    (3, 4, 12),  -- User 3 more than halfway in Mobile App Dev
    (3, 5, 4),   -- User 3 started Cybersecurity
    (4, 2, 15),  -- User 4 completed Web Development
    (4, 3, 9),   -- User 4 almost done with Data Science
    (5, 1, 3),   -- User 5 just starting Programming
    (5, 5, 6)    -- User 5 nearly complete with Cybersecurity

SELECT * FROM UserCourses
