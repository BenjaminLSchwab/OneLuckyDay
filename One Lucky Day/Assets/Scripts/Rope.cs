using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
	//config params
	[SerializeField] float distanceToWin;

	//state

	//cached component reference
	Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(transform.position.x <= distanceToWin*-1)
        {
        	Debug.Log("you win");
        }
        else if(transform.position.x >= distanceToWin)
        {
        	Debug.Log("you lose");
        }
    }

    public void Tug(float direction)
    {
    	rb.velocity += new Vector2(direction,0);
    }
}
