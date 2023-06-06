using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System.Text;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  public interface IBusinessLogicGenerator
  {
    void BuildInterfaceHeader(ProfileDto profile, Schema schema);

    void BuildTableInterfaceMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType);
    void BuildScalarFunctionInterfaceMethod(ProfileDto profile, Schema schema, Function function, MethodType methodType);
    void BuildTableValuedFunctionInterfaceMethod(ProfileDto profile, Schema schema, Function tableValuedFunction, MethodType methodType);
    void BuildViewInterfaceMethod(ProfileDto profile, Schema schema, View view, MethodType methodType);

    void BuildClassHeader(ProfileDto profile, Schema schema);
 
    void BuildTableClassMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType);
    void BuildScalarFunctionClassMethod(ProfileDto profile, Schema schema, Function function, MethodType methodType);
    void BuildTableValuedFunctionClassMethod(ProfileDto profile, Schema schema, Function tableValuedFunction, MethodType methodType);
    void BuildViewClassMethod(ProfileDto profile, Schema schema, View view, MethodType methodType);
  }
}
