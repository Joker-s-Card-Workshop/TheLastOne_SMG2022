using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneUI : MonoBehaviour
{
    public void GameStart(int i)
    {
        StatusManager.Instance.stageIndex = i;
        SceneManager.LoadScene("CardActionTest");
    }

}
