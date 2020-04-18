using System;
using UnityEditor;
using UnityEngine;

namespace VavilichevGD.Tools {
	[CustomPropertyDrawer(typeof(BigNumber))]
	public class BigNumberEditor : PropertyDrawer {


		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

			SerializedProperty value = property.FindPropertyRelative("value");
			SerializedProperty order = property.FindPropertyRelative("order");

			EditorGUI.BeginProperty(position, label, property);

			// Draw label
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			// Don't make child fields be indented
			int lastIndent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			// Calculate rects
			Rect valueRect = new Rect(position.x, position.y, position.width - 95, position.height);
			Rect numberTypeRect = new Rect(position.x + valueRect.width + 5, position.y, 90, position.height);

			// Draw fields - passs GUIContent.none to each so they are drawn without labels
			EditorGUI.PropertyField(numberTypeRect, order, GUIContent.none);
			NumberOrder numberOrder = (NumberOrder) order.enumValueIndex;
			float clampedValue = Clamp(value.floatValue, numberOrder);
			value.floatValue = EditorGUI.FloatField(valueRect, clampedValue);

			// Set indent back to what it was
			EditorGUI.indentLevel = lastIndent;

			EditorGUI.EndProperty();

			property.serializedObject.ApplyModifiedProperties();
		}

		private float Clamp(float value, NumberOrder order) {
			float clampedValue = Mathf.Clamp(value, 1f, 999.9f);
			if (order < NumberOrder.Thousands)
				clampedValue = Mathf.FloorToInt(clampedValue);
			else
				clampedValue = (float) Math.Round(clampedValue, 1);
			
			return clampedValue;
		}
	}
}