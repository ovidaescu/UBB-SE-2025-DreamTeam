using System;

namespace Duo.Models;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool PrivacyStatus { get; set; }
    public bool OnlineStatus { get; set; }
    public DateTime DateJoined { get; set; }
    public string ProfileImage { get; set; } = string.Empty;
    public int TotalPoints { get; set; }
    public int CoursesCompleted{ get; set; }
    public int QuizzesCompleted { get; set; }
    public int Streak { get; set; }





}
