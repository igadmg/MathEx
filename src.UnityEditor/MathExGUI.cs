using MathEx;
using UnityEditor;
using UnityEngine;

public static class MathExGUI
{
	public static float vecLabelWidth = 28f;

	public static vec2 vec2Field(Rect position, string label, vec2 value, bool showLength)
	{
		vec2 result = value;

		position.width *= showLength ? 0.25f : 0.33f;

		EditorGUIUtility.labelWidth = vecLabelWidth;
		result.x = EditorGUI.FloatField(position, new GUIContent("X"), value.x);
		position.x += position.width;
		result.y = EditorGUI.FloatField(position, new GUIContent("Y"), value.y);
		position.x += position.width;

		if (showLength)
		{
			position.x += position.width;
			EditorGUI.FloatField(position, new GUIContent("l"), value.length);
		}

		return result;
	}

	public static vec3 vec3Field(Rect position, string label, vec3 value, bool showLength)
	{
		vec3 result = value;

		position.width *= showLength ? 0.25f : 0.33f;

		EditorGUIUtility.labelWidth = vecLabelWidth;
		result.x = EditorGUI.FloatField(position, new GUIContent("X"), value.x);
		position.x += position.width;
		result.y = EditorGUI.FloatField(position, new GUIContent("Y"), value.y);
		position.x += position.width;
		result.z = EditorGUI.FloatField(position, new GUIContent("X"), value.z);
		position.x += position.width;

		if (showLength)
		{
			EditorGUI.FloatField(position, new GUIContent("l"), value.length);
		}

		return result;
	}
}