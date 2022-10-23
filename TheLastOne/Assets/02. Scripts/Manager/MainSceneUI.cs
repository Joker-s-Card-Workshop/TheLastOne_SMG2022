using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneUI : MonoBehaviour
{
    public static MainSceneUI instance;
    public GameObject[] ClearScreen;
    public GameObject[] ClearNextButton;
    public GameObject[] BackButton;
    public Loby_button loby_Button;

    public GameObject MainScreen;
    public GameObject CreditScreen;

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
    public void ClearEffect(int i, bool isClear)
    {
        ClearScreen[i-1].SetActive(true);
        if (isClear)
        {
            ClearNextButton[i - 1].SetActive(true);
            BackButton[i - 1].SetActive(false);
        }
        else
        {
            ClearNextButton[i - 1].SetActive(false);
            BackButton[i - 1].SetActive(true);
        }
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

    public void OpenCreditScreen()
    {
        MainScreen.SetActive(true);
        CreditScreen.SetActive(true);
    }
}
