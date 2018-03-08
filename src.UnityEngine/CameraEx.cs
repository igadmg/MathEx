using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;



namespace MathEx
{
	public static class CameraEx
	{
		private static float GetGameViewAspectRatio()
		{
			return Screen.width / Screen.height;
		}

		public static float GetFrustumAspectRatio(this Camera camera)
		{
			Rect rect = camera.rect;
			if (rect.width <= 0 || rect.height <= 0)
				return -1f;
			return GetGameViewAspectRatio() * (rect.width / rect.height);
		}

		public static bool GetFrustum(this Camera camera, Vector3[] near, Vector3[] far, out float frustumAspect)
		{
			frustumAspect = camera.GetFrustumAspectRatio();
			if (frustumAspect < 0)
				return false;

			if (far != null)
			{
				far[0] = new Vector3(0.0f, 0.0f, camera.farClipPlane);
				far[1] = new Vector3(0.0f, 1f, camera.farClipPlane);
				far[2] = new Vector3(1f, 1f, camera.farClipPlane);
				far[3] = new Vector3(1f, 0.0f, camera.farClipPlane);
				for (int index = 0; index < 4; ++index)
					far[index] = camera.ViewportToWorldPoint(far[index]);
			}
			if (near != null)
			{
				near[0] = new Vector3(0.0f, 0.0f, camera.nearClipPlane);
				near[1] = new Vector3(0.0f, 1f, camera.nearClipPlane);
				near[2] = new Vector3(1f, 1f, camera.nearClipPlane);
				near[3] = new Vector3(1f, 0.0f, camera.nearClipPlane);
				for (int index = 0; index < 4; ++index)
					near[index] = camera.ViewportToWorldPoint(near[index]);
			}

			return true;
		}

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
