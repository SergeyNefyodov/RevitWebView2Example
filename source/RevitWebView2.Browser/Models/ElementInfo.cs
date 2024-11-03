namespace RevitWebView2.Browser.Models;

[UsedImplicitly]
public record ElementInfo
{
    public string Value { get; init; }
    public string TimeStamp { get; init; }
    public ElementId Id { get; init; }
}