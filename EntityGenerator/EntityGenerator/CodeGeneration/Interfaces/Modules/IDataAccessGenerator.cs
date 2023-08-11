using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  public interface IDataAccessGenerator
  {
    void BuildDependencyInjectionBaseFile();
    void BuildDependencyInjections(Database db, int databaseId);
    void BuildBaseFile(int databaseId);

    void BuildTableDAOHeader(Schema schema, Table table, int databaseId);
    void BuildFunctionDAOHeader(Schema schema, Function function, int databaseId);
    void BuildTableValuedFunctionDAOHeader(Schema schema, Function tableValuedFunction, int databaseId);
    void BuildViewDAOHeader(Schema schema, View view, int databaseId);

    void BuildTableDAOMethod(Schema schema, Table table, MethodType methodType, bool isAsync, int databaseId);
    void BuildFunctionDAOMethod(Schema schema, Function function, MethodType methodType, bool isAsync, int databaseId);
    void BuildTableValuedFunctionDAOMethod(Schema schema, Function tableValuedFunction, MethodType methodType, bool isAsync, int databaseId);
    void BuildViewDAOMethod(Schema schema, View view, MethodType methodType, bool isAsync, int databaseId);
  }
}
