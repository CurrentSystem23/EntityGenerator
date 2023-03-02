using EntityGenerator.CodeGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public class NETCSharpFormatterService : IFormatterService
  {
    public int IndentSize { get; set; }

    public NETCSharpFormatterService(int indentSize = 2)
    {
      IndentSize = indentSize;
    }

    private string Indent(int count)
    {
      return "".PadLeft(count * IndentSize);
    }

    public void CloseFile(StringBuilder sb)
    {
      CloseAllScopes(sb);
      ApplyIndentation(sb);
    }

    public void CloseAllScopes(StringBuilder sb)
    {
      int openBrackets = sb.ToString().Count(x => x == '{');
      for (int i = sb.ToString().Count(x => x == '}'); i < openBrackets; i++)
      {
        sb.AppendLine("}");
      }
    }

    public void ApplyIndentation(StringBuilder sb)
    {
      string[] delim = { Environment.NewLine, "\n" }; // "\n" added in case you manually appended a newline
      string[] lines = sb.ToString().Split(delim, StringSplitOptions.None);
      int currentIndent = 0;

      Regex rxOpenAndCloseBrackets = new Regex(@"{.*}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
      Regex rxOpenBracket = new Regex(@"{", RegexOptions.Compiled | RegexOptions.IgnoreCase);
      Regex rxClosedBracket = new Regex(@"}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

      Regex rxOpenLineEnd = new Regex(@".*[^;]$");
      Regex rxSingleQuote = new Regex(@$"^[^""]*""[^""]*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

      StringBuilder sbNew = new StringBuilder();

      bool isMultiLine = false;
      bool isInsideQuote = false;

      // only true if trimmable whitespace characters are present
      bool appliedTrim = false;

      foreach (string line in lines)
      {
        string lineTrimmed;

        bool isInsideBlockComment = false;

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

        // if inside TypeScript block comment
        if (line.TrimStart().StartsWith('*'))
        {
          lineTrimmed = " " + lineTrimmed;
          isInsideBlockComment = true;
        }

        // Write line with current indent configuration
        sbNew.AppendLine(Indent(currentIndent) + lineTrimmed);

        // skip post-processing if inside block comment
        if (isInsideBlockComment)
          continue;

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
        else if (rxOpenLineEnd.IsMatch(lineTrimmed) && currentIndent > 1)
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
      sb = sbNew;

    }
  }
}
