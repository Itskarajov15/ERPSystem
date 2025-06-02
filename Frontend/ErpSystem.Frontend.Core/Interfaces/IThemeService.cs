namespace ErpSystem.Frontend.Core.Interfaces;

public interface IThemeService
{
    string CurrentTheme { get; }
    void ToggleTheme();
    void SetTheme(string theme);
    bool IsDarkMode();
}
