using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using Nelibur.ObjectMapper;
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
    public enum DbObjectType
    {
      TABLE,
      FUNCTION,
      VIEW,
      TABLEVALUEFUNCTION
    }
    /// <summary>
    /// Clustered parameter object for common parameter sharing.
    /// </summary>
    protected class GeneratorBaseModel
    {
      public GeneratorBaseModel(BaseModel baseModel, Schema schema)
      {
        this.Schema = schema;

        Type baseType = baseModel.GetType();
        
        switch (baseType)
        {
          case Type tableType when tableType == typeof(Table):
            DbObjectType = DbObjectType.TABLE;
            break;
          case Type functionType when functionType == typeof(Function):
            if (((Function)baseModel).FunctionType is "InLineTableFunction" or "TableFunction")
            {
              DbObjectType = DbObjectType.TABLEVALUEFUNCTION;
            }
            else
            {
              DbObjectType = DbObjectType.FUNCTION;
            }
            break;
          case Type viewType when viewType == typeof(View):
            DbObjectType = DbObjectType.VIEW;
            break;
          default:
            throw new NotSupportedException();
        }
 
        List<PropertyInfo> propertiesSrc = baseType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(prop => prop.CanRead).ToList();
        List<PropertyInfo> propertiesDest = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(prop => prop.CanWrite).ToList();

        foreach (PropertyInfo propDest in propertiesDest)
        {
          List<PropertyInfo> prop = propertiesSrc.Where(propSrc => propSrc.Name == propDest.Name && propDest.PropertyType.IsAssignableFrom(propDest.GetType())).ToList();
          if (prop.Count > 1)
          {
            throw new Exception($"Error: Multiple property matches in {baseModel.GetType()}");
          }
          foreach (PropertyInfo property in prop)
          {
            propDest.SetValue(this, property.GetValue(baseModel));
          }
        }
      }
      /// <summary>
      /// Name of currently indexed database object.
      /// </summary>
      public string Name { get; private set; }

      /// <summary>
      /// Parent schema.
      /// </summary>
      public Schema Schema { get; private set; }
      
      /// <summary>
      /// Database object type.
      /// </summary>
      public DbObjectType DbObjectType { get; private set; }

      /// <summary>
      /// List of parameters in indexed database object.  
      /// </summary>
      public List<Column> Parameters { get; set; }

      /// <summary>
      /// List of columns in indexed database object.
      /// </summary>
      public List<Column> Columns { get; set; }

      /// <summary>
      /// DAO name to be used in generated output.
      /// </summary>
      public string DaoName => DbObjectType switch
      {
        DbObjectType.TABLE => $"{Name}Dao",
        DbObjectType.FUNCTION => $"{Name}DaoS",
        DbObjectType.VIEW => $"{Name}DaoV",
        DbObjectType.TABLEVALUEFUNCTION => $"{Name}DaoV",
        _ => throw new NotSupportedException(),
      };

      public string InternalDaoName => DbObjectType switch
      {
        DbObjectType.TABLE => $"{Name}InternalDao",
        DbObjectType.FUNCTION => $"{Name}InternalDaoS",
        DbObjectType.VIEW => $"{Name}InternalDaoV",
        DbObjectType.TABLEVALUEFUNCTION => $"{Name}InternalDaoV",
        _ => throw new NotSupportedException(),
      };

      /// <summary>
      /// DTO name to be used in generated output.
      /// </summary>
      public string DtoName => DbObjectType switch
      {
        DbObjectType.TABLE => $"{Name}Dto",
        DbObjectType.VIEW => $"{Name}DtoV",
        DbObjectType.TABLEVALUEFUNCTION => $"{Name}DtoV",
        _ => throw new NotSupportedException(),
      };

      /// <summary>
      /// HistDTO name for table objects.
      /// </summary>
      public string HistDtoName => DbObjectType switch
      {
        DbObjectType.TABLE => $"{Name}HistDto",
        _ => throw new NotSupportedException(),
      };

      /// <summary>
      /// External DAO method signature names. Boolean is asynchronity toggle.
      /// </summary>
      public Dictionary<(MethodType, bool), List<string>> ExternalMethodSignatures { get; private set; }

      /// <summary>
      /// Internal DAO method signature names. Boolean is asynchronity toggle.
      /// </summary>
      public Dictionary<(MethodType, bool), List<string>> InternalMethodSignatures { get; private set; }
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
