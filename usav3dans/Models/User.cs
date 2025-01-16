namespace usav3dans.Models;

public class User
{
    public int id { get; set; }
    public string username { get; set; }
    public string profile_colour { get; set; }
    public string avatar_url { get; set; }
    public string country_code { get; set; }
    public bool is_active { get; set; }
    public bool is_bot { get; set; }
    public bool is_deleted { get; set; }
    public bool is_online { get; set; }
    public bool is_supporter { get; set; }
}

