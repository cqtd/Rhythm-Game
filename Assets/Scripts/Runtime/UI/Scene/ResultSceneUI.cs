using System.Collections;
using Rhythm.BMS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rhythm.UI
{
	public sealed class ResultSceneUI : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI m_rank = default;
		[SerializeField] private RawImage m_artwork = default;
		[SerializeField] private ResultBlockUI[] m_results = default;

		public void Awake()
		{
			StartCoroutine(DisplayResult());
		}

		private IEnumerator DisplayResult()
		{
			BeMusicHeader header = Game.Instance.header;
			string texturePath = $"file://{header.parentPath}/{header.stageFile}";

			bool complete = false;
			Texture2D tex2D = null;
			TextureDownloader.Instance.GetTexture2D(texturePath, texture =>
			{
				tex2D = texture;
				complete = true;
			});

			yield return new WaitUntil(() => complete);

			m_artwork.texture = tex2D;

			SafeEditorCall.CallEditorOnly(() =>
			{
				if (tex2D == null)
				{
					Debug.LogWarning("아트워크가 없습니다.");
				}
			});

			GameResult result = Game.Instance.GetLastResult();

			// 노트 통계
			m_results[0].Initialize("PERFECT", result.noteData.perfect.ToString("D4"));
			m_results[1].Initialize("GREAT", result.noteData.great.ToString("D4"));
			m_results[2].Initialize("GOOD", result.noteData.good.ToString("D4"));
			m_results[3].Initialize("BAD", result.noteData.bad.ToString("D4"));
			m_results[4].Initialize("POOR", result.noteData.poor.ToString("D4"));
			m_results[5].Initialize("RATE", result.scoreData.accuracyAverage.ToString("P"));
			m_results[6].Initialize("BEST COMBO", result.scoreData.bestCombo.ToString("D4"));

			// 퍼센티지 계산

			// 랭크 계산
			Enum.Rank rate = Judge.Rank(result.scoreData.accuracyAverage);
			m_rank.SetText(rate.ToString());
		}
	}
}