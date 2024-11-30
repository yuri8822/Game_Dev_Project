using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Script : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject Map;

    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
        Map.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectLevelClicked()
    {
        MainMenu.SetActive(false);
        Map.SetActive(true);
    }

    public void BackClicked()
    {
        MainMenu.SetActive(true);
        Map.SetActive(false);
    }

    public void QuitToDesktopClicked()
    {
        Application.Quit();
    }
}
