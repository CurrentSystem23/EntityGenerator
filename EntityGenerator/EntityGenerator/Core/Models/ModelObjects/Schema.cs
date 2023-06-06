using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityGenerator.Core.Models.ModelObjects;

/// <summary>
/// Class <see cref="Schema"/> is the representation of schema model objects.
/// </summary>
[Serializable]
public class Schema : BaseModel
{
  /// <summary>
  /// Get or set the tables as a <see cref="List&lt;Table&gt;"/>.
  /// </summary>
  public List<Table> Tables { get; } = new();

  /// <summary>
  /// Get or set the views as a <see cref="List&lt;View&gt;"/>.
  /// </summary>
  public List<View> Views { get; } = new();

  /// <summary>
  /// Get or set the aggregate functions as a <see cref="List&lt;Function&gt;"/>.
  /// </summary>
  public List<Function> FunctionsAggregate { get; } = new();

  /// <summary>
  /// Get or set the scalar functions as a <see cref="List&lt;Function&gt;"/>.
  /// </summary>
  public List<Function> FunctionsSqlScalar { get; } = new();

  /// <summary>
  /// Get or set the sql inlined table value functions as a <see cref="List&lt;Function&gt;"/>.
  /// </summary>
  public List<Function> FunctionsSqlInlineTableValued { get; } = new();

  /// <summary>
  /// Get or set the sql table valued functions as a <see cref="List&lt;Function&gt;"/>.
  /// </summary>
  public List<Function> FunctionsSqlTableValued { get; } = new();

  /// <summary>
  /// Get or set the CLR scalar functions as a <see cref="List&lt;Function&gt;"/>.
  /// </summary>
  public List<Function> FunctionsClrScalar { get; } = new();

  /// <summary>
  /// Get or set the CLR table valued functions as a <see cref="List&lt;Function&gt;"/>.
  /// </summary>
  public List<Function> FunctionsClrTableValued { get; } = new();

  public List<Function> FunctionsScalar
  {
    get => FunctionsClrScalar
      .Concat(FunctionsClrScalar)
      .Concat(FunctionsSqlScalar)
      .ToList();
  }

  public List<Function> FunctionsTableValued
  {
    get => FunctionsClrTableValued
      .Concat(FunctionsSqlInlineTableValued)
      .Concat(FunctionsSqlTableValued)
      .ToList();
  }


  /// <summary>
  /// Get or set the user defined table type columns as a <see cref="List&lt;UserDefinedTableTypeColumn&gt;"/>.
  /// </summary>
  public List<UserDefinedTableTypeColumn> UserDefinedTableTypes { get; } = new();

  /// <summary>
  /// Get or set the triggers as a <see cref="List&lt;Trigger&gt;"/>.
  /// </summary>
  public List<Trigger> Triggers { get; } = new();
}

