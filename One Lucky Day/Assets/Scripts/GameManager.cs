using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] int playerStartingMoney = 42;
    [SerializeField] int chancesToPlay = 10;
    [SerializeField] float loadLobbyDelay = 1.5f;
    [SerializeField] float loadGameDelay = 2f;
    int playerMoney = 0;
    int gamesPlayed = 0;
    private string selectedGame = "";
    float loadLobbyTimer = 0;
    float loadGameTimer = 0;
    bool loadingLobby = false;
    bool loadingGame = false;
    
    int winnings = 0;
    public int gameCost;
    // Start is called before the first frame update
    void Start()
    {
        SetUpSingleton();
        playerMoney = playerStartingMoney;
        loadLobbyTimer = loadLobbyDelay;
        loadGameTimer = loadGameDelay;
    }





    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (loadingLobby)
        {
            loadLobbyTimer -= Time.deltaTime;
            if (loadLobbyTimer < 0)
            {
                loadingLobby = false;
                loadLobbyTimer = loadLobbyDelay;
                SceneManager.LoadScene("Lobby");
                if (winnings >= 0)
                {
                    StartCoroutine(AddMoneyWithDelay());
                }
                else
                {
                    SubtractFromMoney(winnings);
                }
                
            }
        }
        else if (loadingGame)
        {
            loadGameTimer -= Time.deltaTime;
            if (loadGameTimer < 0)
            {
                loadingGame = false;
                loadGameTimer = loadGameDelay;
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
        }
    }

    IEnumerator AddMoneyWithDelay()
    {
        yield return new WaitForSeconds(1);
        AddToMoney(winnings);
    }

    public int GetGamesLeft()
    {
        return chancesToPlay - gamesPlayed;
    }

    public void SelectGame(string game)
    {
        selectedGame = game;
    }

    public void LoadGame()
    {
        if (loadingGame) return;
        SubtractFromMoney(gameCost);
        loadingGame = true;
    }

    public void LoadLobby()
    {
        loadingLobby = true;
    }



    public int GetMoney()
    {
        return playerMoney;
    }

    public int GetWinnings()
    {
        return winnings;
    }

    public void AddToMoney(int amount)
    {
        playerMoney += amount;
        winnings = 0;
        var disp = FindObjectOfType<DifferenceDisplay>();
        disp.displayText.text = amount.ToString();
        disp.SendMessage("SetDisplayActive");
    }

    public void SubtractFromMoney(int amount)
    {
        playerMoney -= amount;
        var disp = FindObjectOfType<DifferenceDisplay>();
        disp.displayText.text = amount.ToString();
        disp.SendMessage("SetDisplayActive");
    }

    public void AddToWinnings(int amount)
    {
        winnings += amount;
    }
}
