using UnityEditor;
using UnityEngine;



namespace MathEx.UnityEditor
{
	[CustomPropertyDrawer(typeof(circle))]
	public class PropertyDrawer_circle : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			label = EditorGUI.BeginProperty(position, label, property);
			{
				circle obj = (circle)fieldInfo.GetValue(property.serializedObject.targetObject);

				Rect contentPosition = EditorGUI.PrefixLabel(position, label);
				contentPosition.height = 16f;

				EditorGUIUtility.labelWidth = 16f;
				obj.o = MathExGUI.vec2Field(contentPosition, "o:", obj.o, false);

				contentPosition.y += 16f;
				EditorGUIUtility.labelWidth = 48f;
				obj.r = EditorGUI.FloatField(contentPosition, new GUIContent("Radius:"), obj.r);
			}
			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return 32f;
		}
	}
}
