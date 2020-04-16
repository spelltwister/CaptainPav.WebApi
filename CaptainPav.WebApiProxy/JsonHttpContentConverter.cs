using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace CaptainPav.WebApiProxy
{
	/// <summary>
	/// <see cref="IHttpContentConverter"/> implementation which converts
	/// models into a json payload
	/// </summary>
	public class JsonHttpContentConverter : IHttpContentConverter
	{
		protected JsonSerializerSettings SerializerSettings { get; }
		public JsonHttpContentConverter(JsonSerializerSettings serializerSettings)
		{
			if (null == serializerSettings)
			{
				throw new ArgumentNullException(nameof(serializerSettings));
			}

			this.SerializerSettings = serializerSettings;
		}

		public StringContent ToJsonStringContent(object model)
		{
			return new StringContent(JsonConvert.SerializeObject(model, this.SerializerSettings), Encoding.UTF8, "application/json");
		}

		public HttpContent ToHttpContent(object model)
		{
			return ToJsonStringContent(model);
		}
	}
}