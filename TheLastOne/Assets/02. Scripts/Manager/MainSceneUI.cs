using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneUI : MonoBehaviour
{
    public static MainSceneUI instance;
    public GameObject[] ClearScreen;
    public Loby_button loby_Button;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ClearEffect(int i)
    {
        ClearScreen[i-1].SetActive(true);
    }
    public void GameStart(int i)
    {
        StatusManager.Instance.stageIndex = i;
        SceneManager.LoadScene("CardActionTest");
    }

    public void OpenSelectStageScreen()
    {
        loby_Button.B_start_button();
    }

}
