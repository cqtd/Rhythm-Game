using UnityEngine;

namespace Cqunity.Rhythmical
{
	public class AudioManager : MonoBehaviour
	{
		public static AudioManager instance;
		
		private AudioSource m_audioSource;
		private bool bIsPlaying = false;


		private void Start()
		{
			instance = this;
			m_audioSource = GetComponent<AudioSource>();
		}

		public void Play()
		{
			if (!bIsPlaying)
			{
				m_audioSource.Play();
				bIsPlaying = true;
			}
		}
	}
}