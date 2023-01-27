using Drutol.FigureRepository.Shared.Models.Enums;
using Drutol.FigureRepository.Shared.Models.Figure;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

namespace Drutol.FigureRepository.BlazorApp.Util;

public static class Extensions
{
    static string[] prefixes = { "f", "a", "p", "n", "μ", "m", string.Empty, "k", "M", "G", "T", "P", "E" };
    private static readonly Random Random = new Random();

    public static void OpenGallery(this IEnumerable<FigureMedia> media, IJSRuntime js, int index)
    {
        js.InvokeVoidAsync("openLightBox",
            media.Select(m => new
            {
                src = m.Url,
                width = m.Width,
                height = m.Height,
            }).ToArray(),
            index);
    }

    public static string TruncateAddress(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;

        return $"{str.Substring(0, 5)}...{str.Substring(str.Length - 4, 4)}";
    }

    public static string ToCssValue(this FigurePhotoGravity gravity)
    {
        return gravity switch
        {
            FigurePhotoGravity.Center => "center",
            FigurePhotoGravity.Top => "top",
            FigurePhotoGravity.Bottom => "bottom",
            _ => throw new ArgumentOutOfRangeException(nameof(gravity), gravity, null)
        };
    }

    public static int GetDeterministicHashCode(this string str)
    {
        unchecked
        {
            int hash1 = (5381 << 16) + 5381;
            int hash2 = hash1;

            for (int i = 0; i < str.Length; i += 2)
            {
                hash1 = (hash1 << 5) + hash1 ^ str[i];
                if (i == str.Length - 1)
                    break;
                hash2 = (hash2 << 5) + hash2 ^ str[i + 1];
            }

            return hash1 + hash2 * 1566083941;
        }
    }

    public static List<T> Shuffle<T>(this List<T> list)
    {
        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = Random.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }

        return list;
    }

    public static bool IsPrerendering(this IWebAssemblyHostEnvironment env)
    {
        return env.Environment == "Prerendering";
    }

    // Returns the human-readable file size for an arbitrary, 64-bit file size 
    // The default format is "0.### XB", e.g. "4.2 KB" or "1.434 GB"
    public static string Bytes(this double i)
    {
        return Bytes((long)i);
    }

    public static string Bytes(this long i)
    {
        // Get absolute value
        long absolute_i = (i < 0 ? -i : i);
        // Determine the suffix and readable value
        string suffix;
        double readable;
        if (absolute_i >= 0x1000000000000000) // Exabyte
        {
            suffix = "EB";
            readable = (i >> 50);
        }
        else if (absolute_i >= 0x4000000000000) // Petabyte
        {
            suffix = "PB";
            readable = (i >> 40);
        }
        else if (absolute_i >= 0x10000000000) // Terabyte
        {
            suffix = "TB";
            readable = (i >> 30);
        }
        else if (absolute_i >= 0x40000000) // Gigabyte
        {
            suffix = "GB";
            readable = (i >> 20);
        }
        else if (absolute_i >= 0x100000) // Megabyte
        {
            suffix = "MB";
            readable = (i >> 10);
        }
        else if (absolute_i >= 0x400) // Kilobyte
        {
            suffix = "KB";
            readable = i;
        }
        else
        {
            return i.ToString("0 B"); // Byte
        }
        // Divide by 1024 to get fractional value
        readable = (readable / 1024);
        // Return formatted number with suffix
        return readable.ToString("0.## ") + suffix;
    }

    public static string Metric(this int x, int significantDigits)
    {
        return Metric((double)x, significantDigits);
    }

    public static string Metric(this double x, int significantDigits)
    {
        //Check for special numbers and non-numbers
        if (double.IsInfinity(x) || double.IsNaN(x) || x == 0 || significantDigits <= 0)
        {
            return x.ToString();
        }
        // extract sign so we deal with positive numbers only
        int sign = Math.Sign(x);
        x = Math.Abs(x);
        // get scientific exponent, 10^3, 10^6, ...
        int sci = x == 0 ? 0 : (int)Math.Floor(Math.Log(x, 10) / 3) * 3;
        // scale number to exponent found
        x = x * Math.Pow(10, -sci);
        // find number of digits to the left of the decimal
        int dg = x == 0 ? 0 : (int)Math.Floor(Math.Log(x, 10)) + 1;
        // adjust decimals to display
        int decimals = Math.Min(significantDigits - dg, 15);
        // format for the decimals
        string fmt = new string('0', decimals);
        if (sci == 0)
        {
            //no exponent
            return string.Format("{0}{1:0." + fmt + "}",
                sign < 0 ? "-" : string.Empty,
                Math.Round(x, decimals));
        }
        // find index for prefix. every 3 of sci is a new index
        int index = sci / 3 + 6;
        if (index >= 0 && index < prefixes.Length)
        {
            // with prefix
            return string.Format("{0}{1:0." + fmt + "}{2}",
                sign < 0 ? "-" : string.Empty,
                Math.Round(x, decimals),
                prefixes[index]);
        }
        // with 10^exp format
        return string.Format("{0}{1:0." + fmt + "}·10^{2}",
            sign < 0 ? "-" : string.Empty,
            Math.Round(x, decimals),
            sci);
    }
}