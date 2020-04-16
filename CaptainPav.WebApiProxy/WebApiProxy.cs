using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CaptainPav.WebApiProxy
{
	public class WebApiProxy : IWebApiProxy
	{
		protected RoutePrefixedHttpClientWrapper Client { get; }

		protected IHttpContentConverter ContentConverter { get; }

		protected IHttpResponseMessageParser ResponseParser { get; }

		public WebApiProxy(
			HttpClient client,
			string routePrefix,
			IHttpResponseMessageParser messageParser,
			IHttpContentConverter converter)
			: this(new RoutePrefixedHttpClientWrapper(client, routePrefix), messageParser, converter)
		{
		}

		public WebApiProxy(RoutePrefixedHttpClientWrapper client, IHttpResponseMessageParser messageParser, IHttpContentConverter converter)
		{
			this.Client = client;
			this.ResponseParser = messageParser;
			this.ContentConverter = converter;
		}

		public async Task DeleteAsync(
			string relativeRequestUri = null,
			IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null,
			CancellationToken cancellationToken = default(CancellationToken),
			params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage deleteResponse = await this.Client.DeleteAsync(relativeRequestUri, additionalRequestHeaders, cancellationToken).ConfigureAwait(false);
			await this.ResponseParser.ParseAsync(deleteResponse, validCodes).ConfigureAwait(false);
		}

		public async Task<TResult> GetAsync<TResult>(
			string relativeRequestUri = null,
			string jsonTokenSelector = null,
			HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
			IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null,
			CancellationToken cancellationToken = default(CancellationToken),
			params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage getResponse = await this.Client
				.GetAsync(relativeRequestUri, completionOption, additionalRequestHeaders, cancellationToken)
				.ConfigureAwait(false);

			return await this.ResponseParser.ParseAsync<TResult>(getResponse, jsonTokenSelector, validCodes).ConfigureAwait(false);
		}

		public async Task PostAsync(
			string relativeRequestUri = null,
			HttpContent content = null,
			IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null,
			CancellationToken cancellationToken = default(CancellationToken),
			params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage postResponse = await this.Client
				.PostAsync(relativeRequestUri, content, additionalRequestHeaders, cancellationToken)
				.ConfigureAwait(false);

			await this.ResponseParser.ParseAsync(postResponse, validCodes).ConfigureAwait(false);
		}

		public async Task<TResult> PostAsync<TResult>(
			string relativeRequestUri = null,
			HttpContent content = null,
			string jsonTokenSelector = null,
			IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null,
			CancellationToken cancellationToken = default(CancellationToken),
			params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage postResponse = await this.Client
				.PostAsync(relativeRequestUri, content, additionalRequestHeaders, cancellationToken)
				.ConfigureAwait(false);

			return await this.ResponseParser.ParseAsync<TResult>(postResponse, jsonTokenSelector, validCodes).ConfigureAwait(false);
		}

		public async Task PostModelAsync(
			string relativeRequestUri = null,
			object content = null,
			IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null,
			CancellationToken cancellationToken = default(CancellationToken),
			params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage postResponse = await this.Client
				.PostModelAsync(relativeRequestUri, content, this.ContentConverter, additionalRequestHeaders, cancellationToken)
				.ConfigureAwait(false);

			await this.ResponseParser.ParseAsync(postResponse, validCodes).ConfigureAwait(false);
		}

		public async Task<TResult> PostModelAsync<TResult>(
			string relativeRequestUri = null,
			object content = null,
			string jsonTokenSelector = null,
			IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null,
			CancellationToken cancellationToken = default(CancellationToken),
			params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage postResponse = await this.Client
				.PostModelAsync(relativeRequestUri, content, this.ContentConverter, additionalRequestHeaders, cancellationToken)
				.ConfigureAwait(false);

			return await this.ResponseParser.ParseAsync<TResult>(postResponse, jsonTokenSelector, validCodes).ConfigureAwait(false);
		}

		public async Task PutAsync(
			string relativeRequestUri = null,
			HttpContent content = null,
			IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null,
			CancellationToken cancellationToken = default(CancellationToken),
			params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage putResponse = await this.Client
				.PutAsync(relativeRequestUri, content, additionalRequestHeaders, cancellationToken)
				.ConfigureAwait(false);

			await this.ResponseParser.ParseAsync(putResponse, validCodes).ConfigureAwait(false);
		}

		public async Task<TResult> PutAsync<TResult>(
			string relativeRequestUri = null,
			HttpContent content = null,
			string jsonTokenSelector = null,
			IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null,
			CancellationToken cancellationToken = default(CancellationToken),
			params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage putResponse = await this.Client
				.PutAsync(relativeRequestUri, content, additionalRequestHeaders, cancellationToken)
				.ConfigureAwait(false);

			return await this.ResponseParser.ParseAsync<TResult>(putResponse, jsonTokenSelector, validCodes).ConfigureAwait(false);
		}

		public async Task PutModelAsync(
			string relativeRequestUri = null,
			object content = null,
			IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null,
			CancellationToken cancellationToken = default(CancellationToken),
			params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage postResponse = await this.Client
				.PutModelAsync(relativeRequestUri, content, this.ContentConverter, additionalRequestHeaders, cancellationToken)
				.ConfigureAwait(false);

			await this.ResponseParser.ParseAsync(postResponse, validCodes).ConfigureAwait(false);
		}

		public async Task<TResult> PutModelAsync<TResult>(
			string relativeRequestUri = null,
			object content = null,
			string jsonTokenSelector = null,
			IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null,
			CancellationToken cancellationToken = default(CancellationToken),
			params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage putResponse = await this.Client
				.PutModelAsync(relativeRequestUri, content, this.ContentConverter, additionalRequestHeaders, cancellationToken)
				.ConfigureAwait(false);

			return await this.ResponseParser.ParseAsync<TResult>(putResponse, jsonTokenSelector, validCodes).ConfigureAwait(false);
		}

		public async Task SendAsync(
			HttpRequestMessage requestMessage,
			HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
			CancellationToken cancellationToken = default(CancellationToken),
			params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage sendResponse = await this.Client
				.SendAsync(requestMessage, completionOption, cancellationToken)
				.ConfigureAwait(false);

			await this.ResponseParser.ParseAsync(sendResponse, validCodes).ConfigureAwait(false);
		}

		public async Task<TResult> SendAsync<TResult>(
			HttpRequestMessage requestMessage,
			HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
			string jsonTokenSelector = null,
			CancellationToken cancellationToken = default(CancellationToken),
			params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage sendResponse = await this.Client
				.SendAsync(requestMessage, completionOption, cancellationToken)
				.ConfigureAwait(false);

			return await this.ResponseParser.ParseAsync<TResult>(sendResponse, jsonTokenSelector, validCodes).ConfigureAwait(false);
		}
	}
}