using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
	//config params

	//state

	//cached component reference
    TugOfWarGameStatus gameStatus;
	Rigidbody2D rb;

    void Start()
    {
        gameStatus = FindObjectOfType<TugOfWarGameStatus>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Tug(float direction)
    {
        if(!gameStatus.gameOver)
        {
            rb.velocity += new Vector2(direction,0);
        }
    }
}
