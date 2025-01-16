namespace usav3dans.Models;

public class Beatmap
{
    public int beatmapset_id { get; set; }
    public double difficulty_rating { get; set; }
    public int id { get; set; }
    public string mode { get; set; }
    public string status { get; set; }
    public int total_length { get; set; }
    public int user_id { get; set; }
    public string version { get; set; }
    public Beatmapset beatmapset { get; set; }
}