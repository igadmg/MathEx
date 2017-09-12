using System;
using UnityEngine;



namespace MathEx
{
	public class Spline : MonoBehaviour
	{
		[Serializable]
		public class CubicBezierCurve : spline_curve<vec3, CubicBezierSpline<vec3>>
		{
			public CubicBezierCurve() : base()
			{
			}

			public CubicBezierCurve(int length) : base(length)
			{
			}
		}

		[SerializeField]
		protected CubicBezierCurve m_Spline = new CubicBezierCurve(3);



		public CubicBezierCurve spline { get { return m_Spline; } }



		void Start()
		{
		}

		void Update()
		{
		}
	}
}
