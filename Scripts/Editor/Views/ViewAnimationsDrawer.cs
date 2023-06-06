using UnityEditor;
using UnityEngine;
using Utilities.Models.Animations;

namespace Utilities.Editor.Views
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ViewAnimations))]
    public class ViewAnimationsDrawer : PropertyDrawer
    {
        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't indent child fields
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            var amountRect = new Rect(position.x, position.y, position.width / 2 - 2, position.height);
            var unitRect = new Rect(position.x + position.width / 2 + 2, position.y, position.width / 2 - 2, position.height);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("openAnimationClip"), GUIContent.none);
            EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("closeAnimationClip"), GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
#endif
}