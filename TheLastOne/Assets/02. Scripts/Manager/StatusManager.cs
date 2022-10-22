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
    public int stageIndex = 0;

    private SaveClass saveClass = new SaveClass();
    private string savePath = "DataSavePath";
    [SerializeField]
    private bool DEBUG;
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
    public void ClearCheck()
    {
        if (FindObjectsOfType<CardInfo>().Length <= 1)
        {
            isStageClear[stageIndex] = true;
        }
    }
}
