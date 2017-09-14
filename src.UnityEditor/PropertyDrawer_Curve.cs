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
			EditorGUI.SelectableLabel(contentPosition, "l: " + obj.numberOfNodes.ToString());
			contentPosition.y += contentPosition.height;
			EditorGUILayout.BeginVertical();
			pointsFoldoutState = EditorGUILayout.Foldout(pointsFoldoutState, "Curve Points:");
			if (pointsFoldoutState)
			{
				EditorGUI.indentLevel++;

				for (int i = 0; i < obj.p.Length; i++)
				{
					Rect dataRect = GUILayoutUtility.GetRect(0, float.MaxValue, EditorGUIUtility.singleLineHeight, EditorGUIUtility.singleLineHeight);
					Rect leftRect = dataRect;
					Rect rightRect = dataRect;

					leftRect.xMax = leftRect.xMin + 48f;
					dataRect.xMin += 48f;
					dataRect.xMax -= 48f;
					rightRect.xMin = rightRect.xMax - 48f;

					if (i % (obj.chunkSize - 1) == 0)
					{
						GUIStyle s = EditorStyles.label;
						EditorGUI.LabelField(leftRect, "Node");
					}

					vec3 p = MathExGUI.vec3Field(dataRect, null, obj.p[i], false);
					if (obj.p[i] != p)
					{
						if (!bIsDirty)
						{
							Undo.RecordObject(property.serializedObject.targetObject, "Curve Point Modified");
						}

						obj.p[i] = p;
						bIsDirty = true;
					}

					Rect addRect = rightRect;
					Rect removeRect = rightRect;
					addRect.xMax -= addRect.width / 2;
					removeRect.xMin += removeRect.width / 2;

					if (i % (obj.chunkSize - 1) == 0)
					{
						if (GUI.Button(addRect, "+"))
						{
							bIsDirty = true;
							Undo.RecordObject(property.serializedObject.targetObject, "Curve Point Added");

							obj.insert(i / (obj.chunkSize - 1));
						}
						if (GUI.Button(removeRect, "-"))
						{
							bIsDirty = true;
							Undo.RecordObject(property.serializedObject.targetObject, "Curve Point Removed");

							obj.remove(i / (obj.chunkSize - 1));
						}
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