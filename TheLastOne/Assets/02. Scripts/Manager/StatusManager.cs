using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class SaveClass
{
    public bool[] isStageClear = new bool[6];
}

public class StatusManager : MonoBehaviour
{
    public static StatusManager Instance;
    public bool[] isStageClear = new bool[6];
    public int stageIndex = 0;

    private SaveClass saveClass = new SaveClass();
    private string savePath = "DataSavePath";
    [SerializeField]
    private bool DEBUG;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        SoundManager.Instance.PlaySoundClip("BGM_4", SoundType.BGM);

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
    public IEnumerator ClearCheck()
    {
        yield return new WaitForSeconds(1);
        if (FindObjectsOfType<CardInfo>().Length <= 1)
        {
            if (stageIndex >= 5) SceneManager.LoadScene("EndScene");

            SceneManager.LoadScene("Main");
            if (stageIndex <= 4)
                isStageClear[stageIndex + 1] = true;
            MainSceneUI.instance.ClearEffect(stageIndex);
        }
    }
}
