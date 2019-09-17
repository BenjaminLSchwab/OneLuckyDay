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
    int winnings = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerMoney = playerStartingMoney;
    }

    private void Awake()
    {
        SetUpSingleton();
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
        if (winnings >= 0)
        {
            AddToMoney(winnings);
        }
        else
        {
            SubtractFromMoney(winnings);
        }
    }

    public int GetMoney()
    {
        return playerMoney;
    }

    public void AddToMoney(int amount)
    {
        playerMoney += amount;
    }

    public void SubtractFromMoney(int amount)
    {
        playerMoney -= amount;
    }

    public void AddToWinnings(int amount)
    {
        winnings += amount;
    }
}
