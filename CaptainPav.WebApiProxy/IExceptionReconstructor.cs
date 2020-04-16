using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CaptainPav.WebApiProxy
{
	/// <summary>
	/// Reconstructs exceptions from a serialized representation
	/// </summary>
	public interface IExceptionReconstructor
	{
		/// <summary>
		/// Reconstructs an exception from body of the <see cref="HttpResponseMessage"/>
		/// </summary>
		/// <param name="message">
		/// Message whose body should be reconstructed into an exception
		/// </param>
		/// <returns>
		/// A reconstructed exception from the body of the <see cref="HttpResponseMessage"/>
		/// </returns>
		Task<Exception> Reconstruct(HttpResponseMessage message);

		/// <summary>
		/// Reconstructs an exception from and encoded exception
		/// </summary>
		/// <param name="encdedException">
		/// The exception to try and reconstruct
		/// </param>
		/// <returns>
		/// A reconstructed exception or null if it could
		/// not be reconstructed
		/// </returns>
		Exception Reconstruct(string encdedException);
	}
}