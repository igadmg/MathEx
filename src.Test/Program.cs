using System;

namespace MathEx.Test
{
	public static class Program
	{
		public static int Main(string[] args)
		{
			foreach (var i in vec3i.zero.Line(new vec3i(20, 0, 10))) {
				Console.WriteLine(i);
			}

			return 0;
		}
	}
}