﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace CAS.Core.Infrastructure.TypeConverters.JsonConverters
{
	public class InterfaceConverter<TInterface, TConcrete> : JsonConverter<TInterface> where TConcrete : TInterface, new()
	{
		public override TInterface Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return JsonSerializer.Deserialize<TConcrete>(ref reader, options);
		}
		public override void Write(Utf8JsonWriter writer, TInterface value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case null:
					JsonSerializer.Serialize(writer, null, options);
					break;
				default:
					{
						var type = value.GetType();
						JsonSerializer.Serialize(writer, value, type, options);
						break;
					}
			}
		}
	}
}
