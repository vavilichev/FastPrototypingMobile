using System;
using UnityEditor;
using UnityEngine;

namespace VavilichevGD.Tools.Numerics {
	[CustomPropertyDrawer(typeof(BigNumberSetting))]
	public class BigNumberSettingEditor : PropertyDrawer {

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

			SerializedProperty cutFloat = property.FindPropertyRelative("cutFloat");
			SerializedProperty order = property.FindPropertyRelative("order");
			
			EditorGUI.BeginProperty(position, label, property);

			// Draw label
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
//
			// Don't make child fields be indented
			int lastIndent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			// Calculate rects
			Rect cutFloatRect = new Rect(position.x, position.y, position.width - 95, position.height);
			Rect orderRect = new Rect(position.x + cutFloatRect.width + 5, position.y, 90, position.height);

			// Draw fields - passs GUIContent.none to each so they are drawn without labels
			EditorGUI.PropertyField(cutFloatRect, cutFloat, GUIContent.none);
			EditorGUI.PropertyField(orderRect, order, GUIContent.none);
			
			EditorGUI.indentLevel = lastIndent;

			cutFloat.floatValue = this.Clamp(cutFloat.floatValue, (BigNumberOrder) order.enumValueIndex);
				
			property.serializedObject.ApplyModifiedProperties();
		}
		
		private float Clamp(float value, BigNumberOrder order) {
			float clampedValue = Mathf.Clamp(value, 0f, 999.99f);
			if (order < BigNumberOrder.Thousands)
				clampedValue = Mathf.FloorToInt(clampedValue);
			else
				clampedValue = (float) Math.Round(clampedValue, 2);
			
			return clampedValue;
		}
	}
}