﻿using System;
using UnityEngine;



namespace MathEx
{
#if MATHEX_CUSTOM_SPLINES
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

	[Serializable]
	public class CubicBezierCurveController : CubicBezierCurve.controller_type
	{
		public CubicBezierCurveController(CubicBezierCurve c) : base(c)
		{
		}
	}

	[ExecuteInEditMode]
	public class Spline : MonoBehaviour
	{
		[SerializeField]
		protected CubicBezierCurve m_Spline = new CubicBezierCurve(3);

		[SerializeField, HideInInspector]
		protected CubicBezierCurveController m_SplineController = null;



		public CubicBezierCurve spline { get { return m_Spline; } }
		public CubicBezierCurveController splineController { get { return m_SplineController; } }

		public Vector3 value(float t) { return transform.TransformPoint(spline.value(t)); }
		public Vector3 getNodeValue(int i) { return transform.TransformPoint(spline.getNodeValue(i)); }
		public Vector3 getNodeTangent(int i) { return transform.TransformDirection(spline.getNodeTangent(i)); }



		private void Awake()
		{
			if (m_SplineController == null || m_SplineController.modes == null)
			{
				m_SplineController = new CubicBezierCurveController(m_Spline);
			}
			else
			{
				m_SplineController.c = m_Spline;
			}
		}
	}
#endif
}
