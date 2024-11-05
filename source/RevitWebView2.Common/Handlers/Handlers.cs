using Nice3point.Revit.Toolkit.External.Handlers;

namespace RevitWebView2.Common.Handlers;

public static class Handlers
{
    public static AsyncEventHandler<int> AsyncHandler { get; private set; }

    public static void SetupHandlers()
    {
        AsyncHandler = new AsyncEventHandler<int>();
    }
}