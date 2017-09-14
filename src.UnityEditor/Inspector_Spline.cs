using MathEx;
using UnityEditor;
using UnityEngine;



[CustomEditor(typeof(Spline))]
public class Inspector_Spline : Editor
{
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

		float sl = spline.spline.length;
		float islsl = 1 / (sl * sl);
		for (float t = 0; t < 1;)
		{
			vec3 p = handleTransform.TransformPoint(spline.spline.value(t));
			vec3 v = spline.spline.velocity(t);
			float dt = Mathf.Clamp(v.length * islsl, islsl, 1);

			Handles.SphereHandleCap(-1, p, Quaternion.identity, 0.05f, EventType.Repaint);

			Handles.color = Color.white;
			Handles.DrawLine(p, handleTransform.TransformPoint(spline.spline.value(Mathf.Clamp01(t+dt))));
			Handles.color = Color.green;
			Handles.DrawLine(p, p + v / sl);
			t += dt;
		}
	}
}
