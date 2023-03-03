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
    void BuildBaseFile(StringBuilder sb, ProfileDto profile);
    
    void BuildTableDAOHeader(StringBuilder sb, ProfileDto profile, Schema schema, Table table);
    void BuildFunctionDAOHeader(StringBuilder sb, ProfileDto profile, Schema schema, Function function);
    void BuildTableValuedFunctionDAOHeader(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction);
    void BuildViewDAOHeader(StringBuilder sb, ProfileDto profile, Schema schema, View view);

    void BuildTableDAOMethod(StringBuilder sb, ProfileDto profile, Schema schema, Table table, MethodType methodType);
    void BuildFunctionDAOMethod(StringBuilder sb, ProfileDto profile, Schema schema, Function function, MethodType methodType);
    void BuildTableValuedFunctionDAOMethod(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction, MethodType methodType);
    void BuildViewDAOMethod(StringBuilder sb, ProfileDto profile, Schema schema, View view, MethodType methodType);
  }
}
