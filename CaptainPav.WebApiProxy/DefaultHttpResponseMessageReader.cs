using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CaptainPav.WebApiProxy
{
	public class DefaultHttpResponseMessageReader : IHttpResponseMessageReader
	{
		/// <summary>
		/// Gets whether to try and reconstruct the original exception when the
		/// response message indicates unsuccessful result
		/// </summary>
		protected IExceptionReconstructor ExceptionReconstructor { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultHttpResponseMessageReader"/> class
		/// </summary>
		/// <param name="reconstructor">
		/// When the <see cref="HttpResponseMessage"/> indicates an unsuccessful
		/// response, then this reconstructor is used to try and reconstruct the
		/// original exception.
		/// </param>
		public DefaultHttpResponseMessageReader(IExceptionReconstructor reconstructor)
		{
			this.ExceptionReconstructor = reconstructor;
		}

		/// <inheritdoc />
		public Task<string> ReadMessageIfSuccessfulOrThrowAsync(HttpResponseMessage message, params HttpStatusCode[] validCodes)
		{
			return ReadMessageIfSuccessfulOrThrowAsync(message, this.ExceptionReconstructor, validCodes);
		}

		public Task<Stream> GetStreamIfSuccessfulOrThrowAsync(HttpResponseMessage message, params HttpStatusCode[] validCodes)
		{
			return GetStreamIfSuccessfulOrThrowAsync(message, this.ExceptionReconstructor, validCodes);
		}

		public Task ThrowIfNotSuccessfulAsync(HttpResponseMessage message, params HttpStatusCode[] validCodes)
		{
			return ThrowIfNotSuccessfulAsync(message, this.ExceptionReconstructor, validCodes);
		}

		/// <summary>
		/// Throws an exception if the response message indicates
		/// an unsuccessful result
		/// </summary>
		/// <param name="message">
		/// The <see cref="HttpResponseMessage"/> to check for failure
		/// </param>
		/// <param name="reconstructor">
		/// When the <see cref="HttpResponseMessage"/> indicates an unsuccessful
		/// response, then this reconstructor is used to try and reconstruct the
		/// original exception.
		/// </param>
		/// <param name="validCodes">
		/// The set of <see cref="HttpStatusCode"/>s which represent a successful call
		/// </param>
		/// <exception cref="UnexpectedStatusCodeException">
		/// Thrown if the <see cref="HttpResponseMessage"/> indicates an unsuccessful status code
		/// </exception>
		public static async Task ThrowIfNotSuccessfulAsync(HttpResponseMessage message, IExceptionReconstructor reconstructor, params HttpStatusCode[] validCodes)
		{
			if (!message.IsResponseSuccessful(validCodes))
			{
				throw message.CreateExceptionIfNotSuccessful(await reconstructor.Reconstruct(message).ConfigureAwait(false), validCodes);
			}
		}

		/// <summary>
		/// Attempts to read the <see cref="HttpResponseMessage"/> body as a
		/// string and throws an exception if the response message indicates
		/// an unsuccessful result
		/// </summary>
		/// <param name="message">
		/// The <see cref="HttpResponseMessage"/> to read
		/// </param>
		/// <param name="reconstructor">
		/// When the <see cref="HttpResponseMessage"/> indicates an unsuccessful
		/// response, then this reconstructor is used to try and reconstruct the
		/// original exception.
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
		public static async Task<string> ReadMessageIfSuccessfulOrThrowAsync(HttpResponseMessage message, IExceptionReconstructor reconstructor, params HttpStatusCode[] validCodes)
		{
			await ThrowIfNotSuccessfulAsync(message, reconstructor, validCodes).ConfigureAwait(false);

			return await message.Content.ReadAsStringAsync().ConfigureAwait(false);
		}

		/// <summary>
		/// Attempts to read the <see cref="HttpResponseMessage"/> body stream
		/// and throws an exception if the response message indicates
		/// an unsuccessful result
		/// </summary>
		/// <param name="message">
		/// The <see cref="HttpResponseMessage"/> to read
		/// </param>
		/// <param name="reconstructor">
		/// When the <see cref="HttpResponseMessage"/> indicates an unsuccessful
		/// response, then this reconstructor is used to try and reconstruct the
		/// original exception.
		/// </param>
		/// <param name="validCodes">
		/// The set of <see cref="HttpStatusCode"/>s which represent a successful call
		/// </param>
		/// <returns>
		/// The content body stream if the request indicates success;
		/// otherwise, an exception is thrown.
		/// </returns>
		/// <exception cref="UnexpectedStatusCodeException">
		/// Thrown if the <see cref="HttpResponseMessage"/> indicates an unsuccessful status code
		/// </exception>
		public static async Task<Stream> GetStreamIfSuccessfulOrThrowAsync(HttpResponseMessage message, IExceptionReconstructor reconstructor, params HttpStatusCode[] validCodes)
		{
			await ThrowIfNotSuccessfulAsync(message, reconstructor, validCodes).ConfigureAwait(false);

			return await message.Content.ReadAsStreamAsync().ConfigureAwait(false);
		}
	}
}