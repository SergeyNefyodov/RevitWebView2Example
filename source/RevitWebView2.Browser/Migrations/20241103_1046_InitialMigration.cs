using SchemaMigrations.Abstractions;
using SchemaMigrations.Database.Schemas;
using SchemaMigrations.Abstractions.Models;

namespace RevitWebView2.Browser.Migrations;
public class InitialMigration_20241103_1046 : Migration
{
    
    public override Dictionary<string, Guid> GuidDictionary { get; } = new Dictionary<string, Guid>()
    {
        { "ElementInformation", new Guid("491F0FB0-FA10-4AC0-8B68-17B183DFF4B7") },
    };

    public override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddSchemaData(new SchemaBuilderData()
        {
            Guid = GuidDictionary["ElementInformation"],
            Documentation = "Initial schema for ElementInfo",
            Name = "ElementInformation",
            VendorId = "SergeiNefedov"
        },
        new SchemaDescriptor("ElementInformation")
        {
            Fields = new List<FieldDescriptor>()
            {
                new FieldDescriptor( "Value", typeof(String) ),
                new FieldDescriptor( "TimeStamp", typeof(String) ),
                new FieldDescriptor( "Id", typeof(ElementId) )
            }
        });

    }    
}