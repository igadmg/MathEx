using MathEx;
using UnityEditor;
using UnityEngine;



[CustomEditor(typeof(Spline))]
public class Inspector_Spline : Editor
{
	float splineStep = 0.5f;

	private void OnSceneGUI()
	{
		Spline spline = target as Spline;
		Transform handleTransform = spline.transform;

		for (int i = 0; i < spline.spline.p.Length; i++)
		{
			Vector3 p = handleTransform.TransformPoint(spline.spline.p[i]);
			EditorGUI.BeginChangeCheck();
			p = Handles.DoPositionHandle(p, handleTransform.rotation);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(spline, "Curve Point Moved");
				spline.spline.p[i] = handleTransform.InverseTransformPoint(p);
			}

			if (i > 0)
			{
				Handles.color = Color.white;
				Vector3 p0 = handleTransform.TransformPoint(spline.spline.p[i-1]);
				Handles.DrawLine(p0, p);
			}
		}

		Handles.color = Color.white;
		float dt = splineStep / spline.spline.length;
		for (float t = 0; t < 1; t += dt)
		{
			Handles.DrawLine(spline.spline.value(t), spline.spline.value(Mathf.Clamp01(t+dt)));
		}
	}
}
