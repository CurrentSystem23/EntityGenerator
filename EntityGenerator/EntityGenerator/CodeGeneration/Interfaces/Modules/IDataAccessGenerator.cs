using EntityGenerator.Core.Models;
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
    void BuildBaseFile(StringBuilder sb, ProfileGeneratorDto profile);
    
    void BuildTableDAOHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table);
    void BuildFunctionDAOHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Function function);
    void BuildTableValueFunctionDAOHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction);
    void BuildViewDAOHeader(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, View view);

    void BuildTableDAOMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType);
    void BuildFunctionDAOMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType);
    void BuildTableValueFunctionDAOMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType);
    void BuildViewDAOMethod(StringBuilder sb, ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType);
  }
}
