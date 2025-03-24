using System.Globalization;
using System.Resources;
using KopiaBackup.Lib.Interfaces.Helpers;

namespace KopiaBackup.Lib.Helpers;

public class ResourceLocalizerHelper : IResourceLocalizerHelper
{
    private readonly ResourceManager _resourceManager = new("MyLibrary.Localization.Resources", typeof(ResourceLocalizerHelper).Assembly);
    
    public string? GetString(string key)
    {
        return _resourceManager.GetString(key, CultureInfo.CurrentUICulture);
    }
}