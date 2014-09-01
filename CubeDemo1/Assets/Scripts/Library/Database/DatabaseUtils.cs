using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

//////////////////////////////////////////
/// DatabaseUtils
/// Functions for communicating with a 
/// database should go in here.
//////////////////////////////////////////

public class DatabaseUtils {
	//////////////////////////////////////////
	/// SerializeObject()
	/// One serialization function to rule
	/// them all.  Try to use this one because
	/// it uses the proper JSON coverting
	/// settings.
	//////////////////////////////////////////
	public static string SerializeObject<T>( T data ) {
		string strJSON = JsonConvert.SerializeObject(data, new Newtonsoft.Json.Converters.StringEnumConverter());
		return strJSON;
	}

	//////////////////////////////////////////
	/// SendPost()
	/// Send a POST by deserializing the
	/// incoming data into JSON and sending it
	/// to i_strURL.
	/// If i_nAttempts is -1 it means try
	/// infinitely.  1 (default) means try once.
	/// And any other number will try that many
	/// times before giving up.
	//////////////////////////////////////////
	public static IEnumerator SendPost<T>( T data, string i_strURL, Action<WWW> callback, int i_nAttemptsReamining = 1, bool i_bTesting = false ) {
		// turn our data into a JSON string
		string strJSON = SerializeObject<T>(data);

		yield return DatabaseManager.Instance.StartCoroutine(SendPost(strJSON, i_strURL, callback, i_nAttemptsReamining, i_bTesting));
	}
	public static IEnumerator SendPost( string i_strJSON, string i_strURL, Action<WWW> callback, int i_nAttemptsReamining = 1, bool i_bTesting = false ) {
		string strURL = i_strURL;

		// if we are in a dev build, prefix the url
		if ( i_bTesting )
			strURL = Constants.GetConstant<string>("ServerRoot_Testing") + strURL;
		else if (UnityEngine.Debug.isDebugBuild)
			strURL = Constants.GetConstant<string>("ServerRoot") + strURL;
		else {
			string strPrefix = Constants.GetConstant<string>("URL_Prefix");
			strURL = strPrefix + i_strURL;
		}

		Debug.Log("Sending a POST to " + strURL);
		
		Debug.Log("POST JSON is " + i_strJSON);
		
		// header stuff that must be include din every post
		Hashtable headers = new Hashtable();
		headers["Content-Type"] = "application/json";	

		// create www to post the data
		WWW post = new WWW(strURL, System.Text.Encoding.UTF8.GetBytes(i_strJSON), headers);

		// wait for the post to be done
		yield return post;

		Debug.Log("POST completed(" + strURL + ")");

		// did this POST fail?
		bool bFailed = false;

		if (!string.IsNullOrEmpty(post.error)) {
			// something went wrong with the WWW class itself
			Debug.Log("Error while POSTing(" + strURL + ")" + ": " + post.error);
			bFailed = true;
		}
		else {
			Debug.Log("POST return is(" + strURL + "): " + post.text);

			// deserializing the post text into our return type
			SD_PostReturn ret = JsonConvert.DeserializeObject<SD_PostReturn>(post.text);

			// something may have gone wrong with the database; catch that error here
			bFailed = !ret.Success;
		}

		// don't bothter to check for failures on dev builds...could be lamo DB stuff that we don't care about
		if (bFailed && !UnityEngine.Debug.isDebugBuild) {
			// if we failed, decrement our attempts and try again (if appropriate)
			i_nAttemptsReamining -= 1;

			if ( i_nAttemptsReamining != 0 ) {
				// wait a short period
				float fWait = Constants.GetConstant<float>("ReattemptWaitTime");
				yield return new WaitForSeconds(fWait);

				yield return DatabaseManager.Instance.StartCoroutine(DatabaseUtils.SendPost(i_strJSON, i_strURL, callback, i_nAttemptsReamining-1, false));

				// break here because the other coroutines will use the final/real callback
				yield break;
			}
		}

		// use a fancy Action + lambda callback to send the post back to whatever called this function
		callback(post);
	}
}
