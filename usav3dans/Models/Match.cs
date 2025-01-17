namespace usav3dans.Models;

public class MatchBase
{
    public Match match { get; set; }
    public Events[] events { get; set; }
    public Users[] users { get; set; }
    public long first_event_id { get; set; }
    public long latest_event_id { get; set; }
    public object current_game_id { get; set; }
}

public class Match
{
    public int id { get; set; }
    public string start_time { get; set; }
    public string end_time { get; set; }
    public string name { get; set; }
}

public class Events
{
    public long id { get; set; }
    public Detail detail { get; set; }
    public string timestamp { get; set; }
    public int? user_id { get; set; }
    public Game game { get; set; }
}

public class Detail
{
    public string type { get; set; }
    public string text { get; set; }
}

public class Game
{
    public int beatmap_id { get; set; }
    public int id { get; set; }
    public string start_time { get; set; }
    public string end_time { get; set; }
    public string mode { get; set; }
    public int mode_int { get; set; }
    public string scoring_type { get; set; }
    public string team_type { get; set; }
    public string[] mods { get; set; }
    public Beatmap beatmap { get; set; }
    public Scores[] scores { get; set; }
}

public class Covers
{
    public string cover { get; set; }
    public string cover_2x { get; set; }
    public string card { get; set; }
    public string card_2x { get; set; }
    public string list { get; set; }
    public string list_2x { get; set; }
    public string slimcover { get; set; }
    public string slimcover_2x { get; set; }
}

public class Scores
{
    public double accuracy { get; set; }
    public object best_id { get; set; }
    public string created_at { get; set; }
    public object id { get; set; }
    public int max_combo { get; set; }
    public string mode { get; set; }
    public int mode_int { get; set; }
    public string[] mods { get; set; }
    public bool passed { get; set; }
    public int perfect { get; set; }
    public object pp { get; set; }
    public string rank { get; set; }
    public bool replay { get; set; }
    public int score { get; set; }
    public Statistics statistics { get; set; }
    public string type { get; set; }
    public int user_id { get; set; }
    public Current_user_attributes current_user_attributes { get; set; }
    public Match1 match { get; set; }
}

public class Statistics
{
    public int count_100 { get; set; }
    public int count_300 { get; set; }
    public int count_50 { get; set; }
    public int count_geki { get; set; }
    public int count_katu { get; set; }
    public int count_miss { get; set; }
}

public class Current_user_attributes
{
    public object pin { get; set; }
}

public class Match1
{
    public int slot { get; set; }
    public string team { get; set; }
    public bool pass { get; set; }
}

public class Users
{
    public string avatar_url { get; set; }
    public string country_code { get; set; }
    public string default_group { get; set; }
    public int id { get; set; }
    public bool is_active { get; set; }
    public bool is_bot { get; set; }
    public bool is_deleted { get; set; }
    public bool is_online { get; set; }
    public bool is_supporter { get; set; }
    public string last_visit { get; set; }
    public bool pm_friends_only { get; set; }
    public object profile_colour { get; set; }
    public string username { get; set; }
    public Country country { get; set; }
}

public class Country
{
    public string code { get; set; }
    public string name { get; set; }
}

