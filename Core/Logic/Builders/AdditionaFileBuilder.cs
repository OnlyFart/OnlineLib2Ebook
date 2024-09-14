using System.IO;
using System.Threading.Tasks;
using Core.Configs;
using Core.Extensions;
using Core.Types.Book;
using Microsoft.Extensions.Logging;

namespace Core.Logic.Builders;

public class AdditionaFileBuilder  {
    private readonly Options _options;
    private readonly ILogger _logger;

    public AdditionaFileBuilder(Options options, ILogger logger) {
        _options = options;
        _logger = logger;
    }

    public async Task Build(Book book) {
        if (book.AdditionalFiles.Count == 0) {
            _logger.LogInformation("Нет дополнительных файлов");
            return;
        }

        var additionalPath = $"{book.Author.Name} - {book.Title}".Crop(100).RemoveInvalidChars();
        if (!string.IsNullOrWhiteSpace(_options.SavePath)) {
            additionalPath = Path.Combine(_options.SavePath, additionalPath);
        }
        
        if (!Directory.Exists(additionalPath)) {
            Directory.CreateDirectory(additionalPath);
        }
        
        foreach (var file in book.AdditionalFiles) {
            var fileName = Path.Combine(additionalPath, file.FullName);
            _logger.LogInformation($"Начинаю сохранение дополнительного файла {fileName.CoverQuotes()}");
            await File.WriteAllBytesAsync(fileName, file.Content);
            _logger.LogInformation($"Cохранение дополнительного файла {fileName.CoverQuotes()} завершено");
        }
        
    }
}