using Nice3point.Revit.Toolkit.External;
using RevitWebView2.Common.Handlers;
using RevitWebView2Application.Commands;
using RevitWebView2Application.Managers;

namespace RevitWebView2Application;

/// <summary>
///     Application entry point
/// </summary>
[UsedImplicitly]
public class Application : ExternalApplication
{
    public override void OnStartup()
    {
        Host.Start();
        CreateRibbon();
        PanelManager.Register(Application, Host.GetService<IServiceProvider>());
        Handlers.SetupHandlers();
    }

    public override void OnShutdown()
    {
        Host.Stop();
    }

    private void CreateRibbon()
    {
        var panel = Application.CreatePanel("Commands", "Revit Web View");

        panel.AddPushButton<ShowBrowser>("Show Browser")
            .SetImage("/RevitWebView2.Application;component/Resources/Icons/RibbonIcon16.png")
            .SetLargeImage("/RevitWebView2.Application;component/Resources/Icons/RibbonIcon32.png");
    }
}