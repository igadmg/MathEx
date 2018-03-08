using UnityEditor;
using UnityEngine;

namespace MathEx.UnityEditor
{
	[CustomPropertyDrawer(typeof(Quaternion))]
	public class PropertyDrawer_Quaternion : PropertyDrawer
	{
		private bool foldout = false;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			Quaternion obj = (Quaternion)fieldInfo.GetValue(property.serializedObject.targetObject);
			Vector3 ea = obj.eulerAngles;

			Rect labelPosition = new Rect(position.x + EditorGUI.indentLevel * 15f, position.y, EditorGUIUtility.labelWidth - EditorGUI.indentLevel * 15f, 16f);
			foldout = EditorGUI.Foldout(labelPosition, foldout, label);
			Rect contentPosition = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, position.width - EditorGUIUtility.labelWidth, 16f);
			Rect quaternionPosition = contentPosition;

			EditorGUI.BeginChangeCheck();
			contentPosition.width = contentPosition.width * 0.333f - 14f;
			EditorGUI.LabelField(contentPosition, "X");
			contentPosition.x += 14f;
			ea.x = EditorGUI.FloatField(contentPosition, ea.x);
			contentPosition.x += contentPosition.width;
			EditorGUI.LabelField(contentPosition, "Y");
			contentPosition.x += 14f;
			ea.y = EditorGUI.FloatField(contentPosition, ea.y);
			contentPosition.x += contentPosition.width;
			EditorGUI.LabelField(contentPosition, "Z");
			contentPosition.x += 14f;
			ea.z = EditorGUI.FloatField(contentPosition, ea.z);
			contentPosition.x += contentPosition.width;
			if (EditorGUI.EndChangeCheck())
			{
				fieldInfo.SetValue(property.serializedObject.targetObject, Quaternion.Euler(ea));
			}

			if (foldout)
			{
				quaternionPosition.y += 16f;

				quaternionPosition.width *= .25f;
				EditorGUIUtility.labelWidth = 14f;
				EditorGUI.PropertyField(quaternionPosition, property.FindPropertyRelative("x"), new GUIContent("X"));
				quaternionPosition.x += quaternionPosition.width;
				EditorGUI.PropertyField(quaternionPosition, property.FindPropertyRelative("y"), new GUIContent("Y"));
				quaternionPosition.x += quaternionPosition.width;
				EditorGUI.PropertyField(quaternionPosition, property.FindPropertyRelative("z"), new GUIContent("Z"));
				quaternionPosition.x += quaternionPosition.width;
				EditorGUI.PropertyField(quaternionPosition, property.FindPropertyRelative("w"), new GUIContent("W"));
				quaternionPosition.x += quaternionPosition.width;
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return foldout ? 32f : 16f;
		}
	}
}
