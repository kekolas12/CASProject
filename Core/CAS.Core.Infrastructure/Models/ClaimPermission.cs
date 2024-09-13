namespace CAS.Core.Infrastructure.Models
{
	public class ClaimModels
	{
		public List<ClaimPermission> Permission { get; set; }
		public string Provider { get; set; }
	}
	public class ClaimPermission
	{
		public string Controller { get; set; }
		public string Action { get; set; }
		public string Permission { get; set; }
	}
}
