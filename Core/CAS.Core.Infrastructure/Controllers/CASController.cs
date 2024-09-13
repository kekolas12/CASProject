using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CAS.Core.Infrastructure.Controllers
{
	public class CASController : BaseApiController
	{
		private IMediator _Mediator;
		private ISendEndpointProvider _SendEndpointProvider;
		private IPublishEndpoint _PublishEndpoint;

		protected IMediator _mediator => _Mediator ??= HttpContext.RequestServices.GetService<IMediator>();
		protected ISendEndpointProvider _sendEndpointProvider => _SendEndpointProvider ??= HttpContext.RequestServices.GetService<ISendEndpointProvider>();
		protected IPublishEndpoint _publishEndpoint => _PublishEndpoint ??= HttpContext.RequestServices.GetService<IPublishEndpoint>();
		public override JsonResult Json(object data)
		{
			var serializerSettings = new JsonSerializerSettings
			{
				DateTimeZoneHandling = DateTimeZoneHandling.Utc,
				Formatting = Formatting.Indented,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
				ContractResolver = new DefaultContractResolver
				{
					NamingStrategy = new CamelCaseNamingStrategy()
				}
			};

			return new JsonResult(data, serializerSettings);
		}
	}
}
