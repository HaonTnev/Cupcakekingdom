using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        Loader.Load(Loader.Scene.GameScene);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public GameObject credits;
    public GameObject buttons;

    public void CreditsButton()
    {
        
        FindObjectOfType<TextMeshProUGUI>().enabled = true; 
        credits.SetActive(true);
        buttons.SetActive(false);
        
    }
    public void BackButton()
    {
        credits.SetActive(false);
        buttons.SetActive(true);
    }
}
