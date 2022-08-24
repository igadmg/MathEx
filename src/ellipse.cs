namespace MathEx
{
	using vec2 = vec2t<float>;
	using aabb2 = aabb2t<float>;

	public class ellipse2
	{
		vec2 center;
		vec2 direction;
		float major;
		float minor;

		public ellipse2(vec2 focus, vec2 direction, float apo, float peri)
		{
			major = (apo + peri) / 2;
			minor = MathExOps.Sqrt(apo * peri);
			center = focus + direction * ((apo - peri) / 2);
			this.direction = direction;
		}
	}
}
