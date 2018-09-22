using MathEx;
using UnityEditor;
using UnityEngine;



[CustomEditor(typeof(Spline))]
public class Inspector_Spline : Editor
{
	public static Inspector_Spline instance = null;

	public int iChangedNode = -1;

	private void OnSceneGUI()
	{
		instance = this;
		iChangedNode = -1;

		Spline spline = target as Spline;
		Transform handleTransform = spline.transform;

		CubicBezierCurveController splineController = spline.splineController;

		for (int i = 0; i < spline.spline.p.Length; i++)
		{
			Vector3 p = handleTransform.TransformPoint(spline.spline.p[i]);
			EditorGUI.BeginChangeCheck();
			p = Handles.DoPositionHandle(p, handleTransform.rotation);
			//Debug.Log(GUIUtility.GetControlID(FocusType.Passive));
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(spline, "Curve Point Moved");
				spline.splineController.setP(i, handleTransform.InverseTransformPoint(p));
			}

			if (i > 0)
			{
				Handles.color = Color.white;
				Vector3 p0 = handleTransform.TransformPoint(spline.spline.p[i - 1]);
				Handles.DrawLine(p0, p);
			}
		}

		//Debug.Log("hotControl: " + GUIUtility.hotControl + ", keyboardControl: " + GUIUtility.keyboardControl);

		polyline_builder<Vector3> points = new polyline_builder<Vector3>();
		foreach (var i in spline.spline.Iterate())
		{
			points.add(handleTransform.TransformPoint(i.value), true);
		}
		Handles.color = Color.white;
		Handles.DrawPolyLine(points.points.ToArray());
	}

	private bool HasFrameBounds() { return true; }

	private Bounds OnGetFrameBounds()
	{
		Bounds bounds = new Bounds();

		Spline spline = target as Spline;

		bounds.center = spline.transform.position;
		foreach (vec3 p in spline.spline.p)
		{
			bounds.Encapsulate(spline.transform.TransformPoint(p));
		}

		return bounds;
	}
}
