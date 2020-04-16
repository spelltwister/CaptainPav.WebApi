using System.Net.Http;
using Newtonsoft.Json;

namespace CaptainPav.WebApiProxy
{
	/// <summary>
	/// Specialized <see cref="IWebApiProxy"/> which communicates via JSON
	/// </summary>
	public class JsonWebApiProxy : WebApiProxy
	{
		public JsonWebApiProxy(HttpClient client, string routePrefix, IHttpResponseMessageReader messageReader, JsonSerializerSettings serializerSettings)
			: base(client, routePrefix, new JsonHttpResponseMessageParser(messageReader), new JsonHttpContentConverter(serializerSettings))
		{
		}
	}
}