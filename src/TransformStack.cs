using System;
using SystemEx;

namespace MathEx
{
	public class TransformStack
	{
		TransformStack parent;
		public matrix4x4 currentTransform = matrix4x4.identity;


		public vec2 zero2 => (vec2.zero.xyzw(0, 1) * currentTransform).xy();


		public TransformStack Push(matrix4x4 transform)
			=> new TransformStack {
				parent = this,
				currentTransform = transform * currentTransform
			};

		public TransformStack Pop()
			=> parent;


		public static vec2 operator %(vec2 a, TransformStack b)
			=> (a.xyzw(0, 0) * b.currentTransform).xy();

		public static vec2 operator %(vec2i a, TransformStack b)
			=> (a.xyzw(0, 0) * b.currentTransform).xy();

		public static vec2 operator *(vec2 a, TransformStack b)
			=> (a.xyzw(0, 1) * b.currentTransform).xy();

		public static vec2 operator *(vec2i a, TransformStack b)
			=> (a.xyzw(0, 1) * b.currentTransform).xy();
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
