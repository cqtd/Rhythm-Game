namespace Rhythm
{
	public class Event
	{
		public const string OnSongPointerDown = "OnSongClick";
		public const string OnChangeSong = "OnChangeSong";

		public const string OnSongChanged = "OnSongChanged";
		public const string OnPatternChanged = "OnPatterChanged";

		public const string OnToggleOption = "OnToggleOption";

		public const string OnComboUpdate = "OnComboUpdate";
		public const string OnJudgeUpdate = "OnJudgeUpdate";
		public const string OnScoreUpdate = "OnScoreUpdate";

		public const string OnHandleNote = "OnHandleNote";
		public const string OnExistGap = "OnExistGap";

		/// <summary>
		///     노트 키가 눌렸을 때
		/// </summary>
		public const string OnKeyDown = "OnKeyDown";

		/// <summary>
		///     노트 키가 오토에 의해 눌렸을 때
		/// </summary>
		public const string OnKeyDownAuto = "OnKeyDownAuto";

		/// <summary>
		///     노트 키가 떼어졌을 때
		/// </summary>
		public const string OnKeyUp = "OnKeyUp";

		/// <summary>
		///     피버 배율이 올랐을 때
		/// </summary>
		public const string OnFeverIncrease = "OnFeverIncrease";

		/// <summary>
		///     피버 배율이 1이 되었을 때
		/// </summary>
		public const string OnFeverFinished = "OnFeverFinished";

		/// <summary>
		///     피버 게이지 변경 사항이 있을 때
		/// </summary>
		public const string OnFeverUpdate = "OnFeverUpdate";


		public const string OnFadeIn = "OnFadeIn";
		public const string OnFadeOut = "OnFadeOut";
	}
}