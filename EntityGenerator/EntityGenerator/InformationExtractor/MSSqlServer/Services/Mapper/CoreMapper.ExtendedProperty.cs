using EntityGenerator.Core.Models;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services.Mapper;

public static partial class CoreMapper
{
  /// <summary>
  /// Maps all <see cref="ICollection&lt;ExtendedTablePropertyDto&gt;"/> and all subnodes into a <see cref="TablelOrView"/>.
  /// </summary>
  /// <param name="tableOrView">The given <see cref="TablelOrView"/></param>
  /// <param name="parentObject">The given <see cref="DatabaseObjectDto"/></param>
  /// <param name="extendedTableProperties">The given <see cref="ICollection&lt;ExtendedTablePropertyDto&gt;"/></param>
  private static void MapExtendedProperty(
    TablelOrView tableOrView,
    DatabaseObjectDto parentObject,
    ICollection<ExtendedTablePropertyDto> extendedTableProperties
  )
  {
    if (parentObject != null && extendedTableProperties != null)
    {
      foreach (ExtendedTablePropertyDto extendedTablePropertyDto in extendedTableProperties.Where(w =>
                 w.DatabaseName.Equals(parentObject.DatabaseName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.SchemaName.Equals(parentObject.SchemaName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.TableName.Equals(parentObject.ObjectName, StringComparison.InvariantCultureIgnoreCase)))
      {
        tableOrView.ExtendedProperties.Add(MapCMapExtendedProperty(extendedTablePropertyDto));
      }
    }
  }

  /// <summary>
  /// Maps all <see cref="ICollection&lt;ExtendedTablePropertyDto&gt;"/> and all subnodes into a <see cref="Column"/>.
  /// </summary>
  /// <param name="column">The given <see cref="Column"/></param>
  /// <param name="parentObject">The given <see cref="DatabaseObjectDto"/></param>
  /// <param name="extendedColumnProperties">The given <see cref="ICollection&lt;ExtendedColumnPropertyDto&gt;"/></param>
  private static void MapExtendedProperty(
    Column column,
    DatabaseObjectDto parentObject,
    ICollection<ExtendedColumnPropertyDto> extendedColumnProperties
  )
  {
    if (parentObject != null && extendedColumnProperties != null)
    {
      foreach (ExtendedColumnPropertyDto extendedColumnPropertyDto in extendedColumnProperties.Where(w =>
                 w.DatabaseName.Equals(parentObject.DatabaseName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.SchemaName.Equals(parentObject.SchemaName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.TableName.Equals(parentObject.ObjectName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.ColumnName.Equals(column.Name, StringComparison.InvariantCultureIgnoreCase)))
      {
        column.ExtendedProperties.Add(MapCMapExtendedProperty(extendedColumnPropertyDto));
      }
    }
  }

  /// <summary>
  /// Maps all <see cref="ExtendedTablePropertyDto"/> into a <see cref="ExtendedProperty"/>.
  /// </summary>
  /// <param name="extendedTablePropertyDto">The given <see cref="ExtendedTablePropertyDto"/></param>
  /// <returns>A <see cref="ExtendedProperty"/> structure.</returns>
  private static ExtendedProperty MapCMapExtendedProperty(ExtendedTablePropertyDto extendedTablePropertyDto)
  {
    ExtendedProperty extendedProperty = new()
    {
      Name = extendedTablePropertyDto.ObjectName,
      Id = extendedTablePropertyDto.ObjectId,
      MinorId = 0,
      Value = extendedTablePropertyDto.ExtendedPropertyValue,
    };

    return extendedProperty;
  }

  /// <summary>
  /// Maps all <see cref="ExtendedTablePropertyDto"/> into a <see cref="ExtendedProperty"/>.
  /// </summary>
  /// <param name="extendedColumnPropertyDto">The given <see cref="ExtendedColumnPropertyDto"/></param>
  /// <returns>A <see cref="ExtendedProperty"/> structure.</returns>
  private static ExtendedProperty MapCMapExtendedProperty(ExtendedColumnPropertyDto extendedColumnPropertyDto)
  {
    ExtendedProperty extendedProperty = new()
    {
      Name = extendedColumnPropertyDto.ObjectName,
      Id = extendedColumnPropertyDto.ObjectId,
      MinorId = extendedColumnPropertyDto.ObjectMinorId,
      Value = extendedColumnPropertyDto.ExtendedPropertyValue,
    };

    return extendedProperty;
  }

}

