using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CaptainPav.WebApiProxy
{	
	/// <summary>
	/// Interface for reading HttpResponse message bodies
	/// </summary>
	public interface IHttpResponseMessageReader
	{
		/// <summary>
		/// Attempts to read the <see cref="HttpResponseMessage"/> body as a
		/// string and throws an exception if the response message indicates
		/// an unsuccessful result
		/// </summary>
		/// <param name="message">
		/// The <see cref="HttpResponseMessage"/> to read
		/// </param>
		/// <param name="validCodes">
		/// The set of <see cref="HttpStatusCode"/>s which represent a successful call
		/// </param>
		/// <returns>
		/// The content body as a string if the request indicates success;
		/// otherwise, an exception is thrown.
		/// </returns>
		/// <exception cref="UnexpectedStatusCodeException">
		/// Throw if the <see cref="HttpResponseMessage"/> indicates an unsuccessful status code
		/// </exception>
		Task<string> ReadMessageIfSuccessfulOrThrowAsync(HttpResponseMessage message, params HttpStatusCode[] validCodes);
	}
}