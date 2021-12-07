using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EventListeners))]
public class EventListenersEditor : Editor
{
    SerializedProperty eventsToListen;
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(eventsToListen, true);
        serializedObject.ApplyModifiedProperties();
    }

    private void OnEnable()
    {
        eventsToListen = serializedObject.FindProperty("eventsToListen");
    }
}
