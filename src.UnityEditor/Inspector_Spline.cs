using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spline))]
public class Inspector_Spline : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		Spline spline = target as Spline;
	}
}
