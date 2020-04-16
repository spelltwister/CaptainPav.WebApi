using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CaptainPav.WebApiProxy
{
	/// <summary>
	/// Reconstructor which simply returns the response message content
	/// in an <see cref="Exception" />
	/// </summary>
	public class PassThruReconstructor : IExceptionReconstructor
	{
		/// <inheritdoc />
		public async Task<Exception> Reconstruct(HttpResponseMessage message)
		{
			return Reconstruct(await message.Content.ReadAsStringAsync().ConfigureAwait(false));
		}

		/// <inheritdoc />
		public Exception Reconstruct(string encdedException)
		{
			return new Exception(encdedException);
		}
	}
}