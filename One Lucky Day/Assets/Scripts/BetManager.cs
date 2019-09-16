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
	[SerializeField] GameObject fighter1;
	[SerializeField] GameObject fighter2;
	public GameObject chosenFighter;
	[SerializeField] GameObject bettingUI;
	[SerializeField] GameObject endScreen;

    void Start()
    {
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
        gameManager.SendMessage("SubtractFromMoney", betAmount);
    }

    public void BetOnFighter2()
    {
    	chosenFighter = fighter2;
    	Destroy(bettingUI);
        gameManager.SendMessage("SubtractFromMoney", betAmount);
    }

    public void EndGame()
    {
        gameManager.SendMessage("LoadLobby");
    }
}
