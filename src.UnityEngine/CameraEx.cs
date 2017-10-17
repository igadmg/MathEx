using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;



namespace MathEx
{
	public static class CameraEx
	{
		public static IEnumerator<Ray> EnumCornerRays(this Camera c)
		{
			yield return c.ScreenPointToRay(new Vector2(0, 0));
			yield return c.ScreenPointToRay(new Vector2(0, c.pixelHeight));
			yield return c.ScreenPointToRay(new Vector2(c.pixelWidth, c.pixelHeight));
			yield return c.ScreenPointToRay(new Vector2(c.pixelWidth, 0));
			yield break;
		}

		public static Ray[] GetCornerRays(this Camera c)
		{
			Ray[] result = new Ray[4];
			result[0] = c.ScreenPointToRay(new Vector2(0, 0));
			result[1] = c.ScreenPointToRay(new Vector2(0, c.pixelHeight));
			result[2] = c.ScreenPointToRay(new Vector2(c.pixelWidth, c.pixelHeight));
			result[3] = c.ScreenPointToRay(new Vector2(c.pixelWidth, 0));

			return result;
		}

		public static bool Raycast(this Camera c, out RaycastHit hit)
		{
			return Physics.Raycast(c.transform.Ray(), out hit);
		}

		public static bool Raycast(this Camera c, out RaycastHit hit, [DefaultValue("Mathf.Infinity")] float maxDistance = Mathf.Infinity, [DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = Physics.DefaultRaycastLayers, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return Physics.Raycast(c.transform.Ray(), out hit, maxDistance, layerMask, queryTriggerInteraction);
		}
	}
}
