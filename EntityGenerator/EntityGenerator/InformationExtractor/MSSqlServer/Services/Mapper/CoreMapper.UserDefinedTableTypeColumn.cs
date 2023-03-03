using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services.Mapper;

public static partial class CoreMapper
{
  /// <summary>
  /// Maps all <see cref="ICollection&lt;UserDefinedTableTypeColumnDto&gt;"/> into a <see cref="Schema"/>.
  /// </summary>
  /// <param name="schema">The given <see cref="Schema"/></param>
  /// <param name="parentObjectDto">The given <see cref="SchemaDto"/></param>
  /// <param name="userDefinedTableTypeColumnDtos">The given <see cref="ICollection&lt;UserDefinedTableTypeColumnDto&gt;"/></param>
  private static void MapUserDefinedTableTypeColumn(
    Schema schema,
    SchemaDto parentObjectDto,
    ICollection<UserDefinedTableTypeColumnDto> userDefinedTableTypeColumnDtos
    )
  {
    if (parentObjectDto != null && userDefinedTableTypeColumnDtos != null)
    {
      foreach (UserDefinedTableTypeColumnDto userDefinedTableTypeColumnDto in userDefinedTableTypeColumnDtos.Where(w =>
                 w.DatabaseName.Equals(parentObjectDto.DatabaseName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.SchemaName.Equals(parentObjectDto.ObjectName, StringComparison.InvariantCultureIgnoreCase)))
      {
        schema.UserDefinedTableTypes.Add(MapUserDefinedTableTypeColumn(userDefinedTableTypeColumnDto));
      }
    }
  }

  /// <summary>
  /// Maps a <see cref="UserDefinedTableTypeColumnDto"/> into a <see cref="UserDefinedTableTypeColumn"/>.
  /// </summary>
  /// <param name="userDefinedTableTypeColumnDto">The given <see cref="UserDefinedTableTypeColumnDto"/></param>
  /// <returns>A <see cref="UserDefinedTableTypeColumn"/>.</returns>
  private static UserDefinedTableTypeColumn MapUserDefinedTableTypeColumn(UserDefinedTableTypeColumnDto userDefinedTableTypeColumnDto)
  {
    UserDefinedTableTypeColumn column = new()
    {
      Name = userDefinedTableTypeColumnDto.ObjectName,
      Id = userDefinedTableTypeColumnDto.ObjectId,
      Field = "",
      ColumnTypeDataType = MapToCoreDataType(userDefinedTableTypeColumnDto.ColumnTypeDataType),
      ColumnIsIdentity = userDefinedTableTypeColumnDto.ColumnIsIdentity,
      ColumnIsNullable = userDefinedTableTypeColumnDto.ColumnIsNullable,
      ColumnDefaultDefinition = userDefinedTableTypeColumnDto.ColumnDefaultDefinition,
      ColumnIsComputed = userDefinedTableTypeColumnDto.ColumnIsComputed,
      ColumnMaxLength = userDefinedTableTypeColumnDto.ColumnMaxLength,
    };

    return column;
  }
}

