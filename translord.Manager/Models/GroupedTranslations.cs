using translord.Models;

namespace translord.Manager.Models;

public class GroupedTranslations(int size)
{
    public required string Key { get; set; }

    public List<Translation> Translations { get; set; } = new List<Translation>();
}