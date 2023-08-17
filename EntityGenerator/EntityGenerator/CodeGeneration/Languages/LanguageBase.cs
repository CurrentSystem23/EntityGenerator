using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages
{
  public abstract class LanguageBase
  {
    /// <summary>
    /// Language name for use in namespaces or other distinctions.
    /// </summary>
    public readonly string Name;

    /// <summary>
    /// Format string representing type-value syntax being used in language.
    //// Usage:
    //// {0} -> Type
    //// {1} -> Value
    /// </summary>
    public string ParameterFormat;

    /// <summary>
    /// Active application profile.
    /// </summary>
    protected readonly ProfileDto _profile;

    /// <summary>
    /// Central StringBuilder for current file to be written.
    /// </summary>
    protected StringBuilder _sb;

    public LanguageBase(StringBuilder sb, ProfileDto profile, string name)
    {
      _sb = sb;
      _profile = profile;
      Name = name;
    }

    /// <summary>
    /// Get map source data type from column attribute value.
    /// </summary>
    /// <param name="column"></param>
    /// <returns></returns>
    public abstract string GetColumnDataType(Column column);
  }
}
