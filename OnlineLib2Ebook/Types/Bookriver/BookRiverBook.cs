using System.Text.Json.Serialization;

namespace OnlineLib2Ebook.Types.Bookriver; 

public class BookRiverBook {
    [JsonPropertyName("currentBook")]
    public BookRiverCurrentBook Book { get; set; }
}