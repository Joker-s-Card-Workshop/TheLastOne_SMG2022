using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : Singleton<StatusManager>
{
    public bool[] isStageClear = new bool[6];
    private string savePath = "DataSavePath";
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        isStageClear[1] = true;
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
