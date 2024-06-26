using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Properties))]
public class PropertiesEditor : Editor
{
    SerializedProperty propertiesList;
    SerializedProperty isFoodProp;
    SerializedProperty canEatFoodProp;
    SerializedProperty isFlammableProp;
    SerializedProperty isFireProp;
    SerializedProperty isWheelProp;
    SerializedProperty isVehicleProp;

    SerializedProperty fixedProperties;

    private void OnEnable()
    {
        propertiesList = serializedObject.FindProperty("properties");
        isFoodProp = serializedObject.FindProperty("isFood");
        canEatFoodProp = serializedObject.FindProperty("canEatFood");
        isFlammableProp = serializedObject.FindProperty("isFlammable");
        isFireProp = serializedObject.FindProperty("isFire");
        isWheelProp = serializedObject.FindProperty("isWheel");
        isVehicleProp = serializedObject.FindProperty("isVehicle");
        fixedProperties = serializedObject.FindProperty("fixedProperties");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // add all the boolians to a dropdown in the inspectopr after start
        if (!fixedProperties.boolValue)
        {
            EditorGUILayout.LabelField("Properties", EditorStyles.boldLabel);

            for (int i = 0; i < propertiesList.arraySize; i++)
            {
                SerializedProperty property = propertiesList.GetArrayElementAtIndex(i);
                string label = ((Properties.Property)i).ToString();
                property.boolValue = EditorGUILayout.Toggle(label, property.boolValue);
            }

            EditorGUILayout.Space();
        }

        EditorGUILayout.PropertyField(fixedProperties, new GUIContent("Use Fixed Properties"));

        // show all the booleans if fixedProperties is enabled 
        if (fixedProperties.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(isFoodProp, new GUIContent("Is Food"));
            EditorGUILayout.PropertyField(canEatFoodProp, new GUIContent("Can Eat Food"));
            EditorGUILayout.PropertyField(isFlammableProp, new GUIContent("Is Flammable"));
            EditorGUILayout.PropertyField(isFireProp, new GUIContent("Is Fire"));
            EditorGUILayout.PropertyField(isWheelProp, new GUIContent("Is Wheel"));
            EditorGUILayout.PropertyField(isVehicleProp, new GUIContent("Is Vehicle"));
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space();

        // Draw the rest of the default inspector excluding the properties we've manually drawn
        DrawPropertiesExcluding(serializedObject, "m_Script", "fixedProperties", "properties", "isFood", "canEatFood", "isFlammable", "isFire", "isWheel", "isVehicle");

        serializedObject.ApplyModifiedProperties();
    }
}