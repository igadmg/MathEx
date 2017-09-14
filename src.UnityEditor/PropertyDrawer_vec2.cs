using SystemEx;
using UnityEditor;
using UnityEngine;

namespace MathEx.UnityEditor
{
	[CustomPropertyDrawer(typeof(vec2))]
	public class PropertyDrawer_vec2 : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			vec2 obj = (vec2)fieldInfo.GetValue(property.serializedObject.targetObject);

			Rect contentPosition = EditorGUI.PrefixLabel(position, label);
			contentPosition.width *= .25f;
			EditorGUIUtility.labelWidth = 14f;
			EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("x"), new GUIContent("X"));
			contentPosition.x += contentPosition.width;
			EditorGUIUtility.labelWidth = 14f;
			EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("y"), new GUIContent("Y"));
			contentPosition.x += contentPosition.width;
			//EditorGUIUtility.labelWidth = 14f;
			//EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("z"), new GUIContent("Z"));
			contentPosition.x += contentPosition.width;

			EditorGUI.SelectableLabel(contentPosition, "l: " + obj.length.ToString());
		}
	}
}