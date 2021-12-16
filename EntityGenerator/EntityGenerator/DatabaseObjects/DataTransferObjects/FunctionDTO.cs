using System.Collections.Generic;

namespace EntityGenerator.DatabaseObjects.DataTransferObjects
{
  /// <summary>
  /// Class <see cref="SchemaDto"/> models the data transfer object for schemas.
  /// </summary>
  public class FunctionDto : DataTransferObject
  {
    /// <summary>
    /// Constructor for <see cref="SchemaDto"/>
    /// </summary>
    public FunctionDto()
    {
      ReturnColumns = new List<TableValueFunctionReturnColumnDto>();
    }

    /// <summary>
    /// The function id of the function in the source database.
    /// </summary>
    public int FunctionId { get; set; }

    /// <summary>
    /// The function name of the function.
    /// </summary>
    public string FunctionName { get; set; }

    /// <summary>
    /// The type name of the function in the source database.
    /// </summary>
    public string TypeName { get; set; }

    /// <summary>
    /// The xtype of the function in the source database.
    /// </summary>
    public string XType { get; set; }

    /// <summary>
    /// The comma separated parameters of the function in the source database.
    /// </summary>
    public string Parameters { get; set; }

    /// <summary>
    /// The return type of the function in the source database.
    /// </summary>
    public string ReturnType { get; set; }

    /// <summary>
    /// The body of the function in the source database.
    /// </summary>
    public string FunctionBody { get; set; }

    /// <summary>
    /// The <see cref="List<TableValueFunctionReturnColumnDataTransferObject>"/> of the function in the source database.
    /// </summary>
    public List<TableValueFunctionReturnColumnDto> ReturnColumns { get; }
  }
}
