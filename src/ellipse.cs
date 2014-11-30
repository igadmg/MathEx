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
			this.major = (apo + peri) / 2;
			this.minor = MathEx.Sqrt(apo * peri);
			this.center = focus + direction * ((apo - peri) / 2);
			this.direction = direction;
		}
	}
}
