using System;
using System.Net;

namespace CaptainPav.WebApiProxy
{
	/// <summary>
	/// Exception thrown when a response does not have the expected status code
	/// </summary>
	public class UnexpectedStatusCodeException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UnexpectedStatusCodeException"/> class
		/// </summary>
		public UnexpectedStatusCodeException() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="UnexpectedStatusCodeException"/> class
		/// </summary>
		/// <param name="message">
		/// Exception message
		/// </param>
		public UnexpectedStatusCodeException(string message) : this(message, null) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="UnexpectedStatusCodeException"/> class
		/// </summary>
		/// <param name="message">
		/// Exception message
		/// </param>
		/// <param name="innerEx">
		/// Inner exception being wrapped by this exception
		/// </param>
		public UnexpectedStatusCodeException(string message, Exception innerEx) : base(message, innerEx)
		{
		}

		/// <summary>
		/// Gets the collection of status codes which indicates a
		/// successful request.  If no codes are specified, then any 2xx
		/// code is considered successful.
		/// </summary>
		public HttpStatusCode[] ExpectedCodes { get; internal set; }

		/// <summary>
		/// Gets the status code observed in the response
		/// </summary>
		public HttpStatusCode ObservedCode { get; internal set; }
	}
}