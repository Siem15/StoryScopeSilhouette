using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ConsoleToGUI : MonoBehaviour
{
    string myLog = "*begin log";
    string fileName = "";
    bool showOptions = true;
    int kChars = 700;
    // public GameObject sceneCanvas;

    private void Start() => ShowOptions();

    void OnEnable() => Application.logMessageReceived += Log;

    void OnDisable() => Application.logMessageReceived -= Log;

    void Update() 
    {
        if (Input.GetKey(KeyCode.Space) && Input.GetKeyDown(KeyCode.L)) 
        {
            ShowOptions();
        }        
    }

    public void ShowOptions()
    {
        showOptions = !showOptions;
        Cursor.visible = showOptions;    
        //sceneCanvas.SetActive(doShow);

#if !UNITY_EDITOR
        Debug.Log(System.DateTime.Now);
#endif
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        // for onscreen...
        myLog = $"{myLog}\n{logString}";

        if (myLog.Length > kChars) 
        {
            myLog = myLog.Substring(myLog.Length - kChars); 
        }

        // for the file ...
        if (fileName == string.Empty)
        {
            string directory = "C:/StoryScopeMedia/Scene/YOUR_LOGS";
            System.IO.Directory.CreateDirectory(directory);
            string randomInteger = Random.Range(1000, 9999).ToString();
            fileName = $"{directory}/log-{randomInteger}.txt";
        }

        try 
        { 
            System.IO.File.AppendAllText(fileName, $"{logString}\n"); 
        }
        catch 
        { 

        }
    }

    private void OnGUI()
    {
        if (!showOptions) 
        { 
            return; 
        }

        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity,
        new Vector3(Screen.width / 1200.0f, Screen.height / 800.0f, 1.0f));
        GUI.TextArea(new Rect(10, 10, 540, 370), myLog);
    }
}