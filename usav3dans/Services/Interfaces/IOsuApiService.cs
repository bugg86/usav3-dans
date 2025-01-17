using usav3dans.Models;

namespace usav3dans.Services.Interfaces;

public interface IOsuApiService
{
    public Task<User> GetUser(int id);
    public Task<Beatmapset> GetBeatmapset(int id);
    public Task<Beatmap> GetBeatmap(int id);
    public Task<MatchBase> GetMatch(int id);
}