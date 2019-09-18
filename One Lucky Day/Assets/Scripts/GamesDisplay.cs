using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamesDisplay : MonoBehaviour
{
    Text text;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        gameManager = FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        var numberLeft = gameManager.GetGamesLeft();
        if (numberLeft == 0)
        {
            text.text = "Last Game!";
        }
        else
        {

        text.text = gameManager.GetGamesLeft().ToString();
        }
    }
}
