using UnityEditor;
using UnityEngine;

namespace MathEx.UnityEditor
{
	[CustomPropertyDrawer(typeof(Spline.CubicBezierCurve))]
	public class PropertyDrawer_Curve : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			Spline.CubicBezierCurve obj = (Spline.CubicBezierCurve)fieldInfo.GetValue(property.serializedObject.targetObject);

			Rect contentPosition = EditorGUI.PrefixLabel(position, label);
			contentPosition.width *= .25f;
			EditorGUIUtility.labelWidth = 14f;
			EditorGUI.TextField(contentPosition, "l:", obj.length.ToString());
			contentPosition.x += contentPosition.width;
			//EditorGUIUtility.labelWidth = 14f;
			//EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("y"), new GUIContent("Y"));
			//contentPosition.x += contentPosition.width;
			//EditorGUIUtility.labelWidth = 14f;
			//EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("z"), new GUIContent("Z"));
			//contentPosition.x += contentPosition.width;
		}
	}
}