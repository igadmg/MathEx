using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MathEx.Json
{
	class vec2JsonConverter : JsonConverter<vec2>
	{
		public override vec2 ReadJson(JsonReader reader, Type objectType, vec2 existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			var jo = JObject.Load(reader);
			var item = new vec2.Dto();
			serializer.Populate(jo.CreateReader(), item);

			return new vec2(item);
		}

		public override void WriteJson(JsonWriter writer, vec2 value, JsonSerializer serializer)
		{
			var jo = JObject.FromObject(value.ToDto(), serializer);
			jo.WriteTo(writer);
		}
	}
}
