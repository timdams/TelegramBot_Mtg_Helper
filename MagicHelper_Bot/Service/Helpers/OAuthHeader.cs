﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MagicHelper_Bot.Service.Helpers
{
	/// <summary>
	/// Class encapsulates tokens and secret to create OAuth signatures and return Authorization headers for web requests.
	/// </summary>
	class OAuthHeader
	{
		/// <summary>App Token</summary>
		protected String appToken = APIToken.MCM_APP;
		/// <summary>App Secret</summary>
		protected String appSecret = APIToken.MCM_S;
		/// <summary>Access Token (Class should also implement an AccessToken property to set the value)</summary>
		protected String accessToken = String.Empty;
		/// <summary>Access Token Secret (Class should also implement an AccessToken property to set the value)</summary>
		protected String accessSecret = String.Empty;
		/// <summary>OAuth Signature Method</summary>
		protected String signatureMethod = "HMAC-SHA1";
		/// <summary>OAuth Version</summary>
		protected String version = "1.0";
		/// <summary>All Header params compiled into a Dictionary</summary>
		protected IDictionary<String, String> headerParams;

		/// <summary>
		/// Constructor
		/// </summary>
		public OAuthHeader ()
		{
			String nonce = Guid.NewGuid ().ToString ("n");
			String timestamp = (DateTime.UtcNow.Subtract (new DateTime (1970, 1, 1))).TotalSeconds.ToString ();
			headerParams = new Dictionary<String, String> ();
			headerParams.Add ("oauth_consumer_key", appToken);
			headerParams.Add ("oauth_token", accessToken);
			headerParams.Add ("oauth_nonce", nonce);
			headerParams.Add ("oauth_timestamp", timestamp);
			headerParams.Add ("oauth_signature_method", signatureMethod);
			headerParams.Add ("oauth_version", version);
		}

		/// <summary>
		/// Pass request method and URI parameters to get the Authorization header value
		/// </summary>
		/// <param name="method">Request Method</param>
		/// <param name="url">Request URI</param>
		/// <returns>Authorization header value</returns>
		public String getAuthorizationHeader (String method, String url)
		{
			// Add the realm parameter to the header params
			headerParams.Add ("realm", url);

			// Start composing the base string from the method and request URI
			String baseString = method.ToUpper ()
			                    + "&"
			                    + Uri.EscapeDataString (url)
			                    + "&";

			// Gather, encode, and sort the base string parameters
			var encodedParams = new SortedDictionary<String, String> ();
			foreach (KeyValuePair<String, String> parameter in headerParams) {
				if (!parameter.Key.Equals ("realm")) {
					encodedParams.Add (Uri.EscapeDataString (parameter.Key), Uri.EscapeDataString (parameter.Value));
				}
			}

			// Expand the base string by the encoded parameter=value pairs
			var paramStrings = new List<String> ();
			foreach (KeyValuePair<String, String> parameter in encodedParams) {
				paramStrings.Add (parameter.Key + "=" + parameter.Value);
			}
			String paramString = Uri.EscapeDataString (String.Join<String> ("&", paramStrings));
			baseString += paramString;

			// Create the OAuth signature
			String signatureKey = Uri.EscapeDataString (appSecret) + "&" + Uri.EscapeDataString (accessSecret);
			HMAC hasher = HMAC.Create ();
			hasher.Key = Encoding.UTF8.GetBytes (signatureKey);
			Byte[] rawSignature = hasher.ComputeHash (Encoding.UTF8.GetBytes (baseString));
			String oAuthSignature = Convert.ToBase64String (rawSignature);

			// Include the OAuth signature parameter in the header parameters array
			headerParams.Add ("oauth_signature", oAuthSignature);

			// Construct the header string
			var headerParamStrings = new List<String> ();
			foreach (KeyValuePair<String, String> parameter in headerParams) {
				headerParamStrings.Add (parameter.Key + "=\"" + parameter.Value + "\"");
			}
			String authHeader = "OAuth " + String.Join<String> (", ", headerParamStrings);

			return authHeader;
		}
	}
}

