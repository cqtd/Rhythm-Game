using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Rhythm
{
	public sealed class AudioDownloader : MonoSingleton<AudioDownloader>
	{
		protected override void Initialize() { }

		public void GetAudioClip(string path, Action<AudioClip> callback, AudioType type = AudioType.OGGVORBIS)
		{
			StartCoroutine(GetAudioClipCoroutine(path, callback, type));
		}

		private IEnumerator GetAudioClipCoroutine(string path, Action<AudioClip> callback, AudioType type)
		{
			UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, type);
			yield return www.SendWebRequest();

			if (www.downloadHandler.data.Length != 0)
			{
				AudioClip c = DownloadHandlerAudioClip.GetContent(www);
				c.LoadAudioData();

				callback?.Invoke(c);
			}
			else
			{
				callback?.Invoke(null);
			}
		}
	}
}