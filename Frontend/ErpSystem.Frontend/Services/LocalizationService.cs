using ErpSystem.Frontend.Web.Resources;

namespace ErpSystem.Frontend.Web.Services;

public interface ILocalizationService
{
    string Translate(string key);
    string TranslateEndpoint(string controllerName, string actionName);
}

public class LocalizationService : ILocalizationService
{
    public string Translate(string key)
    {
        return LocalizationResources.BulgarianTranslations.TryGetValue(key, out var translation)
            ? translation
            : key;
    }

    public string TranslateEndpoint(string controllerName, string actionName)
    {
        var translatedController = Translate(controllerName);
        var translatedAction = Translate(actionName);
        
        return $"{translatedController}.{translatedAction}";
    }
} 