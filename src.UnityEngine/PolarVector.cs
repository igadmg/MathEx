using UnityEngine;

namespace MathEx
{
	public struct CylinderVector3
	{
		public float u;
		public float e;
		public float n;


		public CylinderVector3(float u, float e, float n)
		{
			this.u = u;
			this.e = e;
			this.n = n;
		}

		public static implicit operator CylinderVector3(Vector3 v)
		{
			float u = v.xy().magnitude;
			return new CylinderVector3(u, Mathf.Atan2(v.x, v.y), v.z);
		}

		/*
		public static implicit operator Vector3(CylinderVector3 v)
		{
			float sN = Mathf.Sin(v.n);
			float cN = Mathf.Cos(v.n);
			float sE = Mathf.Sin(v.e);
			float cE = Mathf.Cos(v.e);
			return new Vector3(v.u * sN * cE, v.u * sN * sE, v.u * cN);
		}
		*/
	}

	public struct SphereVector3
	{
		public float u;
		public float e;
		public float n;


		public SphereVector3(float u, float e, float n)
		{
			this.u = u;
			this.e = e;
			this.n = n;
		}

		public static implicit operator SphereVector3(Vector3 v)
		{
			float u = v.magnitude;
			return new SphereVector3(u, Mathf.Atan2(v.x, v.y), Mathf.Acos(v.z / u));
		}

		public static implicit operator Vector3(SphereVector3 v)
		{
			float sE = Mathf.Sin(v.e);
			float cE = Mathf.Cos(v.e);
			float sN = Mathf.Sin(v.n);
			float cN = Mathf.Cos(v.n);
			return new Vector3(v.u * sN * cE, v.u * sN * sE, v.u * cN);
		}
	}
}

