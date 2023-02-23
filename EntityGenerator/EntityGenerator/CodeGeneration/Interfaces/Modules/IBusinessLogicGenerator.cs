using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Interfaces.Modules
{
  public interface IBusinessLogicGenerator
  {
    void BuildTableInterface();
    void BuildFunctionInterface();
    void BuildTableValueFunctionInterface();
    void BuildViewInterface();

    void BuildTableClass();
    void BuildFunctionClass();
    void BuildTableValueFunctionClass();
    void BuildViewClass();
  }
}
