using System.Net.Http;

namespace CaptainPav.WebApiProxy
{	
	/// <summary>
	/// Converts models into <see cref="HttpContent"/> for use with <see cref="HttpClient"/>
	/// </summary>
	public interface IHttpContentConverter
	{
		/// <summary>
		/// Converts the given model into <see cref="HttpContent"/>
		/// </summary>
		/// <param name="model">
		/// The model to convert
		/// </param>
		/// <returns>
		/// The model encoded into <see cref="HttpContent"/>
		/// </returns>
		HttpContent ToHttpContent(object model);
	}
}