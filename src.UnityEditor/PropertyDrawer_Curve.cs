using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MathEx.UnityEditor
{
	[CustomPropertyDrawer(typeof(CubicBezierCurve))]
	public class PropertyDrawer_Curve : PropertyDrawer
	{
		public bool pointsFoldoutState = false;

		public static string[] modeOptions = Enum.GetNames(typeof(CubicBezierCurveController.CurveMode));

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			FieldInfo controllerFieldInfo = fieldInfo.DeclaringType.GetField(fieldInfo.Name + "Controller", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			bool bIsDirty = false;
			int indentLevel = EditorGUI.indentLevel;
			CubicBezierCurve obj = (CubicBezierCurve)fieldInfo.GetValue(property.serializedObject.targetObject);
			CubicBezierCurveController objController = null;
			if (controllerFieldInfo != null)
			{
				objController = (CubicBezierCurveController)controllerFieldInfo.GetValue(property.serializedObject.targetObject);
				objController.c = obj;
			}

			Rect contentPosition = EditorGUI.PrefixLabel(position, label);
			contentPosition.width *= .25f;
			EditorGUIUtility.labelWidth = 40f;
			EditorGUI.BeginChangeCheck();
			bool newLoop = EditorGUI.Toggle(contentPosition, "Loop:", obj.loop);
			if (EditorGUI.EndChangeCheck())
			{
				bIsDirty = true;
				Undo.RecordObject(property.serializedObject.targetObject, "Curve Loop Flag Changed");

				obj.loop = newLoop;
			}
			contentPosition.x += contentPosition.width;
			EditorGUIUtility.labelWidth = 14f;
			EditorGUI.SelectableLabel(contentPosition, "l: " + obj.numberOfNodes.ToString());
			contentPosition.y += contentPosition.height;
			EditorGUILayout.BeginVertical();
			pointsFoldoutState = EditorGUILayout.Foldout(pointsFoldoutState, "Curve Points:");
			if (pointsFoldoutState)
			{
				EditorGUI.indentLevel++;

				int buttonPanelWidth = 40 + (objController != null ? 40 : 0);

				for (int i = 0; i < obj.p.Length; i++)
				{
					Rect dataRect = GUILayoutUtility.GetRect(0, float.MaxValue, EditorGUIUtility.singleLineHeight, EditorGUIUtility.singleLineHeight);
					Rect leftRect = dataRect;
					Rect rightRect = dataRect;

					leftRect.xMax = leftRect.xMin + 48f;
					dataRect.xMin += 48f;
					dataRect.xMax -= buttonPanelWidth;
					rightRect.xMin = rightRect.xMax - buttonPanelWidth;

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
					addRect.xMax = addRect.xMax - 20;
					addRect.xMin = addRect.xMax - 20;
					removeRect.xMin = removeRect.xMax - 20;

					if (i % (obj.chunkSize - 1) == 0)
					{
						if (objController != null)
						{
							Rect modeRect = rightRect;
							modeRect.xMax = modeRect.xMin + 40;
							modeRect.xMin -= 10;

							int currentMode = (int)objController.modes[i / (obj.chunkSize - 1)];
							int newMode = EditorGUI.Popup(modeRect, currentMode, modeOptions);
							if (newMode != currentMode)
							{
								bIsDirty = true;
								Undo.RecordObject(property.serializedObject.targetObject, "Curve Point Mode Changed");

								objController.modes[i / (obj.chunkSize - 1)] = (CubicBezierCurveController.CurveMode)newMode;
							}
						}

						if (GUI.Button(addRect, "+"))
						{
							bIsDirty = true;
							Undo.RecordObject(property.serializedObject.targetObject, "Curve Point Added");

							int nni = obj.getIndexNode(i);
							float nnit = obj.getNodeTime(nni);

							if (nni == obj.numberOfNodes - 1)
							{
								vec3 nniv = obj.value(nnit);
								vec3 nni0v = obj.velocity(nnit);

								vec3 inv = nniv + nni0v * 4;
								vec3 invv = inv - nni0v;

								if (objController != null)
									objController.insert(nni, nni0v, inv, invv);
								else
									obj.insert(nni, nni0v, inv, invv);
							}
							else
							{
								int ni = nni - 1;
								float nit = ni < 0 ? 0 : obj.getNodeTime(ni);

								vec3 nni0v = obj.velocity(nnit);
								vec3 ni1v = obj.velocity(nit);

								float init = (nit + nnit) / 2f;
								float dt = (nnit - nit) / 2f;
								vec3 inv = obj.value(init);
								vec3 in0v = inv - obj.velocity(init) * dt;
								vec3 in1v = inv + obj.velocity(init) * dt;

								obj.p[obj.getNodeIndex(ni) + 1] = obj.value(nit) + ni1v * dt;
								obj.p[obj.getNodeIndex(nni) - 1] = obj.value(nnit) - nni0v * dt;
								if (objController != null)
									objController.insert(nni, in0v, inv, in1v);
								else
									obj.insert(nni, in0v, inv, in1v);
							}
						}
						if (GUI.Button(removeRect, "-"))
						{
							bIsDirty = true;
							Undo.RecordObject(property.serializedObject.targetObject, "Curve Point Removed");

							if (objController != null)
								objController.remove(i / (obj.chunkSize - 1));
							else
								obj.remove(i / (obj.chunkSize - 1));
						}
					}
				}

				EditorGUI.indentLevel--;
			}
			EditorGUILayout.EndVertical();

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
