using Rhythm.BMS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rhythm.UI
{
	public class LoadingSceneUI : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI m_logger = default;
		[SerializeField] private Image m_progress = default;

		private float _progress = 0f;

		private void Start()
		{
			m_progress.fillAmount = 0f;

			FileSystem.Instance.LogAction += PrintLog;
			FileSystem.Instance.ProgressAction += PrintProgress;
		}

		private void Update()
		{
			m_progress.fillAmount = Mathf.Lerp(m_progress.fillAmount, _progress, 0.1f);
		}

		private void OnDisable()
		{
			FileSystem.Instance.LogAction -= PrintLog;
			FileSystem.Instance.ProgressAction -= PrintProgress;
		}

		private void PrintLog(string log)
		{
			m_logger.SetText(log);
		}

		private void PrintProgress(float value)
		{
			_progress = value;
		}
	}
}