using System;
using System.Collections.Generic;
using System.Text;
using SystemEx;

namespace MathEx
{
	public class TransformStack
	{
		Stack<matrix4x4> transformStack = new Stack<matrix4x4>();
		public matrix4x4 currentTransform = matrix4x4.identity;


		public vec2 zero2 => (vec2.zero.xyzw(0, 1) * currentTransform).xy();


		public void Push(matrix4x4 transform)
		{
			transformStack.Push(currentTransform);
			currentTransform = transform * currentTransform;
		}

		public void Pop()
		{
			currentTransform = transformStack.Pop();
		}

		public IDisposable BeginTransform(vec2 translate)
		{
			Push(matrix4x4.Translate(translate.xyz(0)));
			return DisposableLock.Lock(() => Pop());
		}


		public static vec2 operator *(vec2 a, TransformStack b)
			=> (a.xyzw() * b.currentTransform).xy();

		public static vec2 operator *(vec2i a, TransformStack b)
			=> (a.xyzw() * b.currentTransform).xy();
	}
}
