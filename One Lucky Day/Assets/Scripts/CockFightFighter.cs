﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockFightFighter : MonoBehaviour
{
	//config params
	[SerializeField] float decisionDelay;
	[SerializeField] float moveDelay;
	[SerializeField] float attackDelay;
	[SerializeField] float attackRange;
	[SerializeField] float damage;
	[SerializeField] float moveSpeed;
	[SerializeField] float stoppingDistance;
	[SerializeField] int maxHealth;

	//state
	float decisionTimer;
	float moveTimer;
	float lastAttackTime;
	int health;
	string mode;
	Vector2 moveOffset;

	//cached component reference
	Rigidbody2D rb;
	[SerializeField] GameObject opponent;

    void Start()
    {
    	rb = GetComponent<Rigidbody2D>();
    	health = maxHealth;
    }

    void Update()
    {
    	decisionTimer -= Time.deltaTime;
    	if(decisionTimer <= 0)
    	{
    		decisionTimer = decisionDelay;
    		var rand = Random.Range(1,100);
    		if(rand <= 65)
    		{
    			SetMode("charge");
    		}
    		else
    		{
    			SetMode("run");
    		}
    	}
    	if(mode == "charge" || mode == "run")
    	{
			moveTimer -= Time.deltaTime;
	        if(moveTimer <= 0)
	        {
	        	moveTimer = moveDelay;
	        	Vector2 directionToOpponent = (opponent.transform.position - transform.position).normalized;
				if(mode == "charge")
				{
					float distanceToOpponent = (opponent.transform.position - transform.position).magnitude;
					if(distanceToOpponent > stoppingDistance)
					{
						rb.AddForce((directionToOpponent*moveSpeed)+moveOffset);	
					}	
					if(distanceToOpponent <= attackRange)
					{
						if(Time.realtimeSinceStartup - lastAttackTime >= attackDelay)
						{
							lastAttackTime = Time.realtimeSinceStartup;
							var rand = Random.Range(1,3);
							if(rand == 1)
							{
								opponent.SendMessage("Damage", damage);	
							}
						}
					}
				}
	        	else if(mode == "run")
	        	{
	        		rb.AddForce((directionToOpponent*moveSpeed*-1)+moveOffset);	
	        	}
	        }
    	}
    }

    void SetMode(string newMode)
    {
    	mode = newMode;
    	Vector2 directionToOpponent = (opponent.transform.position - transform.position).normalized;
		var theta = 90 *Mathf.Deg2Rad;
		var x = directionToOpponent.x;
		var y = directionToOpponent.y;
		var cs = Mathf.Cos(theta);
		var sn = Mathf.Sin(theta);
		var rx = x * cs - y * sn; 
		var ry = x * sn + y * cs;
		Vector2 perpendicularDirection = new Vector2(rx,ry);
		float rand = Random.Range(-100,100);
		rand = rand/100;
		moveOffset = perpendicularDirection*moveSpeed*rand;
    }

    void Damage(int damageTaken)
    {
    	health -= damageTaken;
    	Debug.Log(gameObject.name + " Health:" + health);
    	if(health <= 0)
    	{
    		Debug.Log(opponent.name + " wins");
    	}
    }
}
