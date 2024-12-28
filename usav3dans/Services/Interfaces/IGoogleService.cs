using Google.Apis.Sheets.v4.Data;

namespace usav3dans.Services.Interfaces;

public interface IGoogleService
{
    Task<ValueRange> PushMp(string mp);
}