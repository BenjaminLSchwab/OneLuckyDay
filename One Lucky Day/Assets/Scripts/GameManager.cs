using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int playerStartingMoney = 42;
    [SerializeField] int chancesToPlay = 10;
    int playerMoney = 0;
    int gamesPlayed = 0;
    private string selectedGame = "";
    // Start is called before the first frame update
    void Start()
    {
        playerMoney = playerStartingMoney;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectGame(string game)
    {
        selectedGame = game;
    }

    public void LoadGame()
    {
        if (selectedGame == "")
        {
            Debug.Log("No game selected.");
        }
        else
        {
            gamesPlayed++;
            SceneManager.LoadScene(selectedGame);
        }
    }

    public void LoadLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public int GetMoney()
    {
        return playerMoney;
    }

    public void AddToMoney(int ammount)
    {
        playerMoney += ammount;
    }

    public void SubtractFromMoney(int ammount)
    {
        playerMoney -= ammount;
    }
}
