using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static int round;
    public GameObject TicTacToe;
    
    public SpriteRenderer[] tiles;
    public static bool winner;
    public static string tempSendInfo;


    // Update is called once per frame
    void Update()
    {
        if (!winner)
        {
            if (tiles[0].sprite == tiles[1].sprite && tiles[0].sprite == tiles[2].sprite && tiles[0].sprite != null)
            {
                Debug.Log("You win");
                winner = true;

            }else if(tiles[0].sprite == tiles[3].sprite && tiles[0].sprite == tiles[6].sprite && tiles[0].sprite != null)
            {
                Debug.Log("You win");
                winner = true;
            }else if (tiles[0].sprite == tiles[4].sprite && tiles[0].sprite == tiles[8].sprite && tiles[0].sprite != null)
            {
                Debug.Log("You win");
                winner = true;
            }else if(tiles[3].sprite == tiles[4].sprite && tiles[3].sprite == tiles[5].sprite && tiles[3].sprite != null)
            {
                Debug.Log("You win");
                winner = true;
            }else if(tiles[6].sprite == tiles[7].sprite && tiles[6].sprite == tiles[8].sprite && tiles[6].sprite != null)
            {
                Debug.Log("You win");
                winner = true;
            }else if(tiles[2].sprite == tiles[4].sprite && tiles[2].sprite == tiles[6].sprite && tiles[2].sprite != null)
            {
                Debug.Log("You win");
                winner = true;
            }else if(tiles[1].sprite == tiles[4].sprite && tiles[1].sprite == tiles[7].sprite && tiles[1].sprite != null)
            {
                Debug.Log("You win");
                winner = true;
            }else if(tiles[2].sprite == tiles[5].sprite && tiles[2].sprite == tiles[8].sprite && tiles[2].sprite != null)
            {
                Debug.Log("You win");
                winner = true;
            }
            if(round == 9)
            {
                Debug.Log("Draw");
            }
            if (winner)
            {
                if(round % 2 != 0)
                {
                    Debug.Log("Player1 Wins");
                }
                else
                {
                    Debug.Log("Player2 Wins");
                }
            }

        }

    }

}
