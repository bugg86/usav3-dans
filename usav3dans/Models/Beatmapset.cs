namespace usav3dans.Models;

public class Beatmapset
{
    public string artist { get; set; }
    public string artist_unicode { get; set; }
    public Covers covers { get; set; }
    public string creator { get; set; }
    public int favourite_count { get; set; }
    public object hype { get; set; }
    public int id { get; set; }
    public bool nsfw { get; set; }
    public int offset { get; set; }
    public int play_count { get; set; }
    public string preview_url { get; set; }
    public string source { get; set; }
    public bool spotlight { get; set; }
    public string status { get; set; }
    public string title { get; set; }
    public string title_unicode { get; set; }
    public object track_id { get; set; }
    public int user_id { get; set; }
    public bool video { get; set; }
}