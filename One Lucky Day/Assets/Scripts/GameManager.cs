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
    [SerializeField] int moneyToWin = 10000;
    [SerializeField] AudioClip chingNoise;
    [SerializeField] float chingVolume = 1f;
    [SerializeField] AudioClip spendNoise;
    [SerializeField] float spendVolume = 1f;
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
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
                    CheckForGameOver();
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
                    SceneManager.LoadScene(selectedGame);
                    gamesPlayed++;
                }
            }
        }
    }

    void CheckForGameOver()
    {
        if (gamesPlayed > chancesToPlay)
        {
            if (playerMoney > moneyToWin)
            {
                SceneManager.LoadScene("GoodEnd");
            }
            else
            {
                SceneManager.LoadScene("BadEnd");
            }
        }
    }

    IEnumerator AddMoneyWithDelay()
    {
        yield return new WaitForSeconds(1);
        AddToMoney(winnings);
        CheckForGameOver();
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
        if (loadingGame || gamesPlayed > chancesToPlay) return;
        if (playerMoney >= gameCost)
        {
            SubtractFromMoney(gameCost);
            loadingGame = true;
        }

    }

    public void LoadLobby()
    {
        loadingLobby = true;
    }

    public void Restart()
    {
        playerMoney = playerStartingMoney;
        gamesPlayed = 0;
        SceneManager.LoadScene("Lobby");
    }

    public void Exit()
    {
        Application.Quit();
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
        AudioSource.PlayClipAtPoint(chingNoise, transform.position, chingVolume);
        playerMoney += amount;
        winnings = 0;
        var disp = FindObjectOfType<DifferenceDisplay>();
        disp.displayText.text = "+" + amount.ToString();
        disp.SendMessage("SetDisplayActive");
    }

    public void SubtractFromMoney(int amount)
    {
        AudioSource.PlayClipAtPoint(spendNoise, transform.position, spendVolume);
        playerMoney -= amount;
        var disp = FindObjectOfType<DifferenceDisplay>();
        disp.displayText.text = "-" + amount.ToString();
        disp.SendMessage("SetDisplayActive");
    }

    public void AddToWinnings(int amount)
    {
        winnings += amount;
    }
}
