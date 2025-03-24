﻿namespace Duo.ViewModels;

public class LeaderboardEntry
{
    public int Rank { get; set; }
    public string ProfilePicture { get; set; }
    public string Username { get; set; }
    public decimal Accuracy { get; set; }
    public int CompletedQuizzes { get; set; }

}
