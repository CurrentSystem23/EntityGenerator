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
    StringBuilder BuildBaseFile(ProfileGeneratorDto profile);
    
    StringBuilder BuildTableDAOHeader(ProfileGeneratorDto profile, Schema schema, Table table);
    StringBuilder BuildFunctionDAOHeader(ProfileGeneratorDto profile, Schema schema, Function function);
    StringBuilder BuildTableValueFunctionDAOHeader(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction);
    StringBuilder BuildViewDAOHeader(ProfileGeneratorDto profile, Schema schema, View view);

    StringBuilder BuildTableDAOMethod(ProfileGeneratorDto profile, Schema schema, Table table, MethodType methodType);
    StringBuilder BuildFunctionDAOMethod(ProfileGeneratorDto profile, Schema schema, Function function, MethodType methodType);
    StringBuilder BuildTableValueFunctionDAOMethod(ProfileGeneratorDto profile, Schema schema, TableValueFunction tableValueFunction, MethodType methodType);
    StringBuilder BuildViewDAOMethod(ProfileGeneratorDto profile, Schema schema, View view, MethodType methodType);
  }
}
