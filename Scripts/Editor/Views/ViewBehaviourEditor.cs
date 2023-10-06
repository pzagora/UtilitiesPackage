using UnityEditor;
using Utilities.Constants;
using Utilities.Services.ViewManagement.Views;

namespace Utilities.Editor.Views
{
#if UNITY_EDITOR
	[CustomEditor(typeof(ViewBehaviour), true)]
	[CanEditMultipleObjects]
	public class ViewBehaviourEditor : UnityEditor.Editor
	{
		#region FIELDS
		
		private SerializedProperty _animateView;
		private SerializedProperty _viewAnimations;

		#endregion

		#region IMPLEMENTATION OF: 

		

		#endregion

		private void OnEnable()
		{
			_animateView = serializedObject.FindProperty("animateView");
			_viewAnimations = serializedObject.FindProperty("viewAnimations");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUILayout.LabelField(ConstantMessages.GENERAL_SETTINGS, EditorStyles.boldLabel);

			EditorGUILayout.PropertyField(_animateView);
			if (_animateView.boolValue)
			{
				EditorGUI.indentLevel = 1;
				EditorGUILayout.PropertyField(_viewAnimations);
				EditorGUI.indentLevel = 0;
			}

			serializedObject.ApplyModifiedProperties();

			EditorGUILayout.Space(10);
			
			EditorGUILayout.LabelField(ConstantMessages.SPECIFIC_SETTINGS, EditorStyles.boldLabel);
			base.OnInspectorGUI();

			serializedObject.ApplyModifiedProperties();
		}
	}
#endif
}