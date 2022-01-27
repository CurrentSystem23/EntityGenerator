using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Languages.CodeBuilder
{
  public enum AccessType
  {
    DEFAULT = 0,
    PUBLIC = 1,
    PROTECTED = 2,
    PRIVATE = 3,
    INTERNAL = 4,
    PROTECTED_INTERNAL = 5,
    PROTECTED_PRIVATE = 6
  }

  public class CodeBuilder : CodeBuilderBase<CodeBuilder>, ICodeBuilder
  {
    private bool _openNameSpace;

    public LanguageBase languageBase { get; private set; }
    public IIndentationProvider indentationProvider { get; private set; }

    public CodeBuilder(LanguageBase languageBase, IIndentationProvider indentationProvider)
      : base(new StringBuilder())
    {
      this.languageBase = languageBase;
      this.indentationProvider = indentationProvider;
    }

    public void SetLanguageBase(LanguageBase languageBase)
    {
      this.languageBase = languageBase;
    }

    public void SetIndentationProvider(IIndentationProvider indentationProvider)
    {
      this.indentationProvider = indentationProvider;
    }

    public string Close()
    {
      if (_openNameSpace) _sb.AppendLine("}");

      indentationProvider.ApplyIndentation(_sb, _openNameSpace);

      if (languageBase == null)
      {
        return languageBase.Translate(_sb);
      }
      else
      {
        return _sb.ToString();
      }
    }

    public override string ToString()
    {
      return _sb.ToString();
    }

    public CodeBuilder BuildImports(List<string> imports)
    {
      foreach (var import in imports)
      {
        _sb.AppendLine($"using {import};");
      }
      _sb.AppendLine();

      return this;
    }

    public CodeBuilder BuildNameSpace(string nmspace)
    {
      if (_openNameSpace) throw new Exception("Namespace already open.");

      _sb.AppendLine($"namespace {nmspace}");
      _sb.AppendLine("{");

      _openNameSpace = true;

      return this;
    }

    public StructureBuilder BuildClass(string className, string baseClass = null, bool isStatic = false, bool isPartial = false, bool isAbstract = false, AccessType accessModifier = AccessType.PUBLIC)
    {
      var classBuilder = new StructureBuilder(_sb, className, baseClass, StructureType.CLASS, isStatic, isPartial, isAbstract, accessModifier);

      return classBuilder;
    }

    public StructureBuilder BuildInterface(string interfaceName, string baseInterface = null, bool isPartial = false, AccessType accessModifier = AccessType.PUBLIC)
    {
      return new StructureBuilder(_sb, interfaceName, baseInterface, StructureType.INTERFACE, false, isPartial, false, accessModifier);
    }

    public StructureBuilder BuildEnum(string enumName, bool isPartial = false, AccessType accessModifier = AccessType.PUBLIC)
    {
      return new StructureBuilder(_sb, enumName, null, StructureType.ENUM, false, isPartial, false, accessModifier);
    }
  }
}
