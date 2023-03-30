using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace EntityGenerator.Core.Extensions
{
  public static class StringExtensions
  {
    public static string TransformToCharOrDigitOnly(this string s)
    {
      return s.TransformToCharOrDigitOrSpace().Replace(" ", string.Empty);
    }

    public static string TransformToCharOrDigitOnlyCamelCase(this string s)
    {
      string text = string.Empty;
      for (int i = 0; i < s.Length; i++)
      {
        char c = s[i];
        text = ((!c.ToString().All(new Func<char, bool>(char.IsLetterOrDigit))) ? (text + " ") : (text + c));
      }

      text = text.Replace("Ä", "Ae");
      text = text.Replace("ä", "ae");
      text = text.Replace("Ö", "Oe");
      text = text.Replace("ö", "oe");
      text = text.Replace("Ü", "Ue");
      text = text.Replace("ü", "ue");
      text = text.Replace("ß", "ss");
      text = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
      return text.Replace(" ", string.Empty);
    }

    public static string TransformToCharOrDigitOrSpace(this string s)
    {
      string text = string.Empty;
      for (int i = 0; i < s.Length; i++)
      {
        char c = s[i];
        text = ((!c.ToString().All(new Func<char, bool>(char.IsLetterOrDigit))) ? (text + " ") : (text + c));
      }

      text = text.Replace("Ä", "Ae");
      text = text.Replace("ä", "ae");
      text = text.Replace("Ö", "Oe");
      text = text.Replace("ö", "oe");
      text = text.Replace("Ü", "Ue");
      text = text.Replace("ü", "ue");
      text = text.Replace("ß", "ss");
      return text.RemoveDoubleSpaceCharacters().Trim();
    }

    public static string RemoveDoubleSpaceCharacters(this string text)
    {
      return Regex.Replace(text, "[ ]+", " ");
    }

    public static bool IsNullOrEmpty(this string s)
    {
      return s?.Trim().Equals(string.Empty) ?? false;
    }

    public static string ToMultilineHtml(this string value)
    {
      return new Regex("(\\r\\n|\\r|\\n)+").Replace(value, "<br />");
    }

    public static string ToParagraphHtml(this string value)
    {
      Regex regex = new Regex("(\\r\\n|\\r|\\n)+");
      return "<p>" + regex.Replace(value, "</p><p>") + "</p>";
    }

    public static string FirstCharToUpper(this string input)
    {
      if (input != null)
      {
        if (input == "")
        {
          throw new ArgumentException("input cannot be empty", "input");
        }

        return input.First().ToString().ToUpper() + input.Substring(1);
      }

      throw new ArgumentNullException("input");
    }
  }
}
