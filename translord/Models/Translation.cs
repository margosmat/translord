using translord.Enums;

namespace translord.Models;

public class Translation
{
    public required string Key { get; set; }
    public required string Value { get; set; }
    public Language Language { get; set; }
}