using SystemEx;
using UnityEditor;
using UnityEngine;

namespace MathEx.UnityEditor
{
	[CustomPropertyDrawer(typeof(vec3))]
	public class PropertyDrawer_vec3 : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			vec3 obj = (vec3)fieldInfo.GetValue(property.serializedObject.targetObject);

			Rect contentPosition = EditorGUI.PrefixLabel(position, label);
			contentPosition.width *= .25f;
			EditorGUIUtility.labelWidth = 14f;
			EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("x"), new GUIContent("X"));
			contentPosition.x += contentPosition.width;
			EditorGUIUtility.labelWidth = 14f;
			EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("y"), new GUIContent("Y"));
			contentPosition.x += contentPosition.width;
			EditorGUIUtility.labelWidth = 14f;
			EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("z"), new GUIContent("Z"));
			contentPosition.x += contentPosition.width;

			float magnitude = obj.magnitude;
			float new_magnitude;
			if (float.TryParse(EditorGUI.TextField(contentPosition, "m", magnitude.ToString()), out new_magnitude)) {
				if (!magnitude.eq(new_magnitude))
					obj.magnitude = new_magnitude;
			}
		}
	}
}