using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.Languages.CodeBuilder
{
  public class FileWriter : IFileWriter
  {
    public FileWriter()
    { }

    public void WriteToFile(string path, string fileName, string content)
    {
      var filePath = path;

      if (!(filePath.EndsWith('/') || filePath.EndsWith(@"\")))
      {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
          filePath += @"\";
        else
          filePath += "/";
      }

      filePath += fileName;

      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);

      if (File.Exists(filePath))
        File.Delete(filePath);

      using var sw = new StreamWriter(filePath);
      sw.WriteLine(content);
    }
  }
}
