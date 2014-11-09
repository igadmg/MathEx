using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemEx;
using UnityEditor;
using UnityEngine;

namespace MathEx.UnityEditor
{
	[CustomPropertyDrawer(typeof(vec3))]
	class PropertyDrawer_vec3 : PropertyDrawer
	{
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
			vec3 obj = (vec3) fieldInfo.GetValue(property.serializedObject.targetObject);
			
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
			EditorGUI.TextField(contentPosition, "m", obj.magnitude.ToString());
		}
	}
}
