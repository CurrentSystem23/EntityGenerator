using EntityGenerator.Core.Models;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services.Mapper;

public static partial class CoreMapper
{
  /// <summary>
  /// Maps all <see cref="ICollection&lt;TypeDto&gt;"/> and all subnodes into a <see cref="Database"/>.
  /// </summary>
  /// <param name="database">The given <see cref="Database"/></param>
  /// <param name="typeDtos">The given <see cref="ICollection&lt;TypeDto&gt;"/></param>
  private static void MapType(
    Database database,
    ICollection<TypeDto> typeDtos
    )
  {
    if (database != null && typeDtos != null)
    {
      foreach (TypeDto typeDto in typeDtos.Where(w =>
                 w.DatabaseName.Equals(database.Name, StringComparison.InvariantCultureIgnoreCase)))
      {
        database.UsedDatabaseTypes.Add(MapType(typeDto));
      }
    }
  }

  /// <summary>
  /// Maps a <see cref="TypeDto"/> into a <see cref="DatabaseType"/>.
  /// </summary>
  /// <param name="typeDto">The given <see cref="TypeDto"/></param>
  /// <returns>A <see cref="DatabaseType"/> with the core database type information.</returns>
  private static DatabaseType MapType(TypeDto typeDto)
  {
    DatabaseType databaseType = new()
    {
      Name = typeDto.ObjectName,
      Id = typeDto.ObjectId,
      DatabaseObjects = MapDatabaseObjects(typeDto.ObjectName),
    };

    return databaseType;
  }

  /// <summary>
  /// Maps a <see cref="string"/> into a <see cref="Core.Models.Enums.DatabaseObjects"/>.
  /// </summary>
  /// <param name="typeName">The given <see cref="string"/></param>
  /// <returns>A <see cref="Core.Models.Enums.DatabaseObjects"/>.</returns>
  private static Core.Models.Enums.DatabaseObjects MapDatabaseObjects(string typeName)
  {
    if (Enum.TryParse(typeName, out Core.Models.Enums.DatabaseObjects databaseObjectType))
      return databaseObjectType;

    return Core.Models.Enums.DatabaseObjects.Unknown;
  }
}

