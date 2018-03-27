using UnityEngine;
using UnityEditor;
using System.IO;

public class LocalizedTextEditor : EditorWindow
{
    public LocalizationData localizationData;

    private Rect _scrollRect = new Rect(10, 40, 300, 280);
    private Rect _scrollViewRect = new Rect(0, 0, 280, 280);

    private Vector2 _scrollPos = Vector2.zero;

    [MenuItem("Window/Localized Text Editor")]
    static void Init()
    {
        EditorWindow.GetWindowWithRect(typeof(LocalizedTextEditor), new Rect(0, 0, 510, 600)).Show();
    }

    private void OnGUI()
    {

        if (GUILayout.Button("Load data"))
        {
            LoadGameData();
        }

        if (GUILayout.Button("Create new data"))
        {
            CreateNewData();
        }

        if (localizationData != null)
        {
            if (GUILayout.Button("Save data"))
            {
                SaveGameData();
            }

            _scrollPos = GUI.BeginScrollView(new Rect(10, 100, 500, 500), _scrollPos, new Rect(0, 0, 500, 600));

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("localizationData");
            EditorGUILayout.PropertyField(serializedProperty, true);
            serializedObject.ApplyModifiedProperties();

            GUI.EndScrollView();
        }

    }

    private void LoadGameData()
    {
        string filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "json");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);

            localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
        }
    }

    private void SaveGameData()
    {
        string filePath = EditorUtility.SaveFilePanel("Save localization data file", Application.streamingAssetsPath, "", "json");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(localizationData);
            File.WriteAllText(filePath, dataAsJson);
        }
    }

    private void CreateNewData()
    {
        localizationData = new LocalizationData();
    }

}