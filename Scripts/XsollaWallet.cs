using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.IO;
using SimpleJSON;
using System.Runtime.InteropServices;

namespace Xsolla {

	public class XsollaJsonGenerator {

		public User user;
		public Settings settings;

		public XsollaJsonGenerator(string userId, long projectId){
			user = new User ();
			settings = new Settings ();
			user.id = userId;
			settings.id = projectId;
		}

		public string GetPrepared(){
			StringBuilder builder = new StringBuilder();		
			builder.Append ("{")
				.Append ("\"user\":{")
					.Append ("\"id\":{").Append ("\"value\":\"").Append (user.id).Append ("\"}").Append(",");
					if(user.name != null)
						builder.Append("\"name\":{").Append("\"value\":\"").Append(user.name).Append("\"}").Append(",");
					if(user.email != null)
						builder.Append("\"email\":{").Append("\"value\":\"").Append(user.email).Append("\"}").Append(",");
					if (user.country != null) { 
						builder.Append ("\"country\":{")
							.Append ("\"value\":\"").Append (user.country).Append ("\"").Append (",")
								.Append ("\"allow_modify\":").Append (true)
								.Append ("}").Append (",");
					}
				builder.Length--;
				builder.Append ("}").Append(",");
				builder.Append ("\"settings\":{")
					.Append ("\"project_id\":").Append (settings.id).Append(",");
					if(settings.languge != null)
						builder.Append("\"language\":\"").Append(settings.languge).Append("\"").Append(",");
					if(settings.currency != null)
						builder.Append("\"currency\":\"").Append(settings.currency).Append("\"").Append(",");
					if (settings.mode == "sandbox")
						builder.Append ("\"mode\":\"sandbox\",");
					if(settings.secretKey != null)
						builder.Append("\"secretKey\":\"").Append(settings.secretKey).Append("\"").Append(",");
				builder.Length--;
				builder.Append("}")
			.Append("}");
			return builder.ToString();
		}

		public struct User {
			public string id;
			public string name;
			public string email;
			public string country;
		}

		public struct Settings {
			public long id;
			public string languge;
			public string currency;
			public string mode;
			public string secretKey;
		}

		public static IEnumerator FreshToken(Action<string> tokenCallback){
			Logger.isLogRequired = true;
			XsollaJsonGenerator generator = new XsollaJsonGenerator ("user_1", 14004);//test 15764
			generator.user.name = "John Smith";
			generator.user.email = "support@xsolla.com";
			generator.user.country = "US";
			generator.settings.currency = "USD";
			generator.settings.languge = "en";
			string request = generator.GetPrepared ();
			string url = "https://livedemo.xsolla.com/sdk/token/";
			WWWForm form = new WWWForm ();
			form.AddField ("data", request);

//			// Fill data List
//			Dictionary<string,string> dataArgs = new Dictionary<string, string>();
//			dataArgs.Add("data", request);
//
//
//			HttpTlsRequest httpRequest = new HttpTlsRequest();
//			// Get request output
//			string httpTlsOutput = httpRequest.Request(url, form, dataArgs);
//
//			if (httpTlsOutput != "")
//			{
//				JSONNode rootNode = JSON.Parse(httpTlsOutput);
//				tokenCallback (rootNode["token"].Value);
//			}
//			else
//			{
//				tokenCallback(null);
//			}
//
//			yield return null;




//			if (Application.platform == RuntimePlatform.LinuxPlayer)
//			{
//				Logger.Log("Application IF LinuxPlayer");
//			}
//			else if (Application.platform != RuntimePlatform.WebGLPlayer)
//			{
//				Logger.Log("ApplicationPlatform != WebGl");
//				string Arg = "https://livedemo.xsolla.com/sdk/token/?data=%7B%22user%22%3A%7B%22id%22%3A%7B%22value%22%3A%22user_1%22%2C%22allow_modify%22%3Afalse%7D%2C%22email%22%3A%7B%22value%22%3A%22support%40xsolla.com%22%2C%22allow_modify%22%3Afalse%7D%2C%22country%22%3A%7B%22value%22%3A%22US%22%2C%22allow_modify%22%3Atrue%7D%2C%22name%22%3A%7B%22value%22%3A%22John+Smit%22%2C%22allow_modify%22%3Afalse%7D%7D%2C%22settings%22%3A%7B%22language%22%3A%22en%22%2C%22currency%22%3A%22USD%22%2C%22project_id%22%3A14004%7D%7D";
//				int exitCode;
//				string output;
//				CaptureConsoleAppOutput(GetAppPlatformPath(), Arg, 10, out exitCode, out output);
//				Logger.Log(output);
//
//				if (exitCode == 0) {
//					JSONNode rootNode = JSON.Parse(output);
//					tokenCallback (rootNode["token"].Value);
//				} else {
//					tokenCallback(null);
//				}	
//			}
//			else 
//			{
//				WWW www = new WWW(url, form);
//				yield return www;
//				if (www.error == null) {
//					JSONNode rootNode = JSON.Parse(www.text);
//					tokenCallback (rootNode["token"].Value);
//				} else {
//					tokenCallback(null);
//				}
//			}
				

//#if !UNITY_WEBGL
//			string Arg = "https://livedemo.xsolla.com/sdk/token/?data=%7B%22user%22%3A%7B%22id%22%3A%7B%22value%22%3A%22user_1%22%2C%22allow_modify%22%3Afalse%7D%2C%22email%22%3A%7B%22value%22%3A%22support%40xsolla.com%22%2C%22allow_modify%22%3Afalse%7D%2C%22country%22%3A%7B%22value%22%3A%22US%22%2C%22allow_modify%22%3Atrue%7D%2C%22name%22%3A%7B%22value%22%3A%22John+Smit%22%2C%22allow_modify%22%3Afalse%7D%7D%2C%22settings%22%3A%7B%22language%22%3A%22en%22%2C%22currency%22%3A%22USD%22%2C%22project_id%22%3A14004%7D%7D";
//			int exitCode = -1;
//			string output = "";
//			CaptureConsoleAppOutput(GetAppPlatformPath(), Arg, 10, out exitCode, out output);
//			Logger.Log("DEBUG: !UnityWegLG" + output);
//			
//			if (exitCode == 0) {
//				JSONNode rootNode = JSON.Parse(output);
//				tokenCallback (rootNode["token"].Value);
//			} else {
//				tokenCallback(null);
//			}
//#elif UNITY_STANDALONE_WIN
//			string Arg = "https://livedemo.xsolla.com/sdk/token/?data=%7B%22user%22%3A%7B%22id%22%3A%7B%22value%22%3A%22user_1%22%2C%22allow_modify%22%3Afalse%7D%2C%22email%22%3A%7B%22value%22%3A%22support%40xsolla.com%22%2C%22allow_modify%22%3Afalse%7D%2C%22country%22%3A%7B%22value%22%3A%22US%22%2C%22allow_modify%22%3Atrue%7D%2C%22name%22%3A%7B%22value%22%3A%22John+Smit%22%2C%22allow_modify%22%3Afalse%7D%7D%2C%22settings%22%3A%7B%22language%22%3A%22en%22%2C%22currency%22%3A%22USD%22%2C%22project_id%22%3A14004%7D%7D";
//			int exitCode;
//			string output;
//			CaptureConsoleAppOutput(GetAppPlatformPath(), Arg, 10, out exitCode, out output);
//			Logger.Log("DEBUG: UnityStandAlone" + output);
//			
//			if (exitCode == 0) {
//				JSONNode rootNode = JSON.Parse(output);
//				tokenCallback (rootNode["token"].Value);
//			} else {
//				tokenCallback(null);
//			}
//#else
//			WWW www = new WWW(url, form);
//
//			yield return www;
//			if (www.error == null) {
//				Logger.Log("DEBUG: Else different" + www.text);
//				JSONNode rootNode = JSON.Parse(www.text);
//				tokenCallback (rootNode["token"].Value);
//			} else {
//				tokenCallback(null);
//			}
//#endif

			WWW www = new WWW(url, form);
			yield return www;
			if (www.error == null) {
				Logger.Log("DEBUG: Last section" + www.text);
				JSONNode rootNode = JSON.Parse(www.text);
				tokenCallback (rootNode["token"].Value);
			} else {
				tokenCallback(null);
			}

		}

		public static string Base64Encode(string plainText) {
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return System.Convert.ToBase64String(plainTextBytes);
		}

		static byte[] GetBytes(string str)
		{
			byte[] bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		private static string GetAppPlatformPath()
		{
			string plugName = "ExecConnect.dll";
			Logger.Log("ApplicationDataPath --> " + Application.dataPath);

			if (Application.platform == RuntimePlatform.Android)
				return "";
			else if(Application.platform == RuntimePlatform.OSXPlayer)
				return @"" + Application.dataPath +"/Plugins/" + plugName;
			else if(Application.platform == RuntimePlatform.OSXEditor)
				return @"Assets/Plugins/" + plugName;
			else if(Application.platform == RuntimePlatform.WindowsPlayer)
				return @"" + Application.dataPath +"/Plugins/" + "ExecConnectWin.dll";
			else 
				return "";
		}

		private static void CaptureConsoleAppOutput(string exeName, string arguments, int timeoutMilliseconds, out int exitCode, out string output)
		{
			ProcessStartInfo start = new ProcessStartInfo();
			start.FileName = exeName;
			start.Arguments = arguments;
			start.UseShellExecute = false;
			start.RedirectStandardOutput = true;
			start.CreateNoWindow = true;

			/*try
			{   // Open the text file using a stream reader.
				using (StreamReader sr = new StreamReader(exeName))
				{
					// Read the stream to a string, and write the string to the console.
					String line = sr.ReadToEnd();
					Console.WriteLine(line);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("The file could not be read:");
				Logger.Log(e.Message);
			};*/


			try
			{
				Process process = Process.Start(start);
				using (StreamReader reader = process.StandardOutput)
				{
					string result = reader.ReadToEnd();
					output = result;
				}
				
				bool exited = process.WaitForExit(timeoutMilliseconds);
				if (exited)
				{
					exitCode = process.ExitCode;
				}
				else
				{
					exitCode = -1;
				}
			}
			catch (Exception e)
			{
				Logger.Log(e.Message);
				exitCode = -1;
				output = "";
			}
		}
	}


	public class XsollaWallet : MonoBehaviour
	{
		
		private string token;
		
		private string userId;//"1234"
		private string userName;//"jhon"
		private string userEmail;//"a@b.ru"
		private string userCountry;//"US"
		private long projectId;// 15674
		private string language;//"en"
		private string currency;//"USD"
		
		private XsollaWallet(string token){
			this.token = token;
		}

		public string GetToken(){
			return token;
		}
		
		public string GetPrepared(){
			if(token != null) {
				return token;
			} else {
				StringBuilder builder = new StringBuilder();		
				builder.Append("{")
					.Append("\"user\":{")
						.Append("\"id\":{").Append("\"value\":").Append("").Append("}")
						.Append("\"email\":{").Append("\"value\":").Append("").Append("}")
						.Append("\"country\":{").Append("\"value\":").Append("").Append("}")
						.Append("}")
						.Append("\"settings\":{")
						.Append("\"project_id\":").Append("")
						.Append("\"language\":").Append("")
						.Append("\"currency\":").Append("")
						.Append("\"mode\":").Append("")
						.Append("}")
						.Append("}");
				return builder.ToString();
			}
		}
		
		public static class Factory
		{
			
			public static XsollaWallet CreateWallet(string access_token)
			{
				return new XsollaWallet (access_token);
			}
			
		}
		
	}
	
}
