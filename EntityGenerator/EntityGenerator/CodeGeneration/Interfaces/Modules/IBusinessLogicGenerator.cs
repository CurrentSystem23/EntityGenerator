using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System.Text;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  public interface IBusinessLogicGenerator
  {
    void BuildInterfaceHeader(ProfileDto profile, Schema schema);

    void BuildTableInterfaceMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType);
    void BuildFunctionInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, Function function, MethodType methodType);
    void BuildTableValuedFunctionInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction, MethodType methodType);
    void BuildViewInterfaceMethod(StringBuilder sb, ProfileDto profile, Schema schema, View view, MethodType methodType);

    void BuildClassHeader(ProfileDto profile, Schema schema);
 
    void BuildTableClassMethod(ProfileDto profile, Schema schema, Table table, MethodType methodType);
    void BuildFunctionClassMethod(StringBuilder sb, ProfileDto profile, Schema schema, Function function, MethodType methodType);
    void BuildTableValuedFunctionClassMethod(StringBuilder sb, ProfileDto profile, Schema schema, TableValuedFunction tableValuedFunction, MethodType methodType);
    void BuildViewClassMethod(StringBuilder sb, ProfileDto profile, Schema schema, View view, MethodType methodType);
  }
}
