using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : Singleton<StatusManager>
{
    public bool[] isStageClear = new bool[5];
    private string savePath;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        string saveData = PlayerPrefs.GetString(savePath, "null");
        if (saveData != "null")
        {
            isStageClear = JsonUtility.FromJson<bool[]>(saveData);
        }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString(savePath, JsonUtility.ToJson(isStageClear));
    }
}
