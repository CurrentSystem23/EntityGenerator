using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages
{
  public abstract class LanguageBase
  {
    /// <summary>
    /// Clustered parameter object for common parameter sharing.
    /// </summary>
    protected class GeneratorParameterObject
    {
      /// <summary>
      /// Id of currently being used databse language in _dataseLanguages list.
      /// </summary>
      public int DatabaseId { get; set; }

      /// <summary>
      /// Name of currently indexed database object.
      /// </summary>
      public string Name { get; set; }

      /// <summary>
      /// Parent schema.
      /// </summary>
      public Schema Schema { get; set; }

      /// <summary>
      /// Toggle on whether to use a- or synchronous function generation.
      /// </summary>
      public bool IsAsync { get; set; }

      /// <summary>
      /// Toggle if currently used database object is table or other type.
      /// </summary>
      public bool IsTable { get; set; }

      /// <summary>
      /// Actively generated CRUD method type.
      /// </summary>
      public MethodType MethodType { get; set; }

      /// <summary>
      /// List of parameters in indexed database object.
      /// </summary>
      public List<Column> Parameters { get; set; }

      /// <summary>
      /// List of columns in indexed database object.
      /// </summary>
      public List<Column> Columns { get; set; }

      /// <summary>
      /// External DAO method signature names.
      /// </summary>
      public List<string> ExternalMethodSignatures { get; set; }

      /// <summary>
      /// Internal DAO method signature names.
      /// </summary>
      public List<string> InternalMethodSignatures { get; set; }

      /// <summary>
      /// DAO name to be used in generated output.
      /// </summary>
      public string DaoName { get => TypeHelper.GetDaoType(Name, IsTable); }

      /// <summary>
      /// DTO name to be used in generated output.
      /// </summary>
      public string DtoName { get => TypeHelper.GetDtoType(Name, IsTable, (MethodType == MethodType.HIST_GET)); }
    }

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

    public LanguageBase(StringBuilder sb, ProfileDto profile)
    {
      _sb = sb;
      _profile = profile;
    }

    /// <summary>
    /// Get map source data type from column attribute value.
    /// </summary>
    /// <param name="column"></param>
    /// <returns></returns>
    public abstract string GetColumnDataType(Column column);
  }
}
