namespace RevitWebView2.Browser.Models;

[UsedImplicitly]
public record ElementInfoDto
{
    public required string Value { get; set; }
    public required string TimeStamp { get; set; }
    public required string Id { get; set; }
    public required string Name { get; set; }
}