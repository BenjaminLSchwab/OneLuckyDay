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
    bool dead;

	//cached component reference
	GameObject rope;
    [SerializeField] Sprite deadSprite;

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
        	rope.SendMessage("Tug", tugStrength + Random.Range(0,1));
        }
    }

    public void Die()
    {
        if(!dead)
        {
            dead = true;
            GetComponent<SpriteRenderer>().sprite = deadSprite;
            transform.position += new Vector3(0,-1,0);   
        } 
    }
}
