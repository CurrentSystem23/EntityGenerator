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
    void BuildDependencyInjections(ProfileDto profile, Database db);
    void BuildBaseFile(ProfileDto profile);
    
    void BuildTableDAOHeader(ProfileDto profile, Schema schema, Table table);
    void BuildFunctionDAOHeader(ProfileDto profile, Schema schema, Function function);
    void BuildTableValuedFunctionDAOHeader(ProfileDto profile, Schema schema, Function tableValuedFunction);
    void BuildViewDAOHeader(ProfileDto profile, Schema schema, View view);

    void BuildTableDAOMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType);
    void BuildFunctionDAOMethod(ProfileDto profile, Schema schema, Function function, MethodType methodType);
    void BuildTableValuedFunctionDAOMethod(ProfileDto profile, Schema schema, Function tableValuedFunction, MethodType methodType);
    void BuildViewDAOMethod(ProfileDto profile, Schema schema, View view, MethodType methodType);
  }
}
