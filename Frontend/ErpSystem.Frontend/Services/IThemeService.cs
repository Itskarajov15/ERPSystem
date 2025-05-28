namespace ErpSystem.Frontend.Web.Services;

public interface IThemeService
{
    string CurrentTheme { get; }
    void ToggleTheme();
    void SetTheme(string theme);
    bool IsDarkMode();
} 