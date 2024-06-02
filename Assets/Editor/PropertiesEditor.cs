using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Properties))]
public class PropertiesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Properties propertiesScript = (Properties)target;

        // Draw default inspector
        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Properties", EditorStyles.boldLabel);

        // Draw each property with a label
        for (int i = 0; i < propertiesScript.properties.Count; i++)
        {
            string label = ((Properties.Property)i).ToString();
            propertiesScript.properties[i] = EditorGUILayout.Toggle(label, propertiesScript.properties[i]);
        }

        // Save any changes made in the inspectorr
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
