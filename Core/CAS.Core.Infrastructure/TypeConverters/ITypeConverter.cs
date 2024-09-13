namespace SME.Core.Infrastructure.TypeConverters
{
    public interface ITypeConverter
    {
        void Register();
        int Order { get; }
    }
}
