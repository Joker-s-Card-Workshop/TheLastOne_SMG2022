using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Loby_button : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject stageSelect;
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
}
