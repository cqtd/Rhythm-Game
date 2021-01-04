using System;
using System.Collections;
using System.IO;
using Rhythm.BMP;
using UnityEngine;
using UnityEngine.Networking;

namespace Rhythm
{
	public sealed class TextureDownloader : MonoSingleton<TextureDownloader>
	{
		protected override void Initialize() { }

		public void GetTexture2D(string path, Action<Texture2D> callback)
		{
			string filePath = string.Empty;
			if (path.StartsWith("file://", StringComparison.OrdinalIgnoreCase))
			{
				filePath = path.Replace("file://", "");
			}

			if (!new FileInfo(filePath).Exists)
			{
				Debug.LogWarning($"Texture Path Invalid : {path}");
				callback?.Invoke(null);

				return;
			}

			if (path.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
			{
				StartCoroutine(DownloadBMP(path, callback));
			}
			else if (path.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
			{
				StartCoroutine(DownloadPNG(path, callback));
			}
			else if (path.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
			{
				StartCoroutine(DownloadJPG(path, callback));
			}
			else
			{
#if UNITY_EDITOR
				throw new Exception("No compatible file format");
#endif
			}
		}


		private IEnumerator DownloadBMP(string path, Action<Texture2D> onComplete)
		{
			UnityWebRequest www = UnityWebRequest.Get(path);

			yield return www.SendWebRequest();
			yield return new WaitUntil(() => www.isDone);

			BMPLoader loader = new BMPLoader();
			BMPImage img = loader.LoadBMP(www.downloadHandler.data, path);

			if (img != null)
			{
				Texture2D texture = img.ToTexture2D();
				onComplete?.Invoke(texture);
			}
			else
			{
				onComplete?.Invoke(null);
			}

			www?.Dispose();
		}

		private IEnumerator DownloadPNG(string path, Action<Texture2D> onComplete)
		{
			UnityWebRequest www = UnityWebRequestTexture.GetTexture(path);
			yield return www.SendWebRequest();

			Texture2D texture = (www.downloadHandler as DownloadHandlerTexture)?.texture;
			onComplete?.Invoke(texture);

			www?.Dispose();
		}

		private IEnumerator DownloadJPG(string path, Action<Texture2D> onComplete)
		{
			UnityWebRequest www = UnityWebRequestTexture.GetTexture(path);
			yield return www.SendWebRequest();

			yield return new WaitUntil(() => www.isDone);

#if DEVELOPMENT_BUILD || UNITY_EDITOR
			try
			{
#endif
				Texture2D texture = (www.downloadHandler as DownloadHandlerTexture)?.texture;
				onComplete?.Invoke(texture);

#if DEVELOPMENT_BUILD || UNITY_EDITOR
			}
			catch (Exception e)
			{
				Debug.LogWarning(e);
				onComplete?.Invoke(null);
			}
#endif
			www?.Dispose();
		}
	}
}