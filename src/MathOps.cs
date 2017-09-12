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
		};
	}

	public class MathTypeTag<T> : MathTypeTag
	{
		public static MathTypeTag<T> Get()
		{
			return (MathTypeTag<T>)specs[typeof(T)];
		}

		public virtual T zero()
		{
			throw new NotImplementedException();
		}

		public virtual T add(params T[] v)
		{
			throw new NotImplementedException();
		}

		public virtual T mul(float a, T b)
		{
			throw new NotImplementedException();
		}

		public virtual float distance(T p0, T p1)
		{
			throw new NotImplementedException();
		}
	}

	public class MathTypeTagFloat : MathTypeTag<float>
	{
		public override float zero()
		{
			return 0;
		}

		public override float add(params float[] v)
		{
			float result = 0;
			for (int i = 0; i < v.Length; i++)
				result += v[i];
			return result;
		}

		public override float mul(float a, float b)
		{
			return a * b;
		}

		public override float distance(float p0, float p1)
		{
			return p1 - p0;
		}
	}

	public class MathTypeTagVec2 : MathTypeTag<vec2>
	{
		public override vec2 zero()
		{
			return vec2.zero;
		}

		public override vec2 add(params vec2[] v)
		{
			vec2 result = vec2.zero;
			for (int i = 0; i < v.Length; i++)
				result += v[i];
			return result;
		}

		public override vec2 mul(float a, vec2 b)
		{
			return a * b;
		}

		public override float distance(vec2 p0, vec2 p1)
		{
			return (p1 - p0).length;
		}
	}

	public class MathTypeTagVec3 : MathTypeTag<vec3>
	{
		public override vec3 zero()
		{
			return vec3.zero;
		}

		public override vec3 add(params vec3[] v)
		{
			vec3 result = vec3.zero;
			for (int i = 0; i < v.Length; i++)
				result += v[i];
			return result;
		}

		public override vec3 mul(float a, vec3 b)
		{
			return a * b;
		}

		public override float distance(vec3 p0, vec3 p1)
		{
			return (p1 - p0).length;
		}
	}
}