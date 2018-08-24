using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour {

    public void Quit()
    {
        Application.Quit();
    }
    /*public void Retry()
    {
        GameObject.Find("Server").GetComponent<Client>().Restart);
        //SceneManager.LoadScene("SampleScene");
    }*/
}
