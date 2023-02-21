using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;

namespace Microsoft.Data.SqlClient
{
  public static class SqlDataReaderExtensions
  {
    public static byte[] GetBinaryFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetSqlBinary(colIndex).Value;
      else
        return new byte[0];
    }
    public static byte[] GetBinaryNullableFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetSqlBinary(colIndex).Value;
      else
        return null;
    }
    public static bool GetBooleanFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetBoolean(colIndex);
      else
        return false;
    }
    public static bool? GetBooleanNullableFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetBoolean(colIndex);
      else
        return null;
    }
    public static byte GetByteFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetByte(colIndex);
      else
        return 0;
    }
    public static byte? GetByteNullableFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetByte(colIndex);
      else
        return null;
    }
    public static long GetBytesFromNullableDbValue(this SqlDataReader reader, int colIndex, long dataIndex, byte[] buffer, int bufferIndex, int length)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetBytes(colIndex, dataIndex, buffer, bufferIndex, length);
      else
        return 0;
    }
    public static char GetCharFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetSingleChar(colIndex);
      else
        return ' ';
    }
    public static char GetSingleChar(this SqlDataReader reader, int columnIndex)
    {
      SqlChars val = reader.GetSqlChars(columnIndex);
      if (val.Length != 1)
      {
        throw new ApplicationException(
          "Expected value to be 1 char long, but was "
          + val.Length.ToString() + " chars long.");
      }
      return val[0];
    }
    public static char? GetCharNullableFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetChar(colIndex);
      else
        return null;
    }
    public static long GetCharsFromNullableDbValue(this SqlDataReader reader, int colIndex, long dataIndex, char[] buffer, int bufferIndex, int length)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetChars(colIndex, dataIndex, buffer, bufferIndex, length);
      else
        return 0;
    }
    public static DateTime GetDateTimeFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetDateTime(colIndex);
      else
        return DateTime.MinValue;
    }
    public static DateTime? GetDateTimeNullableFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetDateTime(colIndex);
      else
        return null;
    }
    public static DateTimeOffset GetDateTimeOffsetFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetDateTimeOffset(colIndex);
      else
        return DateTimeOffset.MinValue;
    }
    public static DateTimeOffset? GetDateTimeOffsetNullableFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetDateTimeOffset(colIndex);
      else
        return null;
    }
    public static decimal GetDecimalFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetDecimal(colIndex);
      else
        return 0;
    }
    public static decimal? GetDecimalNullableFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetDecimal(colIndex);
      else
        return null;
    }
    public static double GetDoubleFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetDouble(colIndex);
      else
        return 0;
    }
    public static double? GetDoubleNullableFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetDouble(colIndex);
      else
        return null;
    }
    public static float GetFloatFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetFloat(colIndex);
      else
        return 0;
    }
    public static float? GetFloatNullableFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetFloat(colIndex);
      else
        return null;
    }
    public static Guid GetGuidFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetGuid(colIndex);
      else
        return Guid.Empty;
    }
    public static Guid? GetGuidNullableFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetGuid(colIndex);
      else
        return null;
    }
    public static short GetInt16FromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetInt16(colIndex);
      else
        return 0;
    }
    public static short? GetInt16NullableFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetInt16(colIndex);
      else
        return null;
    }
    public static int GetInt32FromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetInt32(colIndex);
      else
        return 0;
    }
    public static int? GetInt32NullableFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetInt32(colIndex);
      else
        return null;
    }
    public static long GetInt64FromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetInt64(colIndex);
      else
        return 0;
    }
    public static long? GetInt64NullableFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetInt64(colIndex);
      else
        return null;
    }
    public static string GetStringFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetString(colIndex);
      else
        return string.Empty;
    }
    public static string GetStringNullableFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetString(colIndex);
      else
        return null;
    }
    public static TextReader GetTextReaderFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetTextReader(colIndex);
      else
        return TextReader.Null;
    }
    public static TimeSpan GetTimeSpanFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetTimeSpan(colIndex);
      else
        return TimeSpan.MinValue;
    }
    public static TimeSpan? GetTimeSpanNullableFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetTimeSpan(colIndex);
      else
        return null;
    }
    public static object GetValueFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetValue(colIndex);
      else
        return null;
    }
    public static XmlReader GetXmlReaderFromNullableDbValue(this SqlDataReader reader, int colIndex)
    {
      if (!reader.IsDBNull(colIndex))
        return reader.GetXmlReader(colIndex);
      else
        return null;
    }

    // Custom types
    public static DataTable GetDataTable(ICollection<double> values)
    {
      DataTable table = new DataTable();
      table.Columns.Add("val", typeof(double));

      foreach (double value in values)
      {
        table.Rows.Add(value);
      }
      return table;
    }
    public static DataTable GetDataTable(ICollection<float> values)
    {
      DataTable table = new DataTable();
      table.Columns.Add("val", typeof(float));

      foreach (float value in values)
      {
        table.Rows.Add(value);
      }
      return table;
    }
    public static DataTable GetDataTable(ICollection<decimal> values)
    {
      DataTable table = new DataTable();
      table.Columns.Add("val", typeof(decimal));

      foreach (decimal value in values)
      {
        table.Rows.Add(value);
      }
      return table;
    }
    public static DataTable GetDataTable(ICollection<long> values)
    {
      DataTable table = new DataTable();
      table.Columns.Add("val", typeof(long));

      foreach (long value in values)
      {
        table.Rows.Add(value);
      }
      return table;
    }
    public static DataTable GetDataTable(ICollection<int> values)
    {
      DataTable table = new DataTable();
      table.Columns.Add("val", typeof(int));

      foreach (int value in values)
      {
        table.Rows.Add(value);
      }
      return table;
    }
    public static DataTable GetDataTable(ICollection<short> values)
    {
      DataTable table = new DataTable();
      table.Columns.Add("val", typeof(short));

      foreach (short value in values)
      {
        table.Rows.Add(value);
      }
      return table;
    }
    public static DataTable GetDataTable(ICollection<byte> values)
    {
      DataTable table = new DataTable();
      table.Columns.Add("val", typeof(byte));

      foreach (byte value in values)
      {
        table.Rows.Add(value);
      }
      return table;
    }
    public static DataTable GetDataTable(ICollection<Guid> values)
    {
      DataTable table = new DataTable();
      table.Columns.Add("val", typeof(Guid));

      foreach (Guid value in values)
      {
        table.Rows.Add(value);
      }
      return table;
    }
    public static DataTable GetDataTable(ICollection<string> values)
    {
      DataTable table = new DataTable();
      table.Columns.Add("val", typeof(string));

      foreach (string value in values)
      {
        table.Rows.Add(value);
      }
      return table;
    }

    public static T GetEnum<T>(this SqlDataReader reader, int i) where T : struct, Enum
    {
      return Enum.Parse<T>(reader.GetString(i));
    }
    public static T GetEnum<T>(this SqlDataReader reader, int i, T defaultValue) where T : struct, Enum
    {
      if (Enum.TryParse(reader.GetString(i), out T columnDataType))
        return columnDataType;

      return defaultValue;
    }

  }

}
