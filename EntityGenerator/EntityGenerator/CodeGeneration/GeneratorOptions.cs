using EntityGenerator.CodeGeneration.Interfaces;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration
{
  internal class GeneratorOptions
  {
    public ProfileDto Profile { get; set; }
    public Schema Schema { get; set; }
    public MethodType MethodType { get; set; }
    public string Name { get; set; }
    public bool IsTable { get; set; }
    public bool Async { get; set; }
    public string ParametersStr { get; set; } = null;
    public string ParametersWithTypeStr { get; set; } = null;
  }
}
