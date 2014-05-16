using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngineEx;

namespace MathEx
{
	public static class Convert
	{
		public static AaBb2 ToAaBb2(Rect rect)
		{
			return new AaBb2(rect.GetMin(), rect.GetMax());
		}
	}
}
