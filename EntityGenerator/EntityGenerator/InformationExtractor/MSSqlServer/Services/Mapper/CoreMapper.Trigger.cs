using EntityGenerator.Core.Models;
using EntityGenerator.InformationExtractor.MSSqlServer.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.InformationExtractor.MSSqlServer.Services.Mapper;

public static partial class CoreMapper
{
  /// <summary>
  /// Maps all <see cref="ICollection&lt;TriggerDto&gt;"/> and all subnodes into a <see cref="Schema"/>.
  /// </summary>
  /// <param name="schema">The given <see cref="Schema"/></param>
  /// <param name="triggerDtos">The given <see cref="ICollection&lt;TriggerDto&gt;"/></param>
  private static void MapTrigger(
    Schema schema,
    ICollection<TriggerDto> triggerDtos
    )
  {
    if (triggerDtos != null)
    {
      // nur die ungebundenen Schematrigger bearbeiten!
      foreach (TriggerDto triggerDto in triggerDtos.Where(w =>
                 w.ParentClass == 0))
      {
        schema.Triggers.Add(MapTrigger(triggerDto));
      }
    }
  }

  /// <summary>
  /// Maps all <see cref="ICollection&lt;TriggerDto&gt;"/> and all subnodes into a <see cref="Table"/>.
  /// </summary>
  /// <param name="table">The given <see cref="Table"/></param>
  /// <param name="parentObject">The given <see cref="DatabaseObjectDto"/></param>
  /// <param name="triggerDtos">The given <see cref="ICollection&lt;TriggerDto&gt;"/></param>
  private static void MapTrigger(
    Table table,
    DatabaseObjectDto parentObject,
    ICollection<TriggerDto> triggerDtos
  )
  {
    if (parentObject != null && triggerDtos != null)
    {
      // nur über die Tabellentrigger laufen!
      foreach (TriggerDto triggerDto in triggerDtos.Where(w =>
                 w.DatabaseName.Equals(parentObject.DatabaseName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.SchemaName.Equals(parentObject.SchemaName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.TableName.Equals(parentObject.ObjectName, StringComparison.InvariantCultureIgnoreCase) &&
                 w.ParentClass > 0))
      {
        table.Triggers.Add(MapTrigger(triggerDto));
      }
    }
  }

  /// <summary>
  /// Maps all <see cref="TriggerDto"/> and all subnodes into a <see cref="Trigger"/>.
  /// </summary>
  /// <param name="triggerDto">The given <see cref="TriggerDto"/></param>
  /// <returns>A <see cref="Trigger"/> with the core trigger structure.</returns>
  private static Trigger MapTrigger(TriggerDto triggerDto)
  {
    Trigger trigger = new()
    {
      Name = triggerDto.ObjectName,
      Id = triggerDto.ObjectId,
      IsMsShipped = triggerDto.IsMsShipped,
      IsDisabled = triggerDto.IsDisabled,
      IsNotForReplication = triggerDto.IsNotForReplication,
      IsInsteadOfTrigger = triggerDto.IsInsteadOfTrigger,
      TriggerDefinition = triggerDto.TriggerDefinition,
    };

    return trigger;
  }
}

