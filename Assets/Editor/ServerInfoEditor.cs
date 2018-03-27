using UnityEngine;
using UnityEditor;
using System.IO;
using Game.Login;

public class ServerInfoEditor : EditorWindow
{
    public ServerInfo serverInfo;

    private Rect _scrollRect = new Rect(10, 40, 300, 280);
    private Rect _scrollViewRect = new Rect(0, 0, 280, 280);

    //定义默认的滚动条位置为0,0
    private Vector2 _scrollPos = Vector2.zero;

    [MenuItem("Window/ServerInfoEditor")]
    static void Init()
    {
        EditorWindow.GetWindowWithRect(typeof(ServerInfoEditor), new Rect(0,0,510, 600)).Show();
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

        if (serverInfo != null)
        {
            if (GUILayout.Button("Save data"))
            {
                SaveGameData();
            }

            _scrollPos = GUI.BeginScrollView(new Rect(10, 100, 500, 500), _scrollPos, new Rect(0, 0, 500, 600));

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("serverInfo");
            EditorGUILayout.PropertyField(serializedProperty, true);
            serializedObject.ApplyModifiedProperties();

            // 结束滚动视图
            GUI.EndScrollView();
        }
    }

    private void LoadGameData()
    {
        string filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "json");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            serverInfo = JsonUtility.FromJson<ServerInfo>(dataAsJson);
        }
    }

    private void SaveGameData()
    {
        string filePath = EditorUtility.SaveFilePanel("Save localization data file", Application.streamingAssetsPath, "", "json");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(serverInfo);
            File.WriteAllText(filePath, dataAsJson);
        }
    }

    private void CreateNewData()
    {
        serverInfo = new ServerInfo();
    }

}
