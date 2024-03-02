using translord.Core;

namespace translord;

public class TranslatorConfiguration
{
    public TranslatorConfiguration()
    {
        
    }

    public ITranslator CreateTranslator()
    {
        return new Translator();
    }
}