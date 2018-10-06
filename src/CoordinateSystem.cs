namespace MathEx
{
	public class coordsystem
	{
		public vec3 right;
		public vec3 forward;
		public vec3 up;

		public coordsystem()
		{
			right = vec3.right;
			forward = vec3.forward;
			up = vec3.up;
		}

		public coordsystem(vec3 right, vec3 forward, vec3 up)
		{
			this.right = right;
			this.forward = forward;
			this.up = up;
		}
	}
}
