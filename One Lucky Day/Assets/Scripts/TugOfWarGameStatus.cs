using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TugOfWarGameStatus : MonoBehaviour
{
	//config params
	[SerializeField] float distanceToWin;
	[SerializeField] float prizeMoney;

	//state
	public bool gameOver;
    bool loadedLobby = false;

	//cached component reference
	GameManager gameManager;
	Rope rope;
    [SerializeField] AudioClip sizzleSound;
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject alien1;
    [SerializeField] GameObject alien2;

	void Start()
	{
		gameManager = FindObjectOfType<GameManager>();
		rope = FindObjectOfType<Rope>();
        var rand = Random.Range(1,3);
        if(rand == 1)
        {
            alien1.SetActive(true);
        }
        else
        {
            alien2.SetActive(true);
        }
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
            FindObjectOfType<TugOfWarOpponent>().gameObject.SendMessage("Die");
            StartCoroutine("Win");
            if(!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().clip = sizzleSound;
                GetComponent<AudioSource>().Play();    
            } 
        }
        else if(rope.transform.position.x >= distanceToWin)
        {
        	gameOver = true;
            FindObjectOfType<TugOfWarPlayer>().gameObject.SendMessage("Die");
            StartCoroutine("Lose");
            if(!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().clip = sizzleSound;
                GetComponent<AudioSource>().Play();    
            } 
        }
    }

    IEnumerator Win()
    {
        if (!loadedLobby)
        {
            loadedLobby = true;
            endScreen.SetActive(true);
            endScreen.transform.Find("Message").GetComponent<Text>().text = "You won!";
            endScreen.transform.Find("Message").GetComponent<Text>().color = Color.green;
            gameManager.SendMessage("AddToWinnings", prizeMoney);   
            yield return new WaitForSeconds(3);
            gameManager.SendMessage("LoadLobby");
        }

    }

    IEnumerator Lose()
    {
        endScreen.SetActive(true);
        endScreen.transform.Find("Message").GetComponent<Text>().text = "You lost";
        endScreen.transform.Find("Message").GetComponent<Text>().color = Color.red;
        yield return new WaitForSeconds(3);
        gameManager.SendMessage("LoadLobby");
    }
}
