using MathEx;
using UnityEditor;
using UnityEngine;

public static class MathExGUI
{
	public static float vecLabelWidth = 12f;

	public static vec2 vec2Field(Rect position, string label, vec2 value, bool showLength)
	{
		float oldLabelWidth = EditorGUIUtility.labelWidth;

		vec2 result = value;

		if (!string.IsNullOrEmpty(label))
		{
			EditorGUI.LabelField(position, label);
			position.x += EditorGUIUtility.labelWidth;
			position.width -= EditorGUIUtility.labelWidth;
		}

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

		EditorGUIUtility.labelWidth = oldLabelWidth;

		return result;
	}

	public static vec3 vec3Field(Rect position, string label, vec3 value, bool showLength)
	{
		float oldLabelWidth = EditorGUIUtility.labelWidth;

		vec3 result = value;

		if (!string.IsNullOrEmpty(label))
		{
			EditorGUI.LabelField(position, label);
			position.x += EditorGUIUtility.labelWidth;
			position.width -= EditorGUIUtility.labelWidth;
		}

		position.width *= showLength ? 0.25f : 0.33f;

		EditorGUIUtility.labelWidth = vecLabelWidth;
		result.x = EditorGUI.FloatField(position, new GUIContent("X"), value.x);
		position.x += position.width;
		result.y = EditorGUI.FloatField(position, new GUIContent("Y"), value.y);
		position.x += position.width;
		result.z = EditorGUI.FloatField(position, new GUIContent("Z"), value.z);
		position.x += position.width;

		if (showLength)
		{
			EditorGUI.FloatField(position, new GUIContent("l"), value.length);
		}

		EditorGUIUtility.labelWidth = oldLabelWidth;

		return result;
	}
}