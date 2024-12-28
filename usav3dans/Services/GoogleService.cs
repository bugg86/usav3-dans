using usav3dans.Services.Interfaces;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;

namespace usav3dans.Services;

public class GoogleService : IGoogleService
{
    private readonly string authFile = "../../../sheetauth.json";
    private readonly string spreadsheetId = "1DmT88CjgTQwxO1-2d1JwKAKzsk105obSIBjadtZnAQw";
    private readonly string sheetTitle = "Import";

    public async Task<ValueRange> PushMp(string mp)
    {
        var service = InitService();
        var mps = await service.Spreadsheets.Values.Get(spreadsheetId, "Import!H3:H").ExecuteAsync();

        mps.Values.Insert(mps.Values.Count, new List<object>(){mp});
        mps.MajorDimension = "ROWS";

        var update = service.Spreadsheets.Values.Update(mps, spreadsheetId, "Import!H3:H1001");
        update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
        var response = await update.ExecuteAsync();

        return response.UpdatedData;
    }

    private SheetsService InitService()
    {
        var credential = GoogleCredential.FromFile(authFile)
            .CreateScoped(SheetsService.Scope.Spreadsheets);

        var service = new SheetsService(new BaseClientService.Initializer { HttpClientInitializer = credential });
        return service;
    }
}