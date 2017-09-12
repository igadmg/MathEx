using UnityEditor;
using UnityEngine;

namespace MathEx.UnityEditor
{
	[CustomPropertyDrawer(typeof(Spline.CubicBezierCurve))]
	public class PropertyDrawer_Curve : PropertyDrawer
	{
		public bool pointsFoldoutState = false;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			bool bIsDirty = false;
			int indentLevel = EditorGUI.indentLevel;
			Spline.CubicBezierCurve obj = (Spline.CubicBezierCurve)fieldInfo.GetValue(property.serializedObject.targetObject);

			//EditorGUI.Vector2Field

			Rect contentPosition = EditorGUI.PrefixLabel(position, label);
			contentPosition.width *= .25f;
			EditorGUIUtility.labelWidth = 14f;
			EditorGUI.TextField(contentPosition, "l:", obj.length.ToString());
			contentPosition.y += contentPosition.height;
			EditorGUILayout.BeginVertical();
			pointsFoldoutState = EditorGUILayout.Foldout(pointsFoldoutState, "points");
			if (pointsFoldoutState)
			{
				EditorGUI.indentLevel++;

				for (int i = 0; i < obj.p.Length; i++)
				{
					Rect rect = GUILayoutUtility.GetRect(0, float.MaxValue, EditorGUIUtility.singleLineHeight, EditorGUIUtility.singleLineHeight);
					vec3 p = MathExGUI.vec3Field(rect, null, obj.p[i], false);
					if (obj.p[i] != p)
					{
						if (!bIsDirty)
						{
							Undo.RecordObject(property.serializedObject.targetObject, "Curve Point Modified");
						}

						obj.p[i] = p;
						bIsDirty = true;
					}
				}

				EditorGUI.indentLevel--;
			}
			EditorGUILayout.EndVertical();
			//EditorGUIUtility.labelWidth = 14f;
			//EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("y"), new GUIContent("Y"));
			//contentPosition.x += contentPosition.width;
			//EditorGUIUtility.labelWidth = 14f;
			//EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("z"), new GUIContent("Z"));
			//contentPosition.x += contentPosition.width;

			EditorGUI.indentLevel = indentLevel;
			if (bIsDirty)
			{
				fieldInfo.SetValue(property.serializedObject.targetObject, obj);
				property.serializedObject.ApplyModifiedProperties();
			}

			EditorGUI.EndProperty();
		}
	}
}