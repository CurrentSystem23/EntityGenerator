using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  public interface IDataAccessSQLGenerator
  {
    void BuildBaseFileExtension(int databaseId);
    void BuildWhereParameterClass(int databaseId);
    void BuildDependencyInjections(Database db, int databaseId);
    void BuildBaseFile();

    void BuildTableDAOHeader(Schema schema, Table table);
    void BuildFunctionDAOHeader(Schema schema, Function function);
    void BuildTableValuedFunctionDAOHeader(Schema schema, Function tableValuedFunction);
    void BuildViewDAOHeader(Schema schema, View view);

    void BuildTableDAOMethod(Schema schema, Table table, MethodType methodType);
    void BuildFunctionDAOMethod(Schema schema, Function function, MethodType methodType);
    void BuildTableValuedFunctionDAOMethod(Schema schema, Function tableValuedFunction, MethodType methodType);
    void BuildViewDAOMethod(Schema schema, View view, MethodType methodType);

  }
}
