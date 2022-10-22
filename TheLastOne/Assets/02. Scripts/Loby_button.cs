using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Loby_button : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject stageSelect;
    public GameObject[] preview;
    public void B_start_button()
    {
        mainMenu.SetActive(false);
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
}
