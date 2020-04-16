using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CaptainPav.WebApiProxy
{	
	public class AbsoluteRouteWebApiProxy : IWebApiProxy
	{
		protected HttpClient Client { get; }
		protected IHttpResponseMessageParser ResponseParser { get; }
		protected IHttpContentConverter ContentConverter { get; }

		public AbsoluteRouteWebApiProxy(HttpClient client, IHttpResponseMessageParser messageParser, IHttpContentConverter converter)
		{
			this.Client = client;
			this.ResponseParser = messageParser;
			this.ContentConverter = converter;
		}

		public async Task DeleteAsync(string absoluteRequestUri = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage deleteResponse = await this.Client.DeleteAsync(absoluteRequestUri, additionalRequestHeaders, cancellationToken).ConfigureAwait(false);
			await this.ResponseParser.ParseAsync(deleteResponse, validCodes).ConfigureAwait(false);
		}

		public async Task<TResult> GetAsync<TResult>(string absoluteRequestUri = null, string jsonTokenSelector = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage getResponse = await this.Client.GetAsync(absoluteRequestUri, completionOption, additionalRequestHeaders, cancellationToken).ConfigureAwait(false);
			return await this.ResponseParser.ParseAsync<TResult>(getResponse, jsonTokenSelector, validCodes).ConfigureAwait(false);
		}

		public async Task PostAsync(string absoluteRequestUri = null, HttpContent content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage postResponse = await this.Client.PostAsync(absoluteRequestUri, content, additionalRequestHeaders, cancellationToken).ConfigureAwait(false);
			await this.ResponseParser.ParseAsync(postResponse, validCodes).ConfigureAwait(false);
		}

		public async Task PostModelAsync(string absoluteRequestUri = null, object content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage postResponse = await this.Client.PostModelAsync(absoluteRequestUri, content, this.ContentConverter, additionalRequestHeaders, cancellationToken).ConfigureAwait(false);
			await this.ResponseParser.ParseAsync(postResponse, validCodes).ConfigureAwait(false);
		}

		public async Task<TResult> PostAsync<TResult>(string absoluteRequestUri = null, HttpContent content = null, string jsonTokenSelector = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage postResponse = await this.Client.PostAsync(absoluteRequestUri, content, additionalRequestHeaders, cancellationToken).ConfigureAwait(false);
			return await this.ResponseParser.ParseAsync<TResult>(postResponse, jsonTokenSelector, validCodes).ConfigureAwait(false);
		}

		public async Task<TResult> PostModelAsync<TResult>(string absoluteRequestUri = null, object content = null, string jsonTokenSelector = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage postResponse = await this.Client.PostModelAsync(absoluteRequestUri, content, this.ContentConverter, additionalRequestHeaders, cancellationToken).ConfigureAwait(false);
			return await this.ResponseParser.ParseAsync<TResult>(postResponse, jsonTokenSelector, validCodes).ConfigureAwait(false);
		}

		public async Task PutAsync(string absoluteRequestUri = null, HttpContent content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage putResponse = await this.Client.PutAsync(absoluteRequestUri, content, additionalRequestHeaders, cancellationToken).ConfigureAwait(false);
			await this.ResponseParser.ParseAsync(putResponse, validCodes).ConfigureAwait(false);
		}

		public async Task PutModelAsync(string absoluteRequestUri = null, object content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage postResponse = await this.Client.PutModelAsync(absoluteRequestUri, content, this.ContentConverter, additionalRequestHeaders, cancellationToken).ConfigureAwait(false);
			await this.ResponseParser.ParseAsync(postResponse, validCodes).ConfigureAwait(false);
		}

		public async Task<TResult> PutAsync<TResult>(string absoluteRequestUri = null, HttpContent content = null, string jsonTokenSelector = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage putResponse = await this.Client.PutAsync(absoluteRequestUri, content, additionalRequestHeaders, cancellationToken).ConfigureAwait(false);
			return await this.ResponseParser.ParseAsync<TResult>(putResponse, jsonTokenSelector, validCodes).ConfigureAwait(false);
		}

		public async Task<TResult> PutModelAsync<TResult>(string absoluteRequestUri = null, object content = null, string jsonTokenSelector = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage putResponse = await this.Client.PutModelAsync(absoluteRequestUri, content, this.ContentConverter, additionalRequestHeaders, cancellationToken).ConfigureAwait(false);
			return await this.ResponseParser.ParseAsync<TResult>(putResponse, jsonTokenSelector, validCodes).ConfigureAwait(false);
		}
		public async Task SendAsync(HttpRequestMessage requestMessage, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage sendResponse = await this.Client.SendAsync(requestMessage, completionOption, cancellationToken).ConfigureAwait(false);
			await this.ResponseParser.ParseAsync(sendResponse, validCodes).ConfigureAwait(false);
		}

		public async Task<TResult> SendAsync<TResult>(HttpRequestMessage requestMessage, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, string jsonTokenSelector = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			HttpResponseMessage sendResponse = await this.Client.SendAsync(requestMessage, completionOption, cancellationToken).ConfigureAwait(false);
			return await this.ResponseParser.ParseAsync<TResult>(sendResponse, jsonTokenSelector, validCodes).ConfigureAwait(false);
		}
	}
}