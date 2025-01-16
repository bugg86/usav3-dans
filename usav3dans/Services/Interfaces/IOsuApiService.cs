using Botvex.DB.Models;

namespace Botvex.osu.Services.Interfaces;

public interface IOsuApiService
{
    public Task<User> GetUser(int id);
    public Task<Beatmapset> GetBeatmapset(int id);
    public Task<Beatmap> GetBeatmap(int id);
}