Use Duolingo;
go

DROP TABLE IF EXISTS Courses

CREATE TABLE Courses(
    CourseId INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    TotalNumberOfLessons INT NOT NULL DEFAULT 0
)

INSERT INTO Courses (Name, TotalNumberOfLessons)
VALUES 
    ('Introduction to Programming', 10),
    ('Web Development Fundamentals', 15),
    ('Data Science Basics', 12),
    ('Mobile App Development', 20),
    ('Cybersecurity Essentials', 8)

SELECT * FROM Courses
