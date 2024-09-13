namespace CAS.Shared.Models
{
	public class BaseModel
	{
		public BaseModel()
		{
			PostInitialize();

		}
		protected virtual void PostInitialize()
		{
		}
	}
}
