using System;

namespace CaptainPav.WebApiProxy
{	
	/// <summary>
	/// Exception thrown when the response was expected to contain data
	/// that was not present
	/// </summary>
	public class ResponseTokenNotFoundException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ResponseTokenNotFoundException"/> class
		/// </summary>
		public ResponseTokenNotFoundException() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ResponseTokenNotFoundException"/> class
		/// </summary>
		/// <param name="message">
		/// Exception message
		/// </param>
		public ResponseTokenNotFoundException(string message) : this(message, null) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ResponseTokenNotFoundException"/> class
		/// </summary>
		/// <param name="message">
		/// Exception message
		/// </param>
		/// <param name="innerEx">
		/// Inner exception being wrapped by this exception
		/// </param>
		public ResponseTokenNotFoundException(string message, Exception innerEx) : base(message, innerEx) { }
	}
}