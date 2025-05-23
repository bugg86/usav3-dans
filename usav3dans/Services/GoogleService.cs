using usav3dans.Services.Interfaces;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;

namespace usav3dans.Services;

public class GoogleService : IGoogleService
{
    private readonly string authFile = "./sheetauth.json";
    private readonly string dansY1Q1SpreadsheetId = "1DmT88CjgTQwxO1-2d1JwKAKzsk105obSIBjadtZnAQw";
    private readonly string dansY1Q2SpreadsheetId = "1uDOsSjtNJrF42s7HKq3xJiE25HknTVMkdaYtLN9YT0A";
    private readonly string dansSheetTitle = "Import";
    private readonly string tryoutsSheetId = "1UctdsBog2ESkTJamwSsoY_rNu1RcVRNds796eX8QtSY";

    public async Task<ValueRange> PushDansMp(string mp, string sheet)
    {
        string dansSpreadsheetId = "";
        
        switch (sheet)
        {
            case "Y1Q1":
                dansSpreadsheetId = dansY1Q1SpreadsheetId;
                break;
            case "Y1Q2":
                dansSpreadsheetId = dansY1Q2SpreadsheetId;
                break;
        }
        
        var service = InitService();
        var mps = await service.Spreadsheets.Values.Get(dansSpreadsheetId, "Import!H3:H").ExecuteAsync();

        mps.Values.Insert(mps.Values.Count, new List<object>(){mp});
        mps.MajorDimension = "ROWS";

        var update = service.Spreadsheets.Values.Update(mps, dansSpreadsheetId, "Import!H3:H1001");
        update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
        var response = await update.ExecuteAsync();

        return response.UpdatedData;
    }

    public async Task<ValueRange> PushTryoutsMp(string mp)
    {
        var service = InitService();
        var mps = await service.Spreadsheets.Values.Get(tryoutsSheetId, "mps_from_bot!A2:A").ExecuteAsync();

        mps.Values.Insert(mps.Values.Count, new List<object>(){mp});
        mps.MajorDimension = "ROWS";

        var update = service.Spreadsheets.Values.Update(mps, tryoutsSheetId, "mps_from_bot!A2:A1000");
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