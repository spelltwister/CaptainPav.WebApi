using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CaptainPav.WebApiProxy
{
	public class RoutePrefixedHttpClient : HttpClient
	{
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
		/// Initializes a new instance of the <see cref="RoutePrefixedHttpClient"/> class
		/// </summary>
		/// <param name="routePrefix">
		/// Route Prefix used when making requests to the remote server
		/// </param>
		public RoutePrefixedHttpClient(string routePrefix) : this(new HttpClientHandler(), routePrefix) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RoutePrefixedHttpClient"/> class
		/// </summary>
		/// <param name="routePrefix">
		/// Route Prefix used when making requests to the remote server
		/// </param>
		/// <param name="handler">
		/// The HTTP handler stack to use for sending requests.
		/// </param>
		public RoutePrefixedHttpClient(HttpMessageHandler handler, string routePrefix) : this(handler, true, routePrefix) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="RoutePrefixedHttpClient"/> class
		/// </summary>
		/// <param name="routePrefix">
		/// Route Prefix used when making requests to the remote server
		/// </param>
		/// <param name="handler">
		/// The HTTP handler stack to use for sending requests.
		/// </param>
		/// <param name="disposeHandler">
		/// true if the inner handler should be disposed of by Dispose(), false if you intend
		/// to reuse the inner handler.
		/// </param>
		public RoutePrefixedHttpClient(HttpMessageHandler handler, bool disposeHandler, string routePrefix)
			: base(handler, disposeHandler)
		{
			this.RoutePrefix = UriHelpers.EnsureNoLeadingSlash(UriHelpers.EnsureNoTrailingSlash(routePrefix));
		}

		/// <inheritdoc />
		public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (!request.RequestUri.IsAbsoluteUri &&
			    !request.RequestUri.OriginalString.StartsWith(this.RoutePrefix))
			{
				request.RequestUri = UriHelpers.RelativeEndpointUri(this.RoutePrefix, request.RequestUri.OriginalString);
			}

			return base.SendAsync(request, cancellationToken);
		}
	}
}