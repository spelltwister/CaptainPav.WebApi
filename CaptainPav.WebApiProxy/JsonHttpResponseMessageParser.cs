using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CaptainPav.WebApiProxy
{	
	/// <summary>
	/// Parses Http Response Message body as JSON
	/// </summary>
	public class JsonHttpResponseMessageParser : IHttpResponseMessageParser
	{
		private static JsonSerializer StreamDeserializer { get; } = new JsonSerializer();

		/// <summary>
		/// Gets the reader which consumes the <see cref="HttpResponseMessage"/> and returns
		/// the content body as string data
		/// </summary>
		protected IHttpResponseMessageReader MessageReader { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="JsonHttpResponseMessageParser"/> class.
		/// </summary>
		/// <param name="reader">
		/// The reader which consumes the <see cref="HttpResponseMessage"/> and returns
		/// the content body as string data
		/// </param>
		public JsonHttpResponseMessageParser(IHttpResponseMessageReader reader)
		{
            this.MessageReader = reader ?? throw new ArgumentNullException(nameof(reader));
		}

		/// <inheritdoc />
		public Task ParseAsync(HttpResponseMessage message, params HttpStatusCode[] validCodes)
		{
			return this.MessageReader.ThrowIfNotSuccessfulAsync(message, validCodes);
		}

		/// <inheritdoc />
		public async Task<TResponse> ParseAsync<TResponse>(HttpResponseMessage message, string jsonTokenSelector = null, params HttpStatusCode[] validCodes)
		{
            if (String.IsNullOrWhiteSpace(jsonTokenSelector))
            {
                using (var stream = await this.MessageReader.GetStreamIfSuccessfulOrThrowAsync(message, validCodes).ConfigureAwait(false))
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    return StreamDeserializer.Deserialize<TResponse>(json);
                }
            }

            string bodyContent = await this.MessageReader.ReadMessageIfSuccessfulOrThrowAsync(message, validCodes).ConfigureAwait(false);
            return InnerParse<TResponse>(bodyContent, jsonTokenSelector);
		}

		/// <summary>
		/// Parses the body content
		/// </summary>
		/// <typeparam name="TResponse">
		/// Type of response into which to deserialize
		/// </typeparam>
		/// <param name="bodyContent">
		/// Content body of the <see cref="HttpResponseMessage"/>
		/// </param>
		/// <param name="jsonTokenSelector">
		/// [Optional] If set, identifies the node in the <paramref name="bodyContent"/>
		/// to treat as the root node when deserializing the response
		/// </param>
		/// <returns>
		/// The <see cref="HttpResponseMessage"/> content body deserialized
		/// into the requested type.
		/// </returns>
		protected TResponse InnerParse<TResponse>(string bodyContent, string jsonTokenSelector = null)
		{
			if (!String.IsNullOrWhiteSpace(jsonTokenSelector))
			{
				JToken token = JObject.Parse(bodyContent).SelectToken(jsonTokenSelector, false);
				if (token == null)
				{
					throw new ResponseTokenNotFoundException($"The response body did not contain the specified token `{jsonTokenSelector}`.");
				}

				bodyContent = token.ToString();
			}

			return JsonConvert.DeserializeObject<TResponse>(bodyContent);
		}
	}
}