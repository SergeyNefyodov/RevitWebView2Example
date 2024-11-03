using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;
using RevitWebView2.Browser.Services;

namespace RevitWebView2Application.Commands;

/// <summary>
///     External command entry point invoked from the Revit interface
/// </summary>
[UsedImplicitly]
[Transaction(TransactionMode.Manual)]
public class ShowBrowser : ExternalCommand
{
    public override void Execute()
    {
        var service = Host.GetService<BrowserDockPanelService>();
        service.Execute();
    }
}