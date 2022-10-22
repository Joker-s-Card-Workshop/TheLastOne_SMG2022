using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Loby_button : MonoBehaviour
{
    public GameObject   mainMenu;
    public GameObject   stageSelect;
    public GameObject[] stageB;
    public GameObject[] stageBLock;
    public GameObject[] preview;
    public void B_start_button()
    {
        mainMenu.SetActive(false);
        Stage_info_Refresh();
        stageSelect.SetActive(true);
    }
    public void B_exit2menu_button()
    {
        stageSelect.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void B_open_preview(int option)
    {
        preview[option].SetActive(true);
    }
    public void B_close_preview(int option)
    {
        preview[option].SetActive(false);
    }
    public void Stage_info_Refresh()
    {
        for (int i = 1; i < 6; i++)
        {
            stageB[i].SetActive(StatusManager.Instance.isStageClear[i]);
            stageBLock[i].SetActive(!StatusManager.Instance.isStageClear[i]);
        }
    }
}
