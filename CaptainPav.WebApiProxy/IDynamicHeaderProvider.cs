using System.Collections.Generic;

namespace CaptainPav.WebApiProxy
{
	public interface IDynamicHeaderProvider
	{
		/// <summary>
		/// Merges additional headers into the <see cref="HttpRequestMessage"/> before sending
		/// </summary>
		/// <param name="additionalHeaders">
		/// Additional headers to merge, if any
		/// </param>
		/// <returns>
		/// The updated headers that will be merged into the <see cref="HttpRequestMessage"/> before sending
		/// </returns>
		IEnumerable<KeyValuePair<string, IEnumerable<string>>> MergeHeaders(IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders);
	}
}