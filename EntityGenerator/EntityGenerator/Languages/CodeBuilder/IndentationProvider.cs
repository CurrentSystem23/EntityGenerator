using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EntityGenerator.Languages.CodeBuilder
{
  public class IndentationProvider : IIndentationProvider
  {
    public int indentSize { get; set; }

    private string Indent(int count)
    {
      return "".PadLeft(count * indentSize);
    }

    public StringBuilder ApplyIndentation(StringBuilder sb, bool openNamespace = false)
    {
      string[] delim = { Environment.NewLine, "\n" }; // "\n" added in case you manually appended a newline
      string[] lines = sb.ToString().Split(delim, StringSplitOptions.None);
      var currentIndent = 0;

      Regex rxOpenAndCloseBrackets = new Regex(@"{.*}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
      Regex rxOpenBracket = new Regex(@"{", RegexOptions.Compiled | RegexOptions.IgnoreCase);
      Regex rxClosedBracket = new Regex(@"}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

      Regex rxOpenLineEnd = new Regex(@".*[^;]$");
      Regex rxSingleQuote = new Regex(@$"^[^""]*""[^""]*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

      // new var needed?
      var sbNew = new StringBuilder();

      var isMultiLine = false;
      var isInsideQuote = false;

      // only true if trimmable whitespace characters are present
      var appliedTrim = false;

      foreach (string line in lines)
      {
        string lineTrimmed;

        if (isMultiLine && ((appliedTrim && line.TrimStart() == "{") || (!appliedTrim && line == "{")))
        {
          isMultiLine = false;
        }

        if (isMultiLine)
        {
          if (isInsideQuote)
          {
            // only if appliedTrim is in use
            lineTrimmed = line;
          }
          else
          {
            lineTrimmed = line.TrimStart();
          }

          if (rxSingleQuote.IsMatch(lineTrimmed) && !rxOpenLineEnd.IsMatch(lineTrimmed) && isInsideQuote)
          {
            isMultiLine = false;
            isInsideQuote = false;
            sbNew.AppendLine(Indent(currentIndent) + lineTrimmed);
            continue;
          }
          else if (!rxOpenLineEnd.IsMatch(lineTrimmed))
          {
            if (!isInsideQuote)
            {
              isMultiLine = false;
            }
          }

          sbNew.AppendLine(Indent(currentIndent + 1) + lineTrimmed);

          continue;
        }
        else if (line.StartsWith(" "))
        {
          // Delete all spaces in front of first char if not inside a multiline command
          appliedTrim = true;
          lineTrimmed = line.TrimStart();
        }
        else
        {
          appliedTrim = false;
          lineTrimmed = line;
        }

        // catch special case
        if (lineTrimmed == "}")
        {
          currentIndent--;
          sbNew.AppendLine(Indent(currentIndent) + lineTrimmed);
          continue;
        }

        // Write line with current indent configuration
        sbNew.AppendLine(Indent(currentIndent) + lineTrimmed);

        // Calculate indent for next iteration
        // Curly brackets opening and closing in same line --> no indent increase
        if (rxOpenAndCloseBrackets.IsMatch(lineTrimmed))
        {
          // ignore other cases
        }

        // Curly bracket opening --> increase indent
        else if (rxOpenBracket.IsMatch(lineTrimmed))
        {
          currentIndent++;
        }

        // Curly bracket closing --> decrease indent
        else if (rxClosedBracket.IsMatch(lineTrimmed))
        {
          currentIndent--;
        }

        // Start of multiline command detected
        else if (rxOpenLineEnd.IsMatch(lineTrimmed) && currentIndent > (openNamespace ? 1 : 0) + 1)
        {
          // special case for statements starting with #
          if (lineTrimmed.StartsWith("#"))
            continue;

          isMultiLine = true;

          // Multiline string detected
          if (rxSingleQuote.IsMatch(lineTrimmed))
          {
            isInsideQuote = true;
          }
        }
      }
      return sbNew;
    }
  }
}
