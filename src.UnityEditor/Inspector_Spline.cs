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

		vec3 lastPoint = vec3.empty;
		foreach (var i in spline.spline.Iterate())
		{
			vec3 p = handleTransform.TransformPoint(i.value);

			//Handles.SphereHandleCap(-1, p, Quaternion.identity, 0.05f, EventType.Repaint);

			if (!lastPoint.isEmpty)
			{
				Handles.color = Color.white;
				Handles.DrawLine(lastPoint, p);
				//Handles.color = Color.green;
				//Handles.DrawLine(p, p + v / sl);
			}

			lastPoint = p;
		}
	}
}
