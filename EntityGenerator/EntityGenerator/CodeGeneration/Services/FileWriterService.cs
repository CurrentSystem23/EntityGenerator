using EntityGenerator.CodeGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Services
{
  public class FileWriterService : IFileWriterService
  {
    public FileWriterService() { }
    public void WriteToFile(string path, string fileName, string data)
    {
      string filePath = path;

      if (!(filePath.EndsWith('/') || filePath.EndsWith(@"\")))
      {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
          filePath += @"\";
        else
          filePath += "/";
      }

      filePath += fileName;

      try
      {
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);

        if (File.Exists(filePath))
          File.Delete(filePath);

        using StreamWriter sw = new StreamWriter(filePath);
        sw.Write(data);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error: " + ex.Message);
        throw;
      }
    }
  }
}
