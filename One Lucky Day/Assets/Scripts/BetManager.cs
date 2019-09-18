using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetManager : MonoBehaviour
{
	//config params
	[SerializeField] int betAmount;

	//state

	//cached component reference
	Canvas canvas;
    GameObject gameManager;
    [SerializeField] GameObject rabbit;
    [SerializeField] GameObject turtle;
    [SerializeField] GameObject lizard;
	public GameObject fighter1;
	public GameObject fighter2;
	public GameObject chosenFighter;
	[SerializeField] GameObject bettingUI;
	[SerializeField] GameObject endScreen;

    void Start()
    {
        var rand = Random.Range(1,4);
        if(rand == 1)
        {
            fighter1 = Instantiate(rabbit);
        }
        else if(rand == 2)
        {
            fighter1 = Instantiate(turtle);
        }
        else
        {
            fighter1 = Instantiate(lizard);
        }
        rand = Random.Range(1,4);
        if(rand == 1)
        {
            fighter2 = Instantiate(rabbit);
        }
        else if(rand == 2)
        {
            fighter2 = Instantiate(turtle);
        }
        else
        {
            fighter2 = Instantiate(lizard);
        }
        fighter1.transform.position = GameObject.Find("Spawn1").transform.position;
        fighter2.transform.position = GameObject.Find("Spawn2").transform.position;
        fighter1.GetComponent<CockFightFighter>().opponent = fighter2;
        fighter2.GetComponent<CockFightFighter>().opponent = fighter1;
        gameManager = FindObjectOfType<GameManager>().gameObject;
        canvas = FindObjectOfType<Canvas>();
        LoadUI();
    }

    void Update()
    {
        CheckForWinner();
    }

    void CheckForWinner()
    {
    	if(fighter1 == null)
    	{
    		if(chosenFighter == fighter2)
    		{
                Win();
    		}
    		else
    		{
    			Lose();
    		}
    	}
    	else if(fighter2 == null)
    	{
    		if(chosenFighter == fighter1)
    		{
                Win();
            }
            else
            {
                Lose();
            }
    	}
    }

    void LoadUI()
    {
    	bettingUI.transform.Find("Fighter1").transform.Find("Image").GetComponent<Image>().sprite = fighter1.GetComponent<SpriteRenderer>().sprite;
    	bettingUI.transform.Find("Fighter2").transform.Find("Image").GetComponent<Image>().sprite = fighter2.GetComponent<SpriteRenderer>().sprite;
        bettingUI.transform.Find("BetAmount").GetComponent<Text>().text = "Bet Amount: " + betAmount.ToString();
    }

    void Win()
    {
        if(!endScreen.activeSelf)
        {
            gameManager.SendMessage("AddToWinnings", betAmount*2);
            endScreen.SetActive(true);
            endScreen.transform.Find("ResultMessage").GetComponent<Text>().text = "Your fighter won!";
            endScreen.transform.Find("WinningsMessage").GetComponent<Text>().text = "You made $" + betAmount + ".";
            endScreen.transform.Find("Image").GetComponent<Image>().sprite = chosenFighter.GetComponent<SpriteRenderer>().sprite;
        }
    }

    void Lose()
    {
        if(!endScreen.activeSelf)
        {
            endScreen.SetActive(true);
            endScreen.transform.Find("ResultMessage").GetComponent<Text>().text = "Your fighter lost.";
            endScreen.transform.Find("WinningsMessage").GetComponent<Text>().text = "You lost $" + betAmount + ".";
            endScreen.transform.Find("WinningsMessage").GetComponent<Text>().color = Color.red;    
        }
    }

    public void BetOnFighter1()
    {
    	chosenFighter = fighter1;
    	Destroy(bettingUI);
    }

    public void BetOnFighter2()
    {
    	chosenFighter = fighter2;
    	Destroy(bettingUI);
    }

    public void EndGame()
    {
        gameManager.SendMessage("LoadLobby");
    }
}
