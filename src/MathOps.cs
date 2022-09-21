using System;
using System.Collections.Generic;

namespace MathEx
{
	public class MathTypeTag
	{
		protected static Dictionary<Type, MathTypeTag> specs = new Dictionary<Type, MathTypeTag>
		{
			{ typeof(float), new MathTypeTagFloat() },
			{ typeof(vec2), new MathTypeTagVec2() },
			{ typeof(vec3), new MathTypeTagVec3() },
#if UNITY || UNITY_5_3_OR_NEWER
			{ typeof(UnityEngine.Vector3), new MathTypeTagVector3() },
#endif
		};
	}

	public class MathTypeTag<T> : MathTypeTag
	{
		public static MathTypeTag<T> Get()
		{
			return (MathTypeTag<T>)specs[typeof(T)];
		}

		public virtual T zero => throw new NotImplementedException();
		public virtual T one => throw new NotImplementedException();
		public virtual T forward => throw new NotImplementedException();

		public virtual T sum(params T[] v) => throw new NotImplementedException();
		public virtual T diff(T a, T b) => throw new NotImplementedException();
		public virtual T mul(float a, T b) => throw new NotImplementedException();
		public virtual float scalar(T v) => throw new NotImplementedException();
		public virtual float distance(T p0, T p1) => throw new NotImplementedException();

		public virtual bool eq(T a, T b) => throw new NotImplementedException();
		public virtual bool eq_zero(T a) => eq(a, zero);
	}

	public class MathTypeTagFloat : MathTypeTag<float>
	{
		public override float zero => 0.0f;
		public override float one => 1.0f;
		public override float forward => one;

		public override float sum(params float[] v)
		{
			float result = 0;
			for (int i = 0; i < v.Length; i++)
				result += v[i];
			return result;
		}

		public override float diff(float a, float b) => a - b;
		public override float mul(float a, float b) => a * b;
		public override float scalar(float v) => v;
		public override float distance(float p0, float p1) => p1 - p0;

		public override bool eq(float a, float b) => a == b;

		public static bool eq(float a, float b, float eps) => MathExOps.Abs(a - b) <= eps;
		public static bool gt(float a, float b, float eps) => (a - b) > eps;
		public static bool lt(float a, float b, float eps) => (b - a) > eps;
	}

	public class MathTypeTagVec2 : MathTypeTag<vec2>
	{
		public override vec2 zero => vec2.zero;
		public override vec2 one => vec2.one;
		public override vec2 forward => vec2.right;

		public override vec2 sum(params vec2[] v)
		{
			vec2 result = vec2.zero;
			for (int i = 0; i < v.Length; i++)
				result += v[i];
			return result;
		}

		public override vec2 diff(vec2 a, vec2 b) => a - b;
		public override vec2 mul(float a, vec2 b) => a * b;
		public override float scalar(vec2 v) => v.length;
		public override float distance(vec2 p0, vec2 p1) => (p1 - p0).length;

		public override bool eq(vec2 a, vec2 b) => a == b;
	}

	public class MathTypeTagVec3 : MathTypeTag<vec3>
	{
		public override vec3 zero => vec3.zero;
		public override vec3 one => vec3.one;
		public override vec3 forward => vec3.forward;

		public override vec3 sum(params vec3[] v)
		{
			vec3 result = vec3.zero;
			for (int i = 0; i < v.Length; i++)
				result += v[i];
			return result;
		}

		public override vec3 diff(vec3 a, vec3 b) => a - b;
		public override vec3 mul(float a, vec3 b) => a * b;
		public override float scalar(vec3 v) => v.length;
		public override float distance(vec3 p0, vec3 p1) => (p1 - p0).length;

		public override bool eq(vec3 a, vec3 b) => a == b;
	}

#if UNITY || UNITY_5_3_OR_NEWER
	public class MathTypeTagVector3 : MathTypeTag<UnityEngine.Vector3>
	{
		public override UnityEngine.Vector3 zero => UnityEngine.Vector3.zero;
		public override UnityEngine.Vector3 one => UnityEngine.Vector3.one;
		public override UnityEngine.Vector3 forward => UnityEngine.Vector3.forward;

		public override UnityEngine.Vector3 sum(params UnityEngine.Vector3[] v)
		{
			UnityEngine.Vector3 result = UnityEngine.Vector3.zero;
			for (int i = 0; i < v.Length; i++)
				result += v[i];
			return result;
		}

		public override UnityEngine.Vector3 diff(UnityEngine.Vector3 a, UnityEngine.Vector3 b) => a - b;
		public override UnityEngine.Vector3 mul(float a, UnityEngine.Vector3 b) => a * b;
		public override float scalar(UnityEngine.Vector3 v) => v.magnitude;
		public override float distance(UnityEngine.Vector3 p0, UnityEngine.Vector3 p1) => (p1 - p0).magnitude;
		public override bool eq(UnityEngine.Vector3 a, UnityEngine.Vector3 b) => a == b;
	}
#endif
}
