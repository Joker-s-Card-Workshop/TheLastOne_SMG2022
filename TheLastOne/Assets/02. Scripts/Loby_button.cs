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
    public GameObject[] stageB_Lock;
    public GameObject[] preview;
    public GameObject   in_game_setting;
    public GameObject[] StageEnd;
    public GameObject[] StageEndStory;
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
            stageB_Lock[i].SetActive(!StatusManager.Instance.isStageClear[i]);
        }
    }
    public void B_open_in_game_setting()
    {
        in_game_setting.SetActive(true);
    }
    public void B_close_in_game_setting()
    {
        in_game_setting.SetActive(false);
    }
    public void B_Stage_end2_story(int option)
    {
        StageEnd[option].SetActive(false);
        StageEndStory[option].SetActive(true);
    }
    public void B_2Stage_select()
    {
        Stage_info_Refresh();
        stageSelect.SetActive(true);
        for (int i = 1; i < 6; i++)
        {
            StageEnd[i].SetActive(false);
            StageEndStory[i].SetActive(false);
        }
    }
}
