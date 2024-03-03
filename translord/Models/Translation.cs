using translord.Enums;

namespace translord.Models;

public class Translation
{
    public string Key { get; set; }
    public string Value { get; set; }
    public Language Language { get; set; }
}