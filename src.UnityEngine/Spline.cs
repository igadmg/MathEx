using MathEx;
using System;
using UnityEngine;



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
	vec3 start;

	[SerializeField]
	CubicBezierCurve spline = new CubicBezierCurve(3);

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
	}
}
