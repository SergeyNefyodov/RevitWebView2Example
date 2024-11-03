using System.Text.Json;
using Microsoft.Web.WebView2.Wpf;

namespace RevitWebView2.Browser.Services;

public class WebMessageService(RevitDataService revitDataService)
{
    public WebView2 WebView { get; set; }

    public async Task HandleMessageAsync(string message)
    {
        var json = JsonDocument.Parse(message);
        var messageType = json.RootElement.GetProperty("type").GetString();

        switch (messageType)
        {
            case "call":
                await SendTableDataAsync();
                break;
            case "update":
            {
                var data = json.RootElement.GetProperty("data").GetString();
                var result = await revitDataService.UpdateData(data);
                await SendNotificationAsync(result);
                break;
            }
            default:
                throw new ArgumentException("Unknown message type");
        }
    }

    private async Task SendNotificationAsync(int text)
    {
        var script = $"notify({text});";
        await WebView.CoreWebView2.ExecuteScriptAsync(script);
    }

    private async Task SendTableDataAsync()
    {
        var data = revitDataService.ReadData();
        var script = $"loadDataTable({data});";
        await WebView.CoreWebView2.ExecuteScriptAsync(script);
    }
}