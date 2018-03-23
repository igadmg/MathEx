using UnityEngine;



namespace MathEx
{
	public static class RectTransformEx
	{
		public static Vector2 WorldToCanvas(this RectTransform canvasRect, Vector3 worldPosition, Camera camera = null)
		{
			if (camera == null)
			{
				camera = Camera.main;
			}

			Vector3 viewportPosition = camera.WorldToViewportPoint(worldPosition);
			return new Vector2((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)
				, (viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));
		}
	}
}
