using UnityEngine;

namespace Rhythm.BMS
{
	public class Audio : MonoBehaviour
	{
		[SerializeField] private int ChannelLength = default;

		private AudioSource[] _audioSources;

		private void Awake()
		{
			_audioSources = new AudioSource[ChannelLength];

			for (int i = 0; i < ChannelLength; ++i)
			{
				_audioSources[i] = gameObject.AddComponent<AudioSource>();
				_audioSources[i].loop = false;
				_audioSources[i].playOnAwake = false;
			}
		}

		public void Play(AudioClip clip, float volume = 1.0f)
		{
			foreach (AudioSource a in _audioSources)
			{
				if (a.isPlaying)
				{
					continue;
				}

				a.clip = clip;
				a.volume = volume;
				a.Play();
				break;
			}
		}

		public void PlayOneShot(AudioClip clip, float volume = 1.0f)
		{
			foreach (AudioSource a in _audioSources)
			{
				if (a.isPlaying)
				{
					continue;
				}

				a.PlayOneShot(clip, volume);
				break;
			}
		}
	}
}