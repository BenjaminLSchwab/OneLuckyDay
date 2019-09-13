﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorManager : MonoBehaviour
{
    [SerializeField] List<GameObject> indicators;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOffAllIndicators()
    {
        foreach (var indicator in indicators)
        {
            indicator.SetActive(false);
        }
    }
}
