using System;

namespace Duo.Models;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool PrivacyStatus { get; set; } = false;
    public bool OnlineStatus { get; set; } = false;
    public DateTime DateJoined { get; set; } = DateTime.Now;
    public string ProfileImage { get; set; } = string.Empty;
    public int TotalPoints { get; set; } = 0;
    public int CoursesCompleted { get; set; } = 0;
    public int QuizzesCompleted { get; set; } = 0;
    public int Streak { get; set; } = 0;
}
