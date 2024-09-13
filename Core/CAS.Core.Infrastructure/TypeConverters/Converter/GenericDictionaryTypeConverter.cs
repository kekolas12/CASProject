using System.ComponentModel;
using System.Globalization;

namespace CAS.Core.Infrastructure.TypeConverters.Converter
{
	public class GenericDictionaryTypeConverter<K, V> : TypeConverter
	{
		private readonly TypeConverter _typeConverterKey;
		private readonly TypeConverter _typeConverterValue;

		public GenericDictionaryTypeConverter()
		{
			_typeConverterKey = TypeDescriptor.GetConverter(typeof(K));
			if (_typeConverterKey == null)
				throw new InvalidOperationException("No type converter exists for type " + typeof(K).FullName);
			_typeConverterValue = TypeDescriptor.GetConverter(typeof(V));
			if (_typeConverterValue == null)
				throw new InvalidOperationException("No type converter exists for type " + typeof(V).FullName);
		}
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is not string input) return base.ConvertFrom(context, culture, value);
			var items = string.IsNullOrEmpty(input) ? Array.Empty<string>() : input.Split(';').Select(x => x.Trim()).ToArray();

			var result = new Dictionary<K, V>();
			foreach (var s in items)
			{
				var keyValueStr = string.IsNullOrEmpty(s) ? Array.Empty<string>() : s.Split(',').Select(x => x.Trim()).ToArray();
				if (keyValueStr.Length != 2) continue;
				var dictionaryKey = _typeConverterKey.ConvertFromInvariantString(keyValueStr[0]);
				var dictionaryValue = _typeConverterKey.ConvertFromInvariantString(keyValueStr[1]);
				if (dictionaryKey == null || dictionaryValue == null) continue;
				if (result.ContainsKey((K)dictionaryKey)) continue;
				var itemValue = Convert.ChangeType(dictionaryValue, typeof(V));
				result.Add((K)dictionaryKey, (V)itemValue);
			}

			return result;
		}
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != typeof(string)) return base.ConvertTo(context, culture, value, destinationType);
			var result = string.Empty;
			if (value == null) return result;

			var counter = 0;
			var dictionary = (IDictionary<K, V>)value;
			foreach (var keyValue in dictionary)
			{
				result += $"{Convert.ToString(keyValue.Key, CultureInfo.InvariantCulture)},{Convert.ToString(keyValue.Value, CultureInfo.InvariantCulture)}";

				if (counter != dictionary.Count - 1)
					result += ";";
				counter++;
			}
			return result;

		}
	}
}
