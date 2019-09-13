using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TugOfWarPlayer : MonoBehaviour
{
	//config params
	[SerializeField] float tugStrength;

	//state

	//cached component reference
	GameObject rope;

    void Start()
    {
        rope = FindObjectOfType<Rope>().gameObject;
    }

    void Update()
    {
        CheckForInput();
    }

    void CheckForInput()
    {
    	if(Input.GetButtonDown("Fire1"))
    	{
    		rope.SendMessage("Tug", tugStrength*-1);
    	}
    }
}
