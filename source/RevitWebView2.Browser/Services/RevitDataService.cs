using System.Text.Json;
using Nice3point.Revit.Toolkit.External.Handlers;
using RevitWebView2.Browser.Models;
using SchemaMigrations.Database;

namespace RevitWebView2.Browser.Services;

public class RevitDataService
{
    private readonly AsyncEventHandler<int> _handler = new();

    public async Task<int> UpdateDataAsync(string text)
    {
        var ids = Context.ActiveUiDocument!.Selection.GetElementIds();
        if (ids.Count == 0) return 0;

        var result = await _handler.RaiseAsync(_ =>
        {
            try
            {
                WriteData(text, ids);
                return ids.Count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        });
        return result;
    }

    public string ReadData()
    {
        var ids = Context.ActiveUiDocument!.Selection.GetElementIds();
        if (ids.Count == 0) return string.Empty;

        var result = new List<ElementInfoDto>();
        foreach (var id in ids)
        {
            var element = id.ToElement(Context.ActiveDocument)!;
            if (element == null) continue;

            var connection = new DatabaseConnection<ElementInfo>(element);
            var info = connection.LoadObject();

            if (info is null) continue;

            result.Add(new ElementInfoDto
            {
                Id = info.Id.ToString(),
                Name = element.Name,
                TimeStamp = info.TimeStamp,
                Value = info.Value
            });
        }

        return JsonSerializer.Serialize(result);
    }

    private void WriteData(string text, ICollection<ElementId> ids)
    {
        using var transaction = new Transaction(Context.ActiveDocument, "Update Data From Web");
        transaction.Start();
        foreach (var id in ids)
        {
            var element = id.ToElement(Context.ActiveDocument)!;
            var connection = new DatabaseConnection<ElementInfo>(element);
            if (string.IsNullOrEmpty(text))
            {
                connection.DeleteObject();
            }
            else
            {
                var info = new ElementInfo
                {
                    Id = element.Id,
                    TimeStamp = DateTime.Now.ToString("dd MMMM yyyy, HH:mm:ss"),
                    Value = text
                };
                connection.SaveObject(info);
            }
        }

        transaction.Commit();
    }
}