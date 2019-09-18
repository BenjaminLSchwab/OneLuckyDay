using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booth : MonoBehaviour
{
    [SerializeField] string gameName = "";
    [SerializeField] GameObject indicator;
    [SerializeField] int gameCost = 10;
    GameManager gameManager;
    IndicatorManager indicatorManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        indicatorManager = FindObjectOfType<IndicatorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.SendMessage("SelectGame", gameName);
        gameManager.gameCost = gameCost;
        indicatorManager.SendMessage("TurnOffAllIndicators");
        indicator.SetActive(true);
    }

}
