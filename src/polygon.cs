namespace MathEx
{
	public class polygon<T>
	{
		public T[] p;

		public polygon(int size)
		{
			p = new T[size];
		}

		public polygon(T[] p)
		{
			this.p = p;
		}
	}
}
