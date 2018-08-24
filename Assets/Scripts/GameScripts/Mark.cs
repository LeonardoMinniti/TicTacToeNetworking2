using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Mark : MonoBehaviour
{
    public SpriteRenderer box;

    public static string sendInfo;

    public void OnMouseDown()
    {
        if (box.sprite == null)
        {
            if (GameManager.round % 2 == 0 && !Client.IsX)
            {
                sendInfo = this.gameObject.name;
            }
            else if (GameManager.round % 2 != 0 && Client.IsX)
            {
                sendInfo = this.gameObject.name;
            }
        }
        else
        {
            Debug.Log("this slot is already Taken");
        }

        if (sendInfo != GameManager.tempSendInfo)
        {
            GameManager.tempSendInfo = sendInfo;
        }
    }

    public void Retry()
    {
        if(box.sprite != null)
        {
            sendInfo = this.gameObject.name;
            sendInfo = null;
        }
    }
}
