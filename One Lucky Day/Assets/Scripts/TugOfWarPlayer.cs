using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TugOfWarPlayer : MonoBehaviour
{
	//config params
	[SerializeField] float tugStrength;

	//state
    bool dead;

	//cached component reference
	GameObject rope;
    TugOfWarEventManager eventManager;
    [SerializeField] Sprite deadSprite;

    void Start()
    {
        rope = FindObjectOfType<Rope>().gameObject;
        eventManager = FindObjectOfType<TugOfWarEventManager>();
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
        else if(Input.GetButtonDown("Fire2"))
        {
            if(eventManager.eventActive)
            {
                eventManager.SendMessage("Respond");
            }
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
