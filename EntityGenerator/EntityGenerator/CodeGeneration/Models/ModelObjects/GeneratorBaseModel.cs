using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EntityGenerator.CodeGeneration.Models.ModelObjects
{
  public class GeneratorBaseModel
  {
    public GeneratorBaseModel(BaseModel baseModel, Schema schema)
    {
      Schema = schema;

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
      List<PropertyInfo> propertiesDest = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(prop => prop.CanWrite).ToList();

      foreach (PropertyInfo propDest in propertiesDest)
      {
        List<PropertyInfo> prop = propertiesSrc.Where(propSrc => propSrc.Name == propDest.Name).ToList();
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

}
