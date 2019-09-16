using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TugOfWarGameStatus : MonoBehaviour
{
	//config params
	[SerializeField] float distanceToWin;
	[SerializeField] float prizeMoney;

	//state
	public bool gameOver;

	//cached component reference
	GameManager gameManager;
	Rope rope;

	void Start()
	{
		gameManager = FindObjectOfType<GameManager>();
		rope = FindObjectOfType<Rope>();
	}

	void Update()
    {
        CheckForWinner();
    }

    void CheckForWinner()
    {
        if(rope.transform.position.x <= distanceToWin*-1)
        {
            gameOver = true;
	    	gameManager.SendMessage("AddToWinnings", prizeMoney);	
            FindObjectOfType<TugOfWarOpponent>().gameObject.SendMessage("Die");
            Invoke("EndGame", 2);
        }
        else if(rope.transform.position.x >= distanceToWin)
        {
        	gameOver = true;
            FindObjectOfType<TugOfWarPlayer>().gameObject.SendMessage("Die");
            Invoke("EndGame", 2);
        }
    }

    void EndGame()
    {
        gameManager.SendMessage("LoadLobby");
    }
}
