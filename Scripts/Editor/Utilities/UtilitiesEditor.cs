using System;
using System.Collections.Generic;
using UnityEditor;
using Utilities.Constants;
using Utilities.Extensions;

namespace Utilities.Editor.Utilities
{
#if UNITY_EDITOR
	[CustomEditor(typeof(UtilityController))]
	[CanEditMultipleObjects]
	public class UtilitiesEditor : UnityEditor.Editor
	{
		#region FIELDS

		private SerializedProperty _enabledServices;
		private List<(Type interfaceType, Type classType)> _services;
		
		#endregion

		#region IMPLEMENTATION OF: ViewBehaviourEditor

		public override void OnInspectorGUI()
		{
			EditorGUILayout.LabelField(ConstantMessages.GENERAL_SETTINGS, EditorStyles.boldLabel);

			base.OnInspectorGUI();

			EditorGUILayout.Space(10);
			EditorGUILayout.LabelField(ConstantMessages.SPECIFIC_SETTINGS, EditorStyles.boldLabel);

			serializedObject.Update();

			var counter = 0;
			foreach (var service in _services)
			{
				var sp = _enabledServices.GetArrayElementAtIndex(counter);
				sp.boolValue = EditorGUILayout.Toggle(service.classType.Name.SplitCamelCase(), sp.boolValue);

				counter++;
			}


			serializedObject.ApplyModifiedProperties();
		}
		
		private void OnEnable()
		{
			_enabledServices = serializedObject.FindProperty("enabledServicesHelper");
			_services = UtilityController.GetSortedServices();

			if (!_enabledServices.isArray || _enabledServices.arraySize >= _services.Count) 
				return;

			_enabledServices.ClearArray();
			var startingArraySize = _enabledServices.arraySize;
			var difference = _services.Count - startingArraySize;
			for (var i = startingArraySize; i < difference; i++)
			{
				_enabledServices.InsertArrayElementAtIndex(i);		
			}
			
			serializedObject.ApplyModifiedProperties();
		}
		
		#endregion
	}
#endif
}