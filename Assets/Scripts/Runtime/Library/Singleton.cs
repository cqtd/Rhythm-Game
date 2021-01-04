namespace Rhythm
{
	public abstract class Singleton<T> where T : class, new()
	{
		private static T m_instance = default;

		public static T Instance {
			get
			{
				if (m_instance == null)
				{
					m_instance = new T();
				}

				return m_instance;
			}
		}

		public static void Reset()
		{
			m_instance = null;
		}
	}
}