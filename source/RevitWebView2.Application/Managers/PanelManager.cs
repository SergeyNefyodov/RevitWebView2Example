using System.IO;
using System.Reflection;
using Autodesk.Revit.UI;
using Nice3point.Revit.Toolkit.Decorators;
using Nice3point.Revit.Toolkit.Options;
using RevitWebView2.Browser.Views;

namespace RevitWebView2Application.Managers;

public static class PanelManager
{
    public static void Register(UIControlledApplication application, IServiceProvider serviceProvider)
    {
#if REVIT2025_OR_GREATER
        AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
#endif
        DockablePaneProvider.Register(application).SetId(new Guid("B1BAC295-93CF-4181-B21B-BC26369B1E0D"))
            .SetTitle("Browser view")
            .SetConfiguration(data =>
            {
                data.FrameworkElementCreator = new FrameworkElementCreator<BrowserView>(serviceProvider);
                data.VisibleByDefault = true;
                data.InitialState = new DockablePaneState
                {
                    DockPosition = DockPosition.Right,
                    MinimumWidth = 350,
                    MinimumHeight = 350
                };
            });
    }
#if REVIT2025_OR_GREATER
    private static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
    {
        var assemblyName = new AssemblyName(args.Name).Name;
        var assemblyPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, $"{assemblyName}.dll");
        if (!File.Exists(assemblyPath)) return null;

        return Assembly.LoadFrom(assemblyPath);
    }
#endif
}