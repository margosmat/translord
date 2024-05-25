using translord.Models;

namespace translord.Manager.Models;

public class GroupedTranslations
{
    public required string Key { get; set; }

    public List<Translation> Translations { get; init; } = [];
}