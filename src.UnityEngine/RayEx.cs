using UnityEngine;
using UnityEngine.Internal;

namespace MathEx
{
	public static class RayEx
	{
		public static bool Raycast(this Ray ray, out RaycastHit hit)
		{
			return Physics.Raycast(ray, out hit);
		}

		public static bool Raycast(this Ray ray, out RaycastHit hit, [DefaultValue("Mathf.Infinity")] float maxDistance = Mathf.Infinity, [DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = Physics.DefaultRaycastLayers, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return Physics.Raycast(ray, out hit, maxDistance, layerMask, queryTriggerInteraction);
		}
	}
}
