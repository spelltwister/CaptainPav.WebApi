using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CaptainPav.WebApiProxy
{
	/// <summary>
	/// WebApi Proxy provides a way to use normal CLR types as inputs and outputs
	/// of the Proxy by converting inputs to HTTP Request and deserializing
	/// outputs from the Response messages.  The WebApi proxy assumes that all
	/// calls made against a specific proxy will use a common route prefix.  All
	/// methods exposed by the WebApi Proxy take the relative request Uri from
	/// the common route prefix.
	/// </summary>
	public interface IWebApiProxy
	{
		/// <summary>
		/// Performs a DELETE request against the remote server
		/// </summary>
		/// <param name="relativeRequestUri">
		/// The relative request uri from the route prefix
		/// </param>
		/// <param name="additionalRequestHeaders">
		/// Additional request headers to be sent with this request
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.
		/// </param>
		/// <param name="validCodes">
		/// Collection of <see cref="HttpStatusCode"/>s which represent
		/// a successful response
		/// </param>
		Task DeleteAsync(string relativeRequestUri = "", IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes);

		/// <summary>
		/// Performs a GET request against the remote server
		/// </summary>
		/// <typeparam name="TResult">
		/// Type of result to expect
		/// </typeparam>
		/// <param name="relativeRequestUri">
		/// The relative request uri from the route prefix
		/// </param>
		/// <param name="jsonTokenSelector">
		/// If set, identifies a node in the response which should be treated
		/// as the actual response body.
		/// </param>
		/// <param name="completionOption">
		/// An HTTP completion option value that indicates when the operation
		/// should be considered completed.
		/// </param>
		/// <param name="additionalRequestHeaders">
		/// Additional request headers to be sent with this request
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.
		/// </param>
		/// <param name="validCodes">
		/// Collection of <see cref="HttpStatusCode"/>s which represent
		/// a successful response
		/// </param>
		/// <returns>
		/// The <see cref="HttpResponseMessage"/> content body deserialized
		/// into the requested type.
		/// </returns>
		Task<TResult> GetAsync<TResult>(string relativeRequestUri = "", string jsonTokenSelector = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes);

		/// <summary>
		/// Performs a POST request against the remote server
		/// </summary>
		/// <param name="relativeRequestUri">
		/// The relative request uri from the route prefix
		/// </param>
		/// <param name="content">
		/// Content body to send with the request or null if no body
		/// should be sent
		/// </param>
		/// <param name="additionalRequestHeaders">
		/// Additional request headers to be sent with this request
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.
		/// </param>
		/// <param name="validCodes">
		/// Collection of <see cref="HttpStatusCode"/>s which represent
		/// a successful response
		/// </param>
		Task PostAsync(string relativeRequestUri = "", HttpContent content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes);

		/// <summary>
		/// Performs a POST request against the remote server serializing the
		/// content body as JSON
		/// </summary>
		/// <param name="relativeRequestUri">
		/// The relative request uri from the route prefix
		/// </param>
		/// <param name="content">
		/// Content body to JSON serialize and send with the request or null
		/// if no body should be sent
		/// </param>
		/// <param name="additionalRequestHeaders">
		/// Additional request headers to be sent with this request
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.
		/// </param>
		/// <param name="validCodes">
		/// Collection of <see cref="HttpStatusCode"/>s which represent
		/// a successful response
		/// </param>
		Task PostModelAsync(string relativeRequestUri = "", object content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes);

		/// <summary>
		/// Performs a POST request against the remote server
		/// </summary>
		/// <typeparam name="TResult">
		/// Type of result to expect
		/// </typeparam>
		/// <param name="relativeRequestUri">
		/// The relative request uri from the route prefix
		/// </param>
		/// <param name="content">
		/// Content body to send with the request or null if no body
		/// should be sent
		/// </param>
		/// <param name="jsonTokenSelector">
		/// If set, identifies a node in the response which should be treated
		/// as the actual response body.
		/// </param>
		/// <param name="additionalRequestHeaders">
		/// Additional request headers to be sent with this request
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.
		/// </param>
		/// <param name="validCodes">
		/// Collection of <see cref="HttpStatusCode"/>s which represent
		/// a successful response
		/// </param>
		/// <returns>
		/// The <see cref="HttpResponseMessage"/> content body deserialized
		/// into the requested type.
		/// </returns>
		Task<TResult> PostAsync<TResult>(string relativeRequestUri = "", HttpContent content = null, string jsonTokenSelector = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes);

		/// <summary>
		/// Performs a POST request against the remote server serializing the
		/// content body as JSON
		/// </summary>
		/// <param name="relativeRequestUri">
		/// The relative request uri from the route prefix
		/// </param>
		/// <param name="content">
		/// Content body to JSON serialize and send with the request or null
		/// if no body should be sent
		/// </param>
		/// <param name="jsonTokenSelector">
		/// If set, identifies a node in the response which should be treated
		/// as the actual response body.
		/// </param>
		/// <param name="additionalRequestHeaders">
		/// Additional request headers to be sent with this request
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.
		/// </param>
		/// <param name="validCodes">
		/// Collection of <see cref="HttpStatusCode"/>s which represent
		/// a successful response
		/// </param>
		/// <returns>
		/// The <see cref="HttpResponseMessage"/> content body deserialized
		/// into the requested type.
		/// </returns>
		Task<TResult> PostModelAsync<TResult>(string relativeRequestUri = "", object content = null, string jsonTokenSelector = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes);

		/// <summary>
		/// Performs a PUT request against the remote server
		/// </summary>
		/// <param name="relativeRequestUri">
		/// The relative request uri from the route prefix
		/// </param>
		/// <param name="content">
		/// Content body to send with the request or null if no body
		/// should be sent
		/// </param>
		/// <param name="additionalRequestHeaders">
		/// Additional request headers to be sent with this request
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.
		/// </param>
		/// <param name="validCodes">
		/// Collection of <see cref="HttpStatusCode"/>s which represent
		/// a successful response
		/// </param>
		Task PutAsync(string relativeRequestUri = "", HttpContent content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes);

		/// <summary>
		/// Performs a PUT request against the remote server serializing the
		/// content body as JSON
		/// </summary>
		/// <param name="relativeRequestUri">
		/// The relative request uri from the route prefix
		/// </param>
		/// <param name="content">
		/// Content body to JSON serialize and send with the request or null
		/// if no body should be sent
		/// </param>
		/// <param name="additionalRequestHeaders">
		/// Additional request headers to be sent with this request
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.
		/// </param>
		/// <param name="validCodes">
		/// Collection of <see cref="HttpStatusCode"/>s which represent
		/// a successful response
		/// </param>
		Task PutModelAsync(string relativeRequestUri = "", object content = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes);

		/// <summary>
		/// Performs a PUT request against the remote server serializing the
		/// content body as JSON
		/// </summary>
		/// <param name="relativeRequestUri">
		/// The relative request uri from the route prefix
		/// </param>
		/// <param name="content">
		/// Content body to send with the request or null
		/// if no body should be sent
		/// </param>
		/// <param name="jsonTokenSelector">
		/// If set, identifies a node in the response which should be treated
		/// as the actual response body.
		/// </param>
		/// <param name="additionalRequestHeaders">
		/// Additional request headers to be sent with this request
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.
		/// </param>
		/// <param name="validCodes">
		/// Collection of <see cref="HttpStatusCode"/>s which represent
		/// a successful response
		/// </param>
		/// <returns>
		/// The <see cref="HttpResponseMessage"/> content body deserialized
		/// into the requested type.
		/// </returns>
		Task<TResult> PutAsync<TResult>(string relativeRequestUri = "", HttpContent content = null, string jsonTokenSelector = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes);

		/// <summary>
		/// Performs a PUT request against the remote server serializing the
		/// content body as JSON
		/// </summary>
		/// <param name="relativeRequestUri">
		/// The relative request uri from the route prefix
		/// </param>
		/// <param name="content">
		/// Content body to JSON serialize and send with the request or null
		/// if no body should be sent
		/// </param>
		/// <param name="jsonTokenSelector">
		/// If set, identifies a node in the response which should be treated
		/// as the actual response body.
		/// </param>
		/// <param name="additionalRequestHeaders">
		/// Additional request headers to be sent with this request
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.
		/// </param>
		/// <param name="validCodes">
		/// Collection of <see cref="HttpStatusCode"/>s which represent
		/// a successful response
		/// </param>
		/// <returns>
		/// The <see cref="HttpResponseMessage"/> content body deserialized
		/// into the requested type.
		/// </returns>
		Task<TResult> PutModelAsync<TResult>(string relativeRequestUri = "", object content = null, string jsonTokenSelector = null, IEnumerable<KeyValuePair<string, IEnumerable<string>>> additionalRequestHeaders = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes);

		/// <summary>
		/// Sends a request to the remote server
		/// </summary>
		/// <param name="requestMessage">
		/// The request to send
		/// </param>
		/// <param name="completionOption">
		/// An HTTP completion option value that indicates when the operation
		/// should be considered completed.
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.
		/// </param>
		/// <param name="validCodes">
		/// Collection of <see cref="HttpStatusCode"/>s which represent
		/// a successful response
		/// </param>
		Task SendAsync(HttpRequestMessage requestMessage, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes);

		/// <summary>
		/// Sends a request to the remote server
		/// </summary>
		/// <param name="requestMessage">
		/// The request to send
		/// </param>
		/// <param name="completionOption">
		/// An HTTP completion option value that indicates when the operation
		/// should be considered completed.
		/// </param>
		/// <param name="jsonTokenSelector">
		/// If set, identifies a node in the response which should be treated
		/// as the actual response body.
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used by other objects or threads to receive
		/// notice of cancellation.
		/// </param>
		/// <param name="validCodes">
		/// Collection of <see cref="HttpStatusCode"/>s which represent
		/// a successful response
		/// </param>
		/// <returns>
		/// The <see cref="HttpResponseMessage"/> content body deserialized
		/// into the requested type.
		/// </returns>
		Task<TResult> SendAsync<TResult>(HttpRequestMessage requestMessage, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, string jsonTokenSelector = null, CancellationToken cancellationToken = default(CancellationToken), params HttpStatusCode[] validCodes);
	}
}