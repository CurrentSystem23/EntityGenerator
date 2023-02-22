using EntityGenerator.Core.Models;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services
{
  public static partial class CoreMapper
  {
    private static Database MapDatabase(
      ICollection<DatabaseDto> databases
    )
    {
      Database database = new();
      DatabaseDto databaseDto = databases.FirstOrDefault();
      if (databaseDto != null)
      {
        database.Name = databaseDto.ObjectName;
        database.Id = databaseDto.ObjectId;
      }
      return database;
    }
  }
}
