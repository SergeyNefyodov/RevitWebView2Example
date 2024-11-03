namespace RevitWebView2.Browser.Models;

[UsedImplicitly]
public record ElementInfo
{
    public string Value { get; set; }
    public string TimeStamp { get; set; }
    public ElementId Id { get; set; }
}