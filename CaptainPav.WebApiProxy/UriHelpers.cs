using System;

namespace CaptainPav.WebApiProxy
{	
	/// <summary>
	/// Helper methods for generating <see cref="Uri"/>s and relative api paths
	/// </summary>
	public static class UriHelpers
	{
		/// <summary>
		/// Gets relative api path for the specified api endpoint
		/// </summary>
		/// <param name="routePrefix">
		/// The route prefix used to create the relative api path
		/// </param>
		/// <param name="relativeEndpointUrl">
		/// Endpoint url relative to the api path
		/// </param>
		/// <returns>
		/// The relative api path
		/// </returns>
		/// <remarks>
		/// This method effectively pre-pends the ApiPath to the relative
		/// endpoint url, ensuring that the empty string relative path
		/// does not include a trailing '/'.
		/// </remarks>
		public static string RelativeApiPath(string routePrefix, string relativeEndpointUrl = null)
		{
			if (null == relativeEndpointUrl)
			{
				return routePrefix;
			}

			return $"{routePrefix}/{EnsureNoLeadingSlash(relativeEndpointUrl)}";
		}

		/// <summary>
		/// Gets the relative endpoint uri for the specified api endpoint
		/// </summary>
		/// <param name="routePrefix">
		/// The route prefix used to create the relative api path
		/// </param>
		/// <param name="relativeEndpointUrl">
		/// Endpoint url relative to the api path
		/// </param>
		/// <returns>
		/// The relative endpoint uri
		/// </returns>
		/// <remarks>
		/// This method effectively pre-pends the ApiPath to the relative
		/// endpoint url, ensuring that the empty string relative path
		/// does not include a trailing '/'.
		/// </remarks>
		public static Uri RelativeEndpointUri(string routePrefix, string relativeEndpointUrl = null)
		{
			return new Uri(RelativeApiPath(routePrefix, relativeEndpointUrl), UriKind.Relative);
		}

		/// <summary>
		/// Returns the input string, adding a '/' to the end
		/// if one does not exist
		/// </summary>
		/// <param name="input">
		/// The string to ensure ends with a trailing slash
		/// </param>
		/// <returns>
		/// The input string, adding a '/' to the end
		/// if one does not exist
		/// </returns>
		public static string EnsureTrailingSlash(string input)
		{
			string temp = input;
			if (temp.EndsWith("/"))
			{
				return temp;
			}

			return $"{temp}/";
		}

		/// <summary>
		/// Returns the input string, skipping any leading '/' characters
		/// </summary>
		/// <param name="input">
		/// The string to ensure does not start with a slash
		/// </param>
		/// <returns>
		/// The input string, skipping any leading '/' characters
		/// </returns>
		public static string EnsureNoLeadingSlash(string input)
		{
			return input.TrimStart('/');
		}

		/// <summary>
		/// Returns the input string, removing any trailing '/' characters
		/// </summary>
		/// <param name="input">
		/// The string to ensure does not end with a slash
		/// </param>
		/// <returns>
		/// The input string, removing any trailing '/' characters
		/// </returns>
		public static string EnsureNoTrailingSlash(string input)
		{
			return input.TrimEnd('/');
		}
	}
}