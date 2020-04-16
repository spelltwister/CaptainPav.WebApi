using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace CaptainPav.WebApiProxy
{
	public static class HttpResponseMessageExtensions
	{
		/// <summary>
		/// Determines if the response should be considered successful based on the
		/// response status code.
		/// </summary>
		/// <param name="response">
		/// The response to determine if successful
		/// </param>
		/// <param name="validCodes">
		/// The HttpStatus codes which represent success.  If not set or empty,
		/// the default set of status codes will be used.
		/// </param>
		/// <returns>
		/// true if the response should be considered successful based on the
		/// response status code; otherwise, false
		/// </returns>
		public static bool IsResponseSuccessful(this HttpResponseMessage response, params HttpStatusCode[] validCodes)
		{
			return (validCodes.Length > 0 && validCodes.Any(x => response.StatusCode == x)) ||
				   (validCodes.Length == 0 && response.IsSuccessStatusCode);
		}

		/// <summary>
		/// Throws an <see cref="UnexpectedStatusCodeException"/> if the <see cref="HttpResponseMessage"/>'s
		/// StatusCode property is not in the valid codes collection
		/// </summary>
		/// <param name="response">
		/// The response to determine if successful
		/// </param>
		/// <param name="innerException">
		/// The exception to wrap
		/// </param>
		/// <param name="validCodes">
		/// The HttpStatus codes which represent success.  If not set or empty,
		/// the default set of status codes will be used.
		/// </param>
		/// <exception cref="UnexpectedStatusCodeException">
		/// Thrown when the <see cref="HttpResponseMessage"/>'s StatusCode property
		/// is not in the valid codes collection
		/// </exception>
		public static UnexpectedStatusCodeException CreateExceptionIfNotSuccessful(this HttpResponseMessage response, Exception innerException = null, params HttpStatusCode[] validCodes)
		{
			if (!IsResponseSuccessful(response, validCodes))
			{
				string expectedCodes = 
					validCodes.Length > 1 
						? $"[{String.Join(", ", validCodes)}]" 
						: (validCodes.Length == 1 
							? validCodes[0].ToString()
							: "2xx");

				string defaultMessage = $"The status code returned was unexpected.  Actual: `{response.StatusCode}`.  Expected: `{expectedCodes}`";

				UnexpectedStatusCodeException ret = innerException == null ? new UnexpectedStatusCodeException(defaultMessage) : new UnexpectedStatusCodeException(defaultMessage, innerException);
				ret.ExpectedCodes = validCodes;
				ret.ObservedCode = response.StatusCode;
				return ret;
			}

			return null;
		}
	}
}