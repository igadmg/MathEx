using UnityEngine;

namespace MathEx
{
	public static class ColorEx
	{
		public static Color empty = new Color(float.NaN, float.NaN, float.NaN, float.NaN);

		/// <summary>
		/// Returns new Color with the Alpha value set.
		/// </summary>
		/// <param name="color"></param>
		/// <param name="a"></param>
		/// <returns>Returns new Color with alpha value set to a</returns>
		public static Color Alpha(this Color color, float a)
		{
			return new Color(color.r, color.g, color.b, a);
		}


		/// <summary>
		/// Cecks if Color is empty - any component of Color is NaN.
		/// </summary>
		/// <param name="color"></param>
		/// <returns>Returns true if any component of color is NaN</returns>
		public static bool IsEmpty(this Color color)
		{
			return float.IsNaN(color.a) || float.IsNaN(color.r) || float.IsNaN(color.g) || float.IsNaN(color.b);
		}

		/// <summary>
		/// Construct color from hex value 0xaarrggbb or 0xrrggbb.
		/// </summary>
		/// <param name="hex"></param>
		/// <returns>Returns constructed color.</returns>
		public static Color FromHex(long hex)
		{
			return new Color(((hex >> 16) & 0xff)/255.0f, ((hex >> 8) & 0xff)/255.0f, (hex & 0xff)/255.0f, ((hex >> 24) & 0xff)/255.0f);
		}

		/// <summary>
		/// Converts color to hex string in format aarrggbb.
		/// </summary>
		/// <param name="color"></param>
		/// <returns>Returns string representing a clolr in fromat aarrggbb</returns>
		public static string ToHexString(this Color color)
		{
			return ((int)(255 * color.r)).ToString("x2") + ((int)(255 * color.g)).ToString("x2") + ((int)(255 * color.b)).ToString("x2") + ((int)(255 * color.a)).ToString("x2");
		}
	}
}

