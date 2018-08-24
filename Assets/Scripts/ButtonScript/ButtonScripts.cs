using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour {
    public AudioSource MainSound;
    bool isSoundOn = true;

    public void Quit()
    {
        Application.Quit();
    }
    /*public void Retry()
    {
        GameObject.Find("Server").GetComponent<Client>().Restart);
        //SceneManager.LoadScene("SampleScene");
    }*/
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void SoundOnOff()
    {
        if (isSoundOn)
        {
            MainSound.volume = 0;
            isSoundOn = false;
        }
        else
        {
            MainSound.volume = 1;
            isSoundOn = true;
        }
    }
}
