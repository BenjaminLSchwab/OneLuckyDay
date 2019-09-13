using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TugOfWarOpponent : MonoBehaviour
{
	//config params
	[SerializeField] float tugDelay;
	[SerializeField] float tugStrength;

	//state
	float tugTimer;

	//cached component reference
	GameObject rope;

    void Start()
    {
        rope = FindObjectOfType<Rope>().gameObject;
        tugTimer = tugDelay;
    }

    void Update()
    {
        tugTimer -= Time.deltaTime;
        if(tugTimer <= 0)
        {
        	tugTimer = tugDelay;
        	rope.SendMessage("Tug", tugStrength);
        }
    }
}
