using EntityGenerator.CodeGeneration.Interfaces.Modules;
using EntityGenerator.CodeGeneration.Languages.Helper;
using EntityGenerator.Core.Extensions;
using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.Profile.DataTransferObject;
using EntityGenerator.Profile.DataTransferObjects.Enums;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EntityGenerator.CodeGeneration.Languages.NET.CSharp
{
  public abstract partial class NETCSharp : ICS23DomainTypeValueGenerator
  {
    sealed protected class DomainTypeDto {
      public dynamic Id { get; set; }
      public DateTime ModifiedDate { get; set; }
      public long ModifiedUser { get; set; }

      public string Type { get; set; }
      public string Description { get; set; }
      public string DomainTypeConstantName { get; set; }
      public string Mode { get; set; }
      public long? StandardId { get; set; }
      public long Editable { get; set; }
    }

    sealed protected class DomainValueDto
    {
      public dynamic Id { get; set; }
      public DateTime ModifiedDate { get; set; }
      public long ModifiedUser { get; set; }

      public dynamic TypeId { get; set; }
      public string ValueC { get; set; }
      public long? ValueN { get; set; }
      public DateTime? ValueD { get; set; }
      public double? ValueF { get; set; }
      public string DivId { get; set; }
      public string Description { get; set; }
      public string DomainValueConstantName { get; set; }
      public string Unit { get; set; }

    }

    protected List<DomainTypeDto> LoadDomainTypes(string domainTypeTableName)
    {
      List<DomainTypeDto> domainTypes = new ();
      using (SqlConnection connection = new (_profile.Database.ConnectionString))
      {
        using (SqlCommand cmd = connection.CreateCommand())
        {
          try
          {
            bool legacyTable = !Convert.ToBoolean(ConstantNameColumnExists(domainTypeTableName, cmd, connection));
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @$"
SELECT [Id]
      ,[ModifiedDate]
      ,[ModifiedUser]
      ,[Type]
      ,[Description]
      ,[Mode]
      ,[StandardId]
      ,[Editable]
      {(legacyTable ? "" : ",[DomainTypeConstantName]")}
  FROM [core].[{domainTypeTableName}]
  ORDER BY [Id]
  ";

            connection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
              while (reader.Read())
              {
                DomainTypeDto row = new()
                {
                  Id = _profile.Global.GuidIndexing ? reader.GetGuid(0) : reader.GetInt64(0),
                  ModifiedDate = reader.GetDateTime(1),
                  ModifiedUser = reader.GetInt64(2),
                  Type = legacyTable ? reader.GetString(3) : reader.GetStringNullableFromNullableDbValue(3),
                  Description = legacyTable ? reader.GetString(4) : reader.GetStringNullableFromNullableDbValue(4),
                  Mode = reader.GetString(5),
                  StandardId = reader.GetInt64NullableFromNullableDbValue(6),
                  Editable = reader.GetInt64(7),
                  DomainTypeConstantName = legacyTable ? reader.GetString(3) : reader.GetString(8),
                };
                domainTypes.Add(row);
              }
              reader.Close();
            }
            connection.Close();
          }
          catch
          {
            throw;
          }
        }
      }
      return domainTypes;
    }

    protected List<DomainValueDto> LoadDomainValues(string domainValueTableName)
    {
      List<DomainValueDto> domainValues = new();
      using (SqlConnection connection = new(_profile.Database.ConnectionString))
      {
        using (SqlCommand cmd = connection.CreateCommand())
        {
          try
          {
            bool legacyTable = !Convert.ToBoolean(ConstantNameColumnExists(domainValueTableName, cmd, connection));
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;

            // ignore [core].[TenantId]
            cmd.CommandText = @$"
SELECT [Id]
      ,[ModifiedDate]
      ,[ModifiedUser]
      ,[TypeId]
      ,[ValueC]
      ,[ValueN]
      ,[ValueD]
      ,[ValueF]
      ,[DivId]
      ,[Description]
      ,[Unit]
      {(legacyTable ? "" : ",[DomainValueConstantName]")}
  FROM [core].[{domainValueTableName}]
  ORDER BY [Id]
  ";
            connection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
              while (reader.Read())
              {
                DomainValueDto row = new()
                {
                  Id = _profile.Global.GuidIndexing ? reader.GetGuid(0) : reader.GetInt64(0),
                  ModifiedDate = reader.GetDateTime(1),
                  ModifiedUser = reader.GetInt64(2),
                  TypeId = _profile.Global.GuidIndexing ? reader.GetGuid(3) : reader.GetInt64(3),
                  ValueC = reader.GetStringFromNullableDbValue(4),
                  ValueN = reader.GetInt64NullableFromNullableDbValue(5),
                  ValueD = reader.GetDateTimeNullableFromNullableDbValue(6),
                  ValueF = reader.GetDoubleNullableFromNullableDbValue(7),
                  DivId = reader.GetStringFromNullableDbValue(8),
                  Description = reader.GetStringFromNullableDbValue(9),
                  Unit = reader.GetStringFromNullableDbValue(10),
                  DomainValueConstantName = legacyTable ? reader.GetStringFromNullableDbValue(4) : reader.GetStringFromNullableDbValue(11),
                };
                domainValues.Add(row);
              }
              reader.Close();
            }
            connection.Close();
          }
          catch
          {
            throw;
          }
        }
      }
      return domainValues;
    }

    protected int ConstantNameColumnExists(string tableName, SqlCommand cmd, SqlConnection connection)
    {
      cmd.Parameters.Clear();
      cmd.CommandType = CommandType.Text;
      cmd.CommandText = $@"SELECT CASE WHEN COL_LENGTH('core.{tableName}', '{tableName}ConstantName') IS NOT NULL THEN 1 ELSE 0 END";

      connection.Open();
      int result;
      using (SqlDataReader reader = cmd.ExecuteReader())
      {
        reader.Read();
        result = reader.GetInt32(0);
      }

      connection.Close();
      return result;
    }


    void ICS23DomainTypeValueGenerator.BuildDomainTypes(Database db)
    {
      // Extract DomainTypes
      List<DomainTypeDto> domainTypes = LoadDomainTypes(_profile.Generator.GeneratorCS23DomainTypeValue.DomainTypeTableName);

      // Extract DomainValues
      List<DomainValueDto> domainValues = LoadDomainValues(_profile.Generator.GeneratorCS23DomainTypeValue.DomainValueTableName);

      // Build DomainTypes
      {
        OpenClass("DomainTypes", isStatic: true);
        foreach (DomainTypeDto domainType in domainTypes)
        {
          _sb.AppendLine($"public const {(_profile.Global.GuidIndexing ? "string" : "long")} {domainType.DomainTypeConstantName.TransformToCharOrDigitOnlyCamelCase()} = {(_profile.Global.GuidIndexing ? $@"""{domainType.Id}""" : $"{domainType.Id}")};");
        }
      }

      // Build DomainValues for DomainTypes
      {
        foreach (DomainTypeDto domainType in domainTypes)
        {
          OpenClass($"{domainType.DomainTypeConstantName.TransformToCharOrDigitOnlyCamelCase()}", null, true);
          _sb.AppendLine($"public const {(_profile.Global.GuidIndexing ? "string" : "long")} DomainTypId = {(_profile.Global.GuidIndexing ? $@"""{domainType.Id}""" : $"{domainType.Id}")};");
          _sb.AppendLine();

          foreach (DomainValueDto domainValue in domainValues.Where(w => w.TypeId == domainType.Id).OrderBy(o => o.Id))
          {
            _sb.AppendLine($"public const {(_profile.Global.GuidIndexing ? "string" : "long")} {domainValue.DomainValueConstantName.TransformToCharOrDigitOnlyCamelCase()} = {(_profile.Global.GuidIndexing ? $@"""{domainValue.Id}""" : $"{domainValue.Id}")};");
          }
        }
      }
    }
  }
}
