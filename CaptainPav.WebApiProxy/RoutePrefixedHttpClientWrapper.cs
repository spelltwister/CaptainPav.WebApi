using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CaptainPav.WebApiProxy
{
	/// <summary>
	/// Wrapper over HttpClient which makes requests relative to a common
	/// route prefix along with additional headers per-request.
	/// </summary>
	public class RoutePrefixedHttpClientWrapper
	{
		/// <summary>
		/// Gets the client used to make requests to the remote server
		/// </summary>
		public HttpClient Client { get; }

		/// <summary>
		/// Gets the Route Prefix used when making requests to the
		/// remote server
		/// </summary>
		/// <remarks>
		/// The Route Prefix is the common prefix used when making
		/// requests to the remote server.  A request Uri is of the
		/// form [BaseAddress/][RoutePrefix][/RoutePath]? where the
		/// BaseAddress includes a trailing slash to the api host,
		/// the RoutePrefix has no leading or trailing slashes and
		/// represents a service, RoutePath is optional (or more
		/// specifically, "" is valid and different from /"").
		/// </remarks>
		public string RoutePrefix { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="RoutePrefixedHttpClientWrapper"/> class.
		/// </summary>
		/// <param name="client">
		/// HttpClient to wrap which has an absolute Uri Base Address set
		/// </param>
		/// <param name="routePrefix">
		/// The route prefix used when making requests to the remote endpoint
		/// </param>
		public RoutePrefixedHttpClientWrapper(HttpClient client, string routePrefix)
		{
			if(client == null)
			{
				throw new ArgumentNullException(nameof(client));
			}
			
			if(client.BaseAddress == null)
			{
				throw new ArgumentException("Base Address cannot be null.");
			}

			if (!client.BaseAddress.IsAbsoluteUri)
			{
				throw new ArgumentException("Base Address must be an absolute Uri.");
			}

			this.Client = client;
			this.RoutePrefix = UriHelpers.EnsureNoLeadingSlash(UriHelpers.EnsureNoTrailingSlash(routePrefix));
		}

		public Task<HttpResponseMessage> DeleteAsync(
			string relativeRequestUri,
			IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.Client.DeleteAsync(this.RoutePrefix, relativeRequestUri, additionalHeaders, cancellationToken);
		}

		public Task<HttpResponseMessage> GetAsync(
			string relativeRequestUri,
			HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
			IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.Client.GetAsync(this.RoutePrefix, relativeRequestUri, completionOption, additionalHeaders, cancellationToken);
		}

		public Task<byte[]> GetByteArrayAsync(string relativeRequestUri)
		{
			return this.Client.GetByteArrayAsync(this.RoutePrefix, relativeRequestUri);
		}

		public Task<Stream> GetStreamAsync(string relativeRequestUri)
		{
			return this.Client.GetStreamAsync(this.RoutePrefix, relativeRequestUri);
		}

		public Task<string> GetStringAsync(string relativeRequestUri)
		{
			return this.Client.GetStringAsync(this.RoutePrefix, relativeRequestUri);
		}

		public Task<HttpResponseMessage> PostAsync(
			string relativeRequestUri,
			HttpContent content = null,
			IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.Client.PostAsync(this.RoutePrefix, relativeRequestUri, content, additionalHeaders, cancellationToken);
		}

		public Task<HttpResponseMessage> PostModelAsync(
			string relativeRequestUri,
			object content = null,
			IHttpContentConverter converter = null,
			IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.Client.PostModelAsync(this.RoutePrefix, relativeRequestUri, content, converter, additionalHeaders, cancellationToken);
		}
		
		public Task<HttpResponseMessage> PutAsync(
			string relativeRequestUri,
			HttpContent content = null,
			IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.Client.PutAsync(this.RoutePrefix, relativeRequestUri, content, additionalHeaders, cancellationToken);
		}

		public Task<HttpResponseMessage> PutModelAsync(
			string relativeRequestUri,
			object content = null,
			IHttpContentConverter converter = null,
			IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.Client.PutModelAsync(this.RoutePrefix, relativeRequestUri, content, converter, additionalHeaders, cancellationToken);
		}

		public Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage requestMessage,
			HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
			CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.Client.SendAsync(requestMessage, completionOption, cancellationToken);
		}
	}
}