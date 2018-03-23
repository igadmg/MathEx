using UnityEditor;
using UnityEngine;



namespace MathEx.UnityEditor
{
	[CustomPropertyDrawer(typeof(line3_segment))]
	public class PropertyDrawer_line3_segment : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			label = EditorGUI.BeginProperty(position, label, property);
			{
				line3_segment obj = (line3_segment)fieldInfo.GetValue(property.serializedObject.targetObject);

				Rect contentPosition = EditorGUI.PrefixLabel(position, label);
				contentPosition.height = 16f;

				EditorGUIUtility.labelWidth = 16f;
				obj.a = MathExGUI.vec3Field(contentPosition, "a:", obj.a, false);

				contentPosition.y += 16f;
				obj.b = MathExGUI.vec3Field(contentPosition, "b:", obj.b, false);
			}
			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return 32f;
		}
	}
}
