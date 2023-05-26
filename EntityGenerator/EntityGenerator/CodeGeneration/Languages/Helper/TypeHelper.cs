using EntityGenerator.Core.Models.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages.Helper
{
  public abstract class TypeHelper
  {
    public static string GetDaoInterface(string name, bool isTable)
    {
      string daoName = $"I{GetDaoType(name, isTable)}";
      return isTable ? daoName : $"{daoName}V";
    }

    public static string GetDaoType(string name, bool isTable)
    {
      string daoName = $"{name}Dao";
      return isTable ? daoName : $"{daoName}V";
    }

    public static string GetDtoType(string name, bool isTable, bool isHistDto)
    {
      if (!isTable && isHistDto)
      {
        throw new Exception($"Error: Only table objects can have HistDTOs: {name}");
      }

      string dtoName = $"{name}{(isHistDto ? "Hist" : "")}Dto";
      return isTable ? dtoName : $"{dtoName}V";
    }
  }
}
