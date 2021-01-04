public static class Math
{
	public static double Abs(double value)
	{
		if (value > 0d)
		{
			return value;
		}

		return -value;
	}

	public static int CeilToInt(double value)
	{
		return (int) (value + 1);
	}
}