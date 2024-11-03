using SchemaMigrations.Abstractions;

namespace RevitWebView2.Browser.Models;

public class BrowserSchemaContext : SchemaContext
{
    public SchemaSet<ElementInfo> ElementInformation { get; } = new();
}