using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MathEx.Json
{
	class vec2iJsonConverter : JsonConverter<vec2i>
	{
		public override vec2i ReadJson(JsonReader reader, Type objectType, vec2i existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			var jo = JObject.Load(reader);
			var item = new vec2i.Dto();
			serializer.Populate(jo.CreateReader(), item);

			return new vec2i(item);
		}

		public override void WriteJson(JsonWriter writer, vec2i value, JsonSerializer serializer)
		{
			var jo = JObject.FromObject(value.ToDto(), serializer);
			jo.WriteTo(writer);
		}
	}
}
