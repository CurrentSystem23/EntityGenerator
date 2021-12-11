﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EntityGenerator.Profile.SerializingHelper
{
  public class StringNullableEnumConverter<T> : JsonConverter<T>
  {
    private readonly JsonConverter<T> _converter;
    private readonly Type _underlyingType;

    public StringNullableEnumConverter() : this(null) { }

    public StringNullableEnumConverter(JsonSerializerOptions options)
    {
      // for performance, use the existing converter if available
      if (options != null)
      {
        _converter = (JsonConverter<T>)options.GetConverter(typeof(T));
      }

      // cache the underlying type
      _underlyingType = Nullable.GetUnderlyingType(typeof(T));
    }

    public override bool CanConvert(Type typeToConvert)
    {
      return typeof(T).IsAssignableFrom(typeToConvert);
    }

    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      if (_converter != null)
      {
        return _converter.Read(ref reader, _underlyingType, options);
      }

      string value = reader.GetString();

      if (String.IsNullOrEmpty(value)) return default;

      // for performance, parse with ignoreCase:false first.
      if (!Enum.TryParse(typeToConvert, value, ignoreCase: false, out object result)
          && !Enum.TryParse(typeToConvert, value, ignoreCase: true, out result))
      {
        throw new JsonException($"Unable to convert \"{value}\" to Enum \"{typeToConvert}\".");
      }

      return (T)result;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
      writer.WriteStringValue(value?.ToString());
    }
  }
}
