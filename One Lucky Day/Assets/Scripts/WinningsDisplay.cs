﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.UI;

public class WinningsDisplay : MonoBehaviour
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
        text.text = gameManager.GetWinnings().ToString();
    }
}
