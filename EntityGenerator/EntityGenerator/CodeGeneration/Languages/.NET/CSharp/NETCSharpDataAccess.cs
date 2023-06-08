using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : IDataAccessGenerator
  {
    void IDataAccessGenerator.BuildBaseFile(ProfileDto profile)
    {
      BuildImports(new List<string> { "System", "System.Collections.Generic", "Microsoft.Data.SqlClient", "System.Text", $"{profile.Global.ProjectName}.Common.DTOs" });
      BuildNameSpace($"{profile.Global.ProjectName}.DataAccess.SQL.BaseClasses");

      OpenClass("Dao", isPartial: true);
    }

    void IDataAccessGenerator.BuildDependencyInjections(ProfileDto profile, Database db)
    {
      List<string> imports = new()
      {
        "Microsoft.Extensions.DependencyInjection"
      };

      foreach (Schema schema in db.Schemas)
      {
        imports.Add($"{profile.Global.ProjectName}.DataAccess.{db.Name}.{schema.Name}");
      }
      BuildImports(imports);
      BuildNameSpace($"{profile.Global.ProjectName}.DataAccess.{db.Name}.Helper");

      OpenClass("DataAccessAdoInitializer", null, false, true);
      OpenMethod("InitializeGenerated(IServiceCollection services)", accessModifier: Enums.AccessType.PRIVATE);

      foreach (Schema schema in db.Schemas)
      {
        foreach (Table table in schema.Tables)
        {
          _sb.AppendLine($"services.AddTransient<{profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{table.Name}Dao, {profile.Global.ProjectName}.DataAccess.{db.Name}.{schema.Name}.{table.Name}Dao>();");
          _sb.AppendLine($"services.AddTransient<{profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{table.Name}InternalDao, {profile.Global.ProjectName}.DataAccess.{db.Name}.{schema.Name}.{table.Name}Dao>();");
        }
        foreach (Function function in schema.FunctionsScalar)
        {
          _sb.AppendLine($"services.AddTransient<{profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{function.Name}DaoS, {profile.Global.ProjectName}.DataAccess.{db.Name}.{schema.Name}.{function.Name}DaoS>();");
          _sb.AppendLine($"services.AddTransient<{profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{function.Name}InternalDaoS, {profile.Global.ProjectName}.DataAccess.{db.Name}.{schema.Name}.{function.Name}DaoS>();");
        }
        foreach (BaseModel baseModel in schema.FunctionsTableValued.Cast<BaseModel>().Concat(schema.Views))
        {
          _sb.AppendLine($"services.AddTransient<{profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{baseModel.Name}DaoV, {profile.Global.ProjectName}.DataAccess.{db.Name}.{schema.Name}.{baseModel.Name}DaoV>();");
          _sb.AppendLine($"services.AddTransient<{profile.Global.ProjectName}.Common.DataAccess.Interfaces.Ado.{schema.Name}.I{baseModel.Name}InternalDaoV, {profile.Global.ProjectName}.DataAccess.{db.Name}.{schema.Name}.{baseModel.Name}DaoV>();");
        }
      }
    }

    void IDataAccessGenerator.BuildFunctionDAOHeader(ProfileDto profile, Schema schema, Function function)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildFunctionDAOMethod(ProfileDto profile, Schema schema, Function function, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildTableDAOHeader(ProfileDto profile, Schema schema, Table table)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildTableDAOMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildTableValuedFunctionDAOHeader(ProfileDto profile, Schema schema, Function tableValuedFunction)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildTableValuedFunctionDAOMethod(ProfileDto profile, Schema schema, Function tableValuedFunction, MethodType methodType)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildViewDAOHeader(ProfileDto profile, Schema schema, View view)
    {
      throw new NotImplementedException();
    }

    void IDataAccessGenerator.BuildViewDAOMethod(ProfileDto profile, Schema schema, View view, MethodType methodType)
    {
      throw new NotImplementedException();
    }
  }
}
