using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services.Mapper;

public static partial class CoreMapper
{
  /// <summary>
  /// Maps all <see cref="ICollection&lt;DatabaseDto&gt;"/> and all subnodes into a <see cref="Database"/>.
  /// </summary>
  /// <param name="databaseDtos">The given <see cref="ICollection&lt;DatabaseDto&gt;"/></param>
  /// <returns>A <see cref="Database"/> with the core database structure.</returns>
  private static Database MapDatabase(
    ICollection<DatabaseDto> databaseDtos
  )
  {
    Database database = new();
    DatabaseDto databaseDto = databaseDtos.FirstOrDefault();
    if (databaseDto != null)
    {
      database.Name = databaseDto.ObjectName;
      database.Id = databaseDto.ObjectId;
    }
    return database;
  }
}

