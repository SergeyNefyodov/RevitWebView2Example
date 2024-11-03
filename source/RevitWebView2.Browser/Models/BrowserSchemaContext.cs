using SchemaMigrations.Abstractions;

namespace RevitWebView2.Browser.Models;

[UsedImplicitly]
public class BrowserSchemaContext : SchemaContext
{
    [UsedImplicitly]
    public SchemaSet<ElementInfo> ElementInformation { get; } = new();
}