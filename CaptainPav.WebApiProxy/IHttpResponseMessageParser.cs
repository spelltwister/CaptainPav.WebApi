using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CaptainPav.WebApiProxy
{
	public interface IHttpResponseMessageParser
	{
		/// <summary>
		/// Parses the response message, ensuring successful status code
		/// </summary>
		/// <param name="message">
		/// The <see cref="HttpResponseMessage"/> to parse
		/// </param>
		/// <param name="validCodes">
		/// Set of <see cref="HttpStatusCode"/>s which reprsent a successful
		/// response message; if none are specified, the default set of
		/// status codes will be used.
		/// </param>
		/// <remarks>
		/// This method is used when the response body does not actually
		/// contain anything useful, but we still need to verify that
		/// the request was successful.  Further, since the response body
		/// does not contain anything useful, there is no reason to
		/// allow the caller to specify a jsonTokenSelector.
		/// </remarks>
		Task ParseAsync(HttpResponseMessage message, params HttpStatusCode[] validCodes);

		/// <summary>
		/// Parses the response message, ensuring successful status code
		/// </summary>
		/// <typeparam name="TResponse">
		/// Type of response into which to deserialize
		/// </typeparam>
		/// <param name="message">
		/// The <see cref="HttpResponseMessage"/> to parse
		/// </param>
		/// <param name="jsonTokenSelector">
		/// If set, identifies a node in the response which should be treated
		/// as the actual response body.
		/// </param>
		/// <param name="validCodes">
		/// Set of <see cref="HttpStatusCode"/>s which reprsent a successful
		/// response message; if none are specified, the default set of
		/// status codes will be used.
		/// </param>
		/// <returns>
		/// The <see cref="HttpResponseMessage"/> content body deserialized
		/// into the requested type.
		/// </returns>
		Task<TResponse> ParseAsync<TResponse>(HttpResponseMessage message, string jsonTokenSelector = null, params HttpStatusCode[] validCodes);
	}
}