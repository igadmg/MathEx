using Newtonsoft.Json;

namespace MathEx.Json
{
	public static class Converters
	{
		public static JsonConverter[] All
			=> new JsonConverter[] {
				new vec2JsonConverter(),
				new vec2iJsonConverter()
			};
	}
}
