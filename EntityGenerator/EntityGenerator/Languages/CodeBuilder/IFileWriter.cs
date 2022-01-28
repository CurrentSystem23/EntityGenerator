using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Languages.CodeBuilder
{
  public interface IFileWriter
  {
    public void WriteToFile(string path, string fileName, string content);
  }
}
