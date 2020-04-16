using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CaptainPav.WebApiProxy
{
	/// <summary>
	/// Extensions to <see cref="HttpClient"/> for posting JSON body content
	/// </summary>
	public static class HttpClientExtensions
	{
		public static Task<HttpResponseMessage> DeleteAsync(this HttpClient client, string requestUri, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return DeleteAsync(client, CreateUri(requestUri), additionalHeaders, cancellationToken);
		}

		public static Task<HttpResponseMessage> DeleteAsync(this HttpClient client, Uri requestUri, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			IDictionary<string, string[]> headers = additionalHeaders?.ToDictionary(x => x.Key, x => x.Value.ToArray());
			if (headers?.Count > 0)
			{
				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
				MergeHeaders(request, headers);
				return client.SendAsync(request, cancellationToken);
			}

			return client.DeleteAsync(requestUri, cancellationToken);
		}

		public static Task<HttpResponseMessage> GetAsync(this HttpClient client, string requestUri, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return GetAsync(client, CreateUri(requestUri), completionOption, additionalHeaders, cancellationToken);
		}

		public static Task<HttpResponseMessage> GetAsync(this HttpClient client, Uri requestUri, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			IDictionary<string, string[]> headers = additionalHeaders?.ToDictionary(x => x.Key, x => x.Value.ToArray());
			if (headers?.Count > 0)
			{
				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
				MergeHeaders(request, headers);
				return client.SendAsync(request, completionOption, cancellationToken);
			}

			return client.GetAsync(requestUri, completionOption, cancellationToken);
		}

		public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string requestUri, HttpContent content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return PostAsync(client, CreateUri(requestUri), content, additionalHeaders, cancellationToken);
		}

		public static Task<HttpResponseMessage> PostAsync(this HttpClient client, Uri requestUri, HttpContent content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			IDictionary<string, string[]> headers = additionalHeaders?.ToDictionary(x => x.Key, x => x.Value.ToArray());
			if (headers?.Count > 0)
			{
				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = content };
				MergeHeaders(request, headers);
				return client.SendAsync(request, cancellationToken);
			}

			return client.PostAsync(requestUri, content, cancellationToken);
		}

		public static Task<HttpResponseMessage> PutAsync(this HttpClient client, string requestUri, HttpContent content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return PutAsync(client, CreateUri(requestUri), content, additionalHeaders, cancellationToken);
		}

		public static Task<HttpResponseMessage> PutAsync(this HttpClient client, Uri requestUri, HttpContent content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			IDictionary<string, string[]> headers = additionalHeaders?.ToDictionary(x => x.Key, x => x.Value.ToArray());
			if (headers?.Count > 0)
			{
				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, requestUri) { Content = content };
				MergeHeaders(request, headers);
				return client.SendAsync(request, cancellationToken);
			}

			return client.PutAsync(requestUri, content, cancellationToken);
		}

		#region Serialized Body Content
		/// <summary>
		/// Gets the HttpContent representation of the model, safely
		/// </summary>
		/// <param name="model">
		/// The model to convert. If the model is null, then null
		/// is returned.
		/// </param>
		/// <param name="converter">
		/// The converter used to convert the content when the model
		/// is not null
		/// </param>
		/// <returns>
		/// The HttpContent represetnation of the model.  If null is
		/// supplied as the model, the null is returned.
		/// </returns>
		private static HttpContent GetHttpContent(object model, IHttpContentConverter converter)
		{
			if (model == null)
			{
				return null;
			}

			if (converter == null)
			{
				throw new ArgumentNullException(nameof(converter), "Converter cannot be null when model is not null.");
			}

			return converter.ToHttpContent(model);
		}

		/// <summary>
		/// Posts content as JSON to an endpoint as an asynchronous operation
		/// </summary>
		/// <param name="client">
		/// The <see cref="HttpClient"/> used to post the JSON content
		/// </param>
		/// <param name="requestUri">
		/// The uri to which to send the request
		/// </param>
		/// <param name="content">
		/// The request content sent to the server
		/// </param>
		/// <param name="converter">
		/// Converter used when serializing the body content
		/// </param>
		/// <param name="additionalHeaders">
		/// Additional request headers to be sent with this request
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.
		/// </param>
		public static Task<HttpResponseMessage> PostModelAsync(this HttpClient client, string requestUri, object content = null, IHttpContentConverter converter = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return PostModelAsync(client, CreateUri(requestUri), content, converter, additionalHeaders, cancellationToken);
		}

		/// <summary>
		/// Posts content as JSON to an endpoint as an asynchronous operation
		/// </summary>
		/// <param name="client">
		/// The <see cref="HttpClient"/> used to post the JSON content
		/// </param>
		/// <param name="requestUri">
		/// The uri to which to send the request
		/// </param>
		/// <param name="content">
		/// The request content sent to the server
		/// </param>
		/// <param name="converter">
		/// Converter used when serializing the body content
		/// </param>
		/// <param name="additionalHeaders">
		/// Additional request headers to be sent with this request
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.
		/// </param>
		public static Task<HttpResponseMessage> PostModelAsync(this HttpClient client, Uri requestUri, object content = null, IHttpContentConverter converter = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return client.PostAsync(requestUri, GetHttpContent(content, converter), additionalHeaders, cancellationToken);
		}

		/// <summary>
		/// Puts content as JSON to an endpoint as an asynchronous operation
		/// </summary>
		/// <param name="client">
		/// The <see cref="HttpClient"/> used to put the JSON content
		/// </param>
		/// <param name="requestUri">
		/// The uri to which to send the request
		/// </param>
		/// <param name="content">
		/// The request content sent to the server
		/// </param>
		/// <param name="converter">
		/// Converter used when serializing the body content
		/// </param>
		/// <param name="additionalHeaders">
		/// Additional request headers to be sent with this request
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.
		/// </param>
		public static Task<HttpResponseMessage> PutModelAsync(this HttpClient client, string requestUri, object content = null, IHttpContentConverter converter = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return PutModelAsync(client, CreateUri(requestUri), content, converter, additionalHeaders, cancellationToken);
		}

		/// <summary>
		/// Puts content as JSON to an endpoint as an asynchronous operation
		/// </summary>
		/// <param name="client">
		/// The <see cref="HttpClient"/> used to put the JSON content
		/// </param>
		/// <param name="requestUri">
		/// The uri to which to send the request
		/// </param>
		/// <param name="content">
		/// The request content sent to the server
		/// </param>
		/// <param name="converter">
		/// Converter used when serializing the body content
		/// </param>
		/// <param name="additionalHeaders">
		/// Additional request headers to be sent with this request
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.
		/// </param>
		public static Task<HttpResponseMessage> PutModelAsync(this HttpClient client, Uri requestUri, object content = null, IHttpContentConverter converter = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return client.PutAsync(requestUri, GetHttpContent(content, converter), additionalHeaders, cancellationToken);
		}
		#endregion

		#region Route Prefixed
		public static Task<HttpResponseMessage> PostModelAsync(this HttpClient client, string routePrefix, string relativeRequestUri, object content = null, IHttpContentConverter converter = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			Uri relativeUri = UriHelpers.RelativeEndpointUri(routePrefix, relativeRequestUri);
			return client.PostModelAsync(relativeUri, content, converter, additionalHeaders, cancellationToken);
		}

		public static Task<HttpResponseMessage> PutModelAsync(this HttpClient client, string routePrefix, string relativeRequestUri, object content = null, IHttpContentConverter converter = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			Uri relativeUri = UriHelpers.RelativeEndpointUri(routePrefix, relativeRequestUri);
			return client.PutModelAsync(relativeUri, content, converter, additionalHeaders, cancellationToken);
		}

		public static Task<HttpResponseMessage> DeleteAsync(this HttpClient client, string routePrefix, string relativeRequestUri, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			Uri relativeUri = UriHelpers.RelativeEndpointUri(routePrefix, relativeRequestUri);
			return client.DeleteAsync(relativeUri, additionalHeaders, cancellationToken);
		}

		public static Task<HttpResponseMessage> GetAsync(this HttpClient client, string routePrefix, string relativeRequestUri, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			Uri relativeUri = UriHelpers.RelativeEndpointUri(routePrefix, relativeRequestUri);
			return client.GetAsync(relativeUri, completionOption, additionalHeaders, cancellationToken);
		}

		public static Task<byte[]> GetByteArrayAsync(this HttpClient client, string routePrefix, string relativeRequestUri)
		{
			Uri relativeUri = UriHelpers.RelativeEndpointUri(routePrefix, relativeRequestUri);
			return client.GetByteArrayAsync(relativeUri);
		}

		public static Task<Stream> GetStreamAsync(this HttpClient client, string routePrefix, string relativeRequestUri)
		{
			Uri relativeUri = UriHelpers.RelativeEndpointUri(routePrefix, relativeRequestUri);
			return client.GetStreamAsync(relativeUri);
		}

		public static Task<string> GetStringAsync(this HttpClient client, string routePrefix, string relativeRequestUri)
		{
			Uri relativeUri = UriHelpers.RelativeEndpointUri(routePrefix, relativeRequestUri);
			return client.GetStringAsync(relativeUri);
		}

		public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string routePrefix, string relativeRequestUri, HttpContent content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			Uri relativeUri = UriHelpers.RelativeEndpointUri(routePrefix, relativeRequestUri);
			return client.PostAsync(relativeUri, content, additionalHeaders, cancellationToken);
		}

		public static Task<HttpResponseMessage> PutAsync(this HttpClient client, string routePrefix, string relativeRequestUri, HttpContent content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			Uri relativeUri = UriHelpers.RelativeEndpointUri(routePrefix, relativeRequestUri);
			return client.PutAsync(relativeUri, content, additionalHeaders, cancellationToken);
		}
		#endregion

		/// <summary>
		/// Creates a <see cref="UriKind.RelativeOrAbsolute"/> <see cref="Uri"/> from the uri string
		/// </summary>
		/// <param name="uri">
		/// String representation of the <see cref="Uri"/> to create
		/// </param>
		/// <returns>
		/// A <see cref="UriKind.RelativeOrAbsolute"/> <see cref="Uri"/> from the uri string
		/// </returns>
		private static Uri CreateUri(string uri)
		{
			if (string.IsNullOrEmpty(uri))
			{
				return null;
			}

			return new Uri(uri, UriKind.RelativeOrAbsolute);
		}

		private static void MergeHeaders(HttpRequestMessage request, IDictionary<string, string[]> headers)
		{
			foreach (var header in headers)
			{
				if (!request.Headers.TryAddWithoutValidation(header.Key, header.Value))
				{
					request.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
				}
			}
		}
	}
}