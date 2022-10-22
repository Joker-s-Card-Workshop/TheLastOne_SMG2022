using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class SaveClass
{
    public bool[] isStageClear = new bool[6];
}

public class StatusManager : Singleton<StatusManager>
{
    public bool[] isStageClear = new bool[6];
    private SaveClass saveClass;
    private string savePath = "DataSavePath";
    [SerializeField]
    private bool DEBUG;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        isStageClear[1] = true;
    }
    private void Start()
    {
        string saveData = PlayerPrefs.GetString(savePath, "null");
        if (saveData != "null" && !DEBUG)
        {
            saveClass = JsonUtility.FromJson<SaveClass>(saveData);
            isStageClear = saveClass.isStageClear;
        }
    }
    private void OnApplicationQuit()
    {
        saveClass.isStageClear = isStageClear;
        PlayerPrefs.SetString(savePath, JsonUtility.ToJson(saveClass));
    }
}
