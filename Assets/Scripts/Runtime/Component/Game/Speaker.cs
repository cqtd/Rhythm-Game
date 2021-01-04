using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Rhythm.BMS;
using UnityEngine;
using UnityEngine.Networking;

namespace Rhythm
{
	public sealed class Speaker : MonoBehaviour
	{
		public GameObject KeySoundObject;
		public Audio audioComponent;

		[NonSerialized] public bool isPrepared = default;
		[NonSerialized] public Dictionary<int, string> pathes = default;
		[NonSerialized] public Dictionary<int, AudioClip> clips = default;

		public void Awake()
		{
			pathes = new Dictionary<int, string>();
			clips = new Dictionary<int, AudioClip>();
		}

		public void AddAudioClips()
		{
			StartCoroutine(CAddAudioClips());
		}

		private IEnumerator CAddAudioClips()
		{
			foreach (KeyValuePair<int, string> p in pathes)
			{
				string url = Game.Instance.header.parentPath + @"\";
				UnityWebRequest www = null;
				int extensionFailCount = 0;
				AudioType type = AudioType.OGGVORBIS;
				do
				{
					if (File.Exists(url + p.Value + Constant.SOUND_EXTENSIONS[extensionFailCount]))
					{
						break;
					}

					url = url.Replace(Constant.SOUND_EXTENSIONS[extensionFailCount],
						Constant.SOUND_EXTENSIONS[extensionFailCount + 1]);
					++extensionFailCount;
				} while (extensionFailCount < Constant.SOUND_EXTENSIONS.Length - 1);

				if (string.Compare(Constant.SOUND_EXTENSIONS[extensionFailCount], ".wav",
					StringComparison.OrdinalIgnoreCase) == 0)
				{
					type = AudioType.WAV;
				}
				else if (string.Compare(Constant.SOUND_EXTENSIONS[extensionFailCount], ".mp3",
					StringComparison.OrdinalIgnoreCase) == 0)
				{
					type = AudioType.MPEG;
				}

				www = UnityWebRequestMultimedia.GetAudioClip(
					"file://" + url + UnityWebRequest.EscapeURL(p.Value + Constant.SOUND_EXTENSIONS[extensionFailCount])
						.Replace('+', ' '), type);
				yield return www.SendWebRequest();

				if (www.downloadHandler.data.Length != 0)
				{
					AudioClip c = DownloadHandlerAudioClip.GetContent(www);
					c.LoadAudioData();
					clips.Add(p.Key, c);
				}
				else
				{
					Debug.LogWarning($"Failed to read sound data : {www.url}");
				}
			}

			isPrepared = true;
		}

		public void PlayKeySound(int key, float volume = 1.0f)
		{
			if (key == 0)
			{
				return;
			}

			if (clips.ContainsKey(key))
			{
				audioComponent.PlayOneShot(clips[key], volume);
			}
		}
	}
}