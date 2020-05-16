using System.IO;
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
		/// Thrown if the <see cref="HttpResponseMessage"/> indicates an unsuccessful status code
		/// </exception>
		Task<string> ReadMessageIfSuccessfulOrThrowAsync(HttpResponseMessage message, params HttpStatusCode[] validCodes);

		Task<Stream> GetStreamIfSuccessfulOrThrowAsync(HttpResponseMessage message, params HttpStatusCode[] validCodes);

		/// <summary>
		/// Throws an exception if the response message indicates
		/// an unsuccessful result
		/// </summary>
		/// <param name="message">
		/// The <see cref="HttpResponseMessage"/> to check for failure
		/// </param>
        /// <param name="validCodes">
        /// The set of <see cref="HttpStatusCode"/>s which represent a successful call
        /// </param>
        /// <remarks>
        /// This method will try to reconstruct the original exception sent in the
        /// body of the response
        /// </remarks>
		Task ThrowIfNotSuccessfulAsync(HttpResponseMessage message, params HttpStatusCode[] validCodes);
    }
}