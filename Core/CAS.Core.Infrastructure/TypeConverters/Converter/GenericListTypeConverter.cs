using System.ComponentModel;
using System.Globalization;

namespace CAS.Core.Infrastructure.TypeConverters.Converter
{
	public class GenericListTypeConverter<T> : TypeConverter
	{
		private readonly TypeConverter _typeConverter;
		public GenericListTypeConverter()
		{
			_typeConverter = TypeDescriptor.GetConverter(typeof(T));
			if (_typeConverter == null)
				throw new InvalidOperationException("No type converter exists for type " + typeof(T).FullName);
		}
		protected virtual string[] GetStringArray(string input)
		{
			return string.IsNullOrEmpty(input) ? Array.Empty<string>() : input.Split(',').Select(x => x.Trim()).ToArray();
		}
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType != typeof(string)) return base.CanConvertFrom(context, sourceType);
			var items = GetStringArray(sourceType.ToString());
			return items.Any();

		}
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is not string strValue) return base.ConvertFrom(context, culture, value);
			var items = GetStringArray(strValue);
			var result = new List<T>();
			Array.ForEach(items, s =>
			{
				var item = _typeConverter.ConvertFromInvariantString(s);
				if (item != null)
				{
					result.Add((T)item);
				}
			});

			return result;
		}
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != typeof(string)) return base.ConvertTo(context, culture, value, destinationType);
			var result = string.Empty;
			if (value == null) return result;
			for (var i = 0; i < ((IList<T>)value).Count; i++)
			{
				var str1 = Convert.ToString(((IList<T>)value)[i], CultureInfo.InvariantCulture);
				result += str1;
				if (i != ((IList<T>)value).Count - 1)
					result += ",";
			}
			return result;

		}
	}
}
