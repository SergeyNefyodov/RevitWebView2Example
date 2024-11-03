using Autodesk.Revit.UI;

namespace RevitWebView2.Browser.Services;

public sealed class BrowserDockPanelService
{
    public void Execute()
    {
        var panelId = new DockablePaneId(new Guid("B1BAC295-93CF-4181-B21B-BC26369B1E0D"));
        if (!DockablePane.PaneExists(panelId))
        {
            TaskDialog.Show("Revit Web Browser unavailable",
                """
                The Web Browser is not available for the current project.
                It might have been opened outside the Revit interface. Try reopening the project.
                """);
            return;
        }

        var dockPanel = Context.UiApplication.GetDockablePane(panelId);
        if (dockPanel.IsShown())
        {
            dockPanel.Hide();
        }
        else
        {
            dockPanel.Show();
        }
    }
}