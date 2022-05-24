using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GegaGamez.WebUI.PageHelpers;

public static class Validations
{
    /// <summary>
    /// Removes validation entries that do not start with <paramref name="entryKeys"/>
    /// </summary>
    /// <param name="page"></param>
    /// <param name="entryKeys"></param>
    public static void ValidateOnly(this PageModel page, string [] entryKeys) {

        foreach (var entry in page.ModelState)
        {
            if (entryKeys.Any(k => entry.Key.StartsWith(k)) == false)
                page.ModelState.Remove(entry.Key);
        }
    }
}
