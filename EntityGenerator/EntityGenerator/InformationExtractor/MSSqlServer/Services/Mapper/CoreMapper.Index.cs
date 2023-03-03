using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services.Mapper;

public static partial class CoreMapper
{
  /// <summary>
  /// Maps all <see cref="ICollection&lt;IndexDto&gt;"/> into a <see cref="TableOrView"/>.
  /// </summary>
  /// <param name="tableOrView">The given <see cref="TableOrView"/></param>
  /// <param name="parentObjectDto">The given <see cref="DatabaseObjectDto"/></param>
  /// <param name="indexDtos">The given <see cref="ICollection&lt;IndexDto&gt;"/></param>
  private static void MapIndex(
    TableOrView tableOrView,
    DatabaseObjectDto parentObjectDto,
    ICollection<IndexDto> indexDtos
    )
  {
    if (parentObjectDto != null && indexDtos != null)
    {
      foreach (IndexDto indexDto in indexDtos.Where(w =>
                 w.DatabaseName.Equals(parentObjectDto.DatabaseName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.SchemaName.Equals(parentObjectDto.SchemaName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.TableName.Equals(parentObjectDto.ObjectName, StringComparison.InvariantCultureIgnoreCase)))
      {
        tableOrView.Indexes.Add(MapIndex(indexDto, tableOrView));
      }
    }
  }

  /// <summary>
  /// Maps all <see cref="IndexDto"/> into a <see cref="Core.Models.ModelObjects.Index"/>.
  /// </summary>
  /// <param name="indexDto">The given <see cref="IndexDto"/></param>
  /// <param name="tableOrView">The given <see cref="TableOrView"/></param>
  /// <returns>A <see cref="Database"/> with the core database structure.</returns>
  private static Core.Models.ModelObjects.Index MapIndex(IndexDto indexDto, TableOrView tableOrView)
  {
    Core.Models.ModelObjects.Index index = new()
    {
      Name = indexDto.ObjectName,
      Id = indexDto.ObjectId,
      IndexTypeId = indexDto.IndexTypeId,
      IsUnique = indexDto.IsUnique,
      ObjectType = indexDto.ObjectType,
    };

    index.ColumnsIndexed.AddRange(MapReferencedColumns(indexDto.IndexColumns, tableOrView));
    index.ColumnsIncluded.AddRange(MapReferencedColumns(indexDto.IncludedColumns, tableOrView));

    return index;
  }

  /// <summary>
  /// Maps all <see cref="string"/> column names information into a <see cref="ICollection&lt;Column&gt;"/>.
  /// </summary>
  /// <param name="colNames">The given <see cref="string"/></param>
  /// <param name="tableOrView">The given <see cref="TableOrView"/></param>
  /// <returns>A <see cref="ICollection&lt;Column&gt;"/> with the columns structure.</returns>
  private static ICollection<Column> MapReferencedColumns(string colNames, TableOrView tableOrView)
  {
    List<Column> columns = new();
    string[] columnNames = colNames.Split(',');
    foreach (string columnName in columnNames)
    {
      Column column = tableOrView.Columns.FirstOrDefault(s => s.Name.Equals(columnName.Trim(), StringComparison.InvariantCultureIgnoreCase));
      if (column != null)
      {
        columns.Add(column);
      }
    }

    return columns;
  }
}

