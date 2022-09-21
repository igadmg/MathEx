using System;
using SystemEx;

namespace MathEx
{
	using vec2 = vec2t<float>;
	using aabb2 = aabb2t<float>;

	public class TransformStack
	{
		TransformStack parent;
		public matrix4x4 currentTransform = matrix4x4.identity;
		Lazy<matrix4x4> inverseTransform_;
		public matrix4x4 inverseTransform => inverseTransform_.Value;


		public vec2 zero2 => (vec2.zero.xyzw(0, 1) * currentTransform).xy();

		public TransformStack()
		{
			inverseTransform_ = new Lazy<matrix4x4>(() => currentTransform.inverted);
		}

		public TransformStack Push(matrix4x4 transform)
			=> new TransformStack {
				parent = this,
				currentTransform = transform * currentTransform
			};

		public TransformStack Pop()
			=> parent;


		public static vec4 operator *(vec4 a, TransformStack b) => a * b.currentTransform;
		public static vec2 operator *(vec2 a, TransformStack b) => (a.xyzw(0, 1) * b.currentTransform).xy();
		public static vec2 operator *(vec2i a, TransformStack b) => (a.xyzw(0, 1) * b.currentTransform).xy();

		public static vec2 operator %(vec2 a, TransformStack b) => (a.xyzw(0, 0) * b.currentTransform).xy();
		public static vec2 operator %(vec2i a, TransformStack b) => (a.xyzw(0, 0) * b.currentTransform).xy();


		public static vec4 operator *(TransformStack a, vec4 b) => b * a.inverseTransform;
		public static vec2 operator *(TransformStack a, vec2 b) => (b.xyzw(0, 1) * a.inverseTransform).xy();
		public static vec2 operator *(TransformStack a, vec2i b) => (b.xyzw(0, 1) * a.inverseTransform).xy();

		public static vec2 operator %(TransformStack a, vec2 b) => (b.xyzw(0, 0) * a.inverseTransform).xy();
		public static vec2 operator %(TransformStack a, vec2i b) => (b.xyzw(0, 0) * a.inverseTransform).xy();
	}

	public class TrasformStackCursor
	{
		public TransformStack ts { get; protected set; } = new TransformStack();

		public IDisposable BeginTransform(vec2 translate)
		{
			ts = ts.Push(matrix4x4.Translate(translate.xyz(0)));
			return DisposableLock.Lock(() => ts = ts.Pop());
		}
	}
}
