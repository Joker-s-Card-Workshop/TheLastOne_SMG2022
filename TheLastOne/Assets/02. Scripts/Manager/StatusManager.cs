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
        var cards = FindObjectsOfType<CardInfo>();

        bool isClear = false;

        if (cards.Length <= 1)
            switch (cards[0].mydata.CardName)
            {
                case "Citizen":
                    if (stageIndex == 1)
                        isClear = true;
                    break;
                case "Knight":
                    if (stageIndex == 2)
                        isClear = true;
                    break;
                case "Novel":
                    if (stageIndex == 3)
                        isClear = true;
                    break;
                case "KingsMan":
                    if (stageIndex == 4)
                        isClear = true;
                    break;
                case "King":
                    if (stageIndex == 5)
                        isClear = true;
                    break;
            }
        Debug.Log(isClear);
        if (isClear)
        {
            if (stageIndex >= 5)
            {
                 SceneManager.LoadScene("EndScene");
            }
            else
            {
                SceneManager.LoadScene("Main");
                if (stageIndex <= 4)
                    isStageClear[stageIndex + 1] = true;
                MainSceneUI.instance.ClearEffect(stageIndex, true);
            }
        }
        else if (cards.Length <= 1)
        {
            SceneManager.LoadScene("Main");
            MainSceneUI.instance.ClearEffect(stageIndex, false);
        }
    }
}
