using System;

namespace Duo.Models;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool PrivacyStatus { get; set; } = false; //0 = Public, 1 = Private
    public bool OnlineStatus { get; set; } = false; //0 = Offline, 1 = Online
    public DateTime DateJoined { get; set; } = DateTime.Now;//data la care a fost creat contul?
    public string ProfileImage { get; set; } = string.Empty;
    public int TotalPoints { get; set; } = 0;
    public int CoursesCompleted { get; set; } = 0;
    public int QuizzesCompleted { get; set; } = 0;
    public int Streak { get; set; } = 0;

    public string Password { get; set; } = string.Empty;
}
