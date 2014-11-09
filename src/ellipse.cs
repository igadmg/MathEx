using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEx
{
	public class ellipse2
	{
		vec2 center;
		vec2 direction;
		float major;
		float minor;

		public ellipse2(vec2 focus, vec2 direction, float apo, float peri)
		{
			major = (apo + peri) / 2;
			minor = MathEx.Sqrt(apo * peri);
			center = focus + direction * ((apo - peri) / 2);
		}
	}
}
