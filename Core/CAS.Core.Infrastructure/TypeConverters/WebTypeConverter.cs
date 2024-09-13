using CAS.Core.Infrastructure.TypeConverters.Converter;
using SME.Core.Infrastructure.TypeConverters;
using System.ComponentModel;

namespace CAS.Core.Infrastructure.TypeConverters
{
    public class WebTypeConverter : ITypeConverter
    {
        public void Register()
        {
            TypeDescriptor.AddAttributes(typeof(bool), new TypeConverterAttribute(typeof(BoolTypeConverter)));


            TypeDescriptor.AddAttributes(typeof(Dictionary<int, int>), new TypeConverterAttribute(typeof(GenericDictionaryTypeConverter<int, int>)));
            TypeDescriptor.AddAttributes(typeof(Dictionary<string, bool>), new TypeConverterAttribute(typeof(GenericDictionaryTypeConverter<string, bool>)));


        }

        public int Order => 0;
    }
}
