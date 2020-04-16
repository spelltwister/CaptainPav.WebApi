using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CaptainPav.WebApiProxy
{
	public class DynamicHeaderWebApiProxyWrapper : IWebApiProxy
	{
		protected IWebApiProxy Proxy { get; }

		protected IDynamicHeaderProvider HeaderProvider { get; }

		public DynamicHeaderWebApiProxyWrapper(IWebApiProxy proxy, IDynamicHeaderProvider headerProvider)
		{
			this.Proxy = proxy;
			this.HeaderProvider = headerProvider;
		}

		public Task DeleteAsync(string relativeRequestUri = "", IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			additionalRequestHeaders = this.HeaderProvider.MergeHeaders(additionalRequestHeaders);
			return this.Proxy.DeleteAsync(relativeRequestUri, additionalRequestHeaders, cancellationToken, validCodes);
		}

		public Task<TResult> GetAsync<TResult>(string relativeRequestUri = "", string jsonTokenSelector = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			additionalRequestHeaders = this.HeaderProvider.MergeHeaders(additionalRequestHeaders);
			return Proxy.GetAsync<TResult>(relativeRequestUri, jsonTokenSelector, completionOption, additionalRequestHeaders, cancellationToken, validCodes);
		}

		public Task PostAsync(string relativeRequestUri = "", HttpContent content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			additionalRequestHeaders = this.HeaderProvider.MergeHeaders(additionalRequestHeaders);
			return Proxy.PostAsync(relativeRequestUri, content, additionalRequestHeaders, cancellationToken, validCodes);
		}

		public Task PostModelAsync(string relativeRequestUri = "", object content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			additionalRequestHeaders = this.HeaderProvider.MergeHeaders(additionalRequestHeaders);
			return Proxy.PostModelAsync(relativeRequestUri, content, additionalRequestHeaders, cancellationToken, validCodes);
		}

		public Task<TResult> PostAsync<TResult>(string relativeRequestUri = "", HttpContent content = null, string jsonTokenSelector = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			additionalRequestHeaders = this.HeaderProvider.MergeHeaders(additionalRequestHeaders);
			return Proxy.PostAsync<TResult>(relativeRequestUri, content, jsonTokenSelector, additionalRequestHeaders, cancellationToken, validCodes);
		}

		public Task<TResult> PostModelAsync<TResult>(string relativeRequestUri = "", object content = null, string jsonTokenSelector = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			additionalRequestHeaders = this.HeaderProvider.MergeHeaders(additionalRequestHeaders);
			return Proxy.PostModelAsync<TResult>(relativeRequestUri, content, jsonTokenSelector, additionalRequestHeaders, cancellationToken, validCodes);
		}

		public Task PutAsync(string relativeRequestUri = "", HttpContent content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			additionalRequestHeaders = this.HeaderProvider.MergeHeaders(additionalRequestHeaders);
			return Proxy.PutAsync(relativeRequestUri, content, additionalRequestHeaders, cancellationToken, validCodes);
		}

		public Task PutModelAsync(string relativeRequestUri = "", object content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			additionalRequestHeaders = this.HeaderProvider.MergeHeaders(additionalRequestHeaders);
			return Proxy.PutModelAsync(relativeRequestUri, content, additionalRequestHeaders, cancellationToken, validCodes);
		}

		public Task<TResult> PutAsync<TResult>(string relativeRequestUri = "", HttpContent content = null, string jsonTokenSelector = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			additionalRequestHeaders = this.HeaderProvider.MergeHeaders(additionalRequestHeaders);
			return Proxy.PutAsync<TResult>(relativeRequestUri, content, jsonTokenSelector, additionalRequestHeaders, cancellationToken, validCodes);
		}

		public Task<TResult> PutModelAsync<TResult>(string relativeRequestUri = "", object content = null, string jsonTokenSelector = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			additionalRequestHeaders = this.HeaderProvider.MergeHeaders(additionalRequestHeaders);
			return Proxy.PutModelAsync<TResult>(relativeRequestUri, content, jsonTokenSelector, additionalRequestHeaders, cancellationToken, validCodes);
		}

		public Task SendAsync(HttpRequestMessage requestMessage,
			HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
			CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			throw new System.NotImplementedException();
		}

		public Task<TResult> SendAsync<TResult>(HttpRequestMessage requestMessage,
			HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, string jsonTokenSelector = null,
			CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes)
		{
			throw new System.NotImplementedException();
		}
	}
}