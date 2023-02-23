using EntityGenerator.CodeGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Services
{
  public class FileWriterService : IFileWriterService
  {
    public void Write(string data)
    {
      WriteToFile(data);
    }

    public void WriteToFile(string data)
    {
      throw new NotImplementedException();
    }
  }
}
