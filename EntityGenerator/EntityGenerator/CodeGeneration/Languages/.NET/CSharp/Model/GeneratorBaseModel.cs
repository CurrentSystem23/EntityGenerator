using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp.Model
{
  internal class GeneratorBaseModel : BaseModel
  {
    public List<Column> Columns { get; } = new();

    public List<Column> Parameters { get; }

    public DataTypes ReturnType { get; }

    public List<Column> ReturnTable { get; }

    public GeneratorBaseModel(BaseModel baseModel)
    {
      foreach (PropertyInfo pi in baseModel.GetType().GetProperties())
      {
        PropertyInfo targetProp = this.GetType().GetProperty(pi.Name);
        targetProp?.SetValue(this, pi.GetValue(baseModel, null), null);
      }
    }
  }
}
