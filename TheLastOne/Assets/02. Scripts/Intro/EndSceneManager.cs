using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GoNextScene();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoNextScene()
    {
        SceneManager.LoadScene("Main");
        MainSceneUI.instance.OpenCreditScreen();
    }
}
