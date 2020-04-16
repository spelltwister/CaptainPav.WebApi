using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CaptainPav.WebApiProxy
{
	/// <summary>
	/// Reconstructs .net exceptions sent in a json response
	/// </summary>
	public class NetExceptionReconstructor : IExceptionReconstructor
	{
		/// <inheritdoc />
		public async Task<Exception> Reconstruct(HttpResponseMessage message)
		{
			return Reconstruct(await message.Content.ReadAsStringAsync().ConfigureAwait(false));
		}

		/// <inheritdoc />
		public Exception Reconstruct(string responseContent)
		{
			if (String.IsNullOrWhiteSpace(responseContent))
			{
				return null;
			}

			try
			{
				JObject jObject = JObject.Parse(responseContent);
				var exTypeProperty = jObject.Properties().FirstOrDefault(x => StringComparer.OrdinalIgnoreCase.Equals(x.Name, "ExceptionType"));

				if (exTypeProperty != null)
				{
					Type innerExceptionType = Type.GetType(exTypeProperty.Value.Value<string>());
					if (innerExceptionType != null)
					{
						var exMessageProperty = jObject.Properties().First(x => StringComparer.OrdinalIgnoreCase.Equals(x.Name, "ExceptionMessage"));
						var exMessage = exMessageProperty.Value?.Value<string>();

						return Activator.CreateInstance(innerExceptionType, exMessage) as Exception;
					}
				}
			}
			catch (JsonReaderException)
			{
				// could not parse the response as an exception
				return new Exception(responseContent);
			}
			catch
			{
				return new Exception(responseContent);
			}

			return new Exception(responseContent);
		}
	}
}