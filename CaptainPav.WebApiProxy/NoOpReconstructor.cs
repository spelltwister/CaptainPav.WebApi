using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CaptainPav.WebApiProxy
{
	/// <summary>
	/// Class that does not try to reconstruct exceptions
	/// </summary>
	public class NoOpReconstructor : IExceptionReconstructor
	{
		/// <inheritdoc />
		public Task<Exception> Reconstruct(HttpResponseMessage message)
		{
			return Task.FromResult<Exception>(null);
		}

		/// <inheritdoc />
		public Exception Reconstruct(string encdedException)
		{
			return null;
		}
	}
}