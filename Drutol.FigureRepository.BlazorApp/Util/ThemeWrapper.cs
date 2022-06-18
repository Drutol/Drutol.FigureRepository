using MudBlazor;

namespace Drutol.FigureRepository.BlazorApp.Util;

public class ThemeWrapper
{
    private readonly MudTheme _theme;
    private readonly Func<bool> _isDarkMode;

    public ThemeWrapper(MudTheme theme, Func<bool> isDarkMode)
    {
        _theme = theme;
        _isDarkMode = isDarkMode;
    }

    public Palette Palette => _isDarkMode() ? _theme.PaletteDark : _theme.Palette;
}