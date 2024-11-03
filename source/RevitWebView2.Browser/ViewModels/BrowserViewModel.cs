using System.IO;
using Microsoft.Web.WebView2.Wpf;

namespace RevitWebView2.Browser.ViewModels;

public sealed partial class BrowserViewModel : ObservableObject
{
    public CoreWebView2CreationProperties WebViewCreationProperties { get; } = new()
    {
        UserDataFolder = Path.GetTempPath()
    };

    public event EventHandler UpdateRequest;

    [RelayCommand]
    private void RequestUpdate()
    {
        UpdateRequest?.Invoke(this, EventArgs.Empty);
    }
}