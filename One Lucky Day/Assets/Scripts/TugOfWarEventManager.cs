using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TugOfWarEventManager : MonoBehaviour
{
	//config params
	[SerializeField] float eventDelay;
	[SerializeField] float eventDuration;
	[SerializeField] float advantage;

	//state
	float timer;
	public bool eventActive;

	//cached component reference
    TugOfWarGameStatus gameStatus;
	GameObject rope;
	[SerializeField] GameObject sprite;

    void Start()
    {
    	sprite.SetActive(false);
        rope = FindObjectOfType<Rope>().gameObject;
        gameStatus = FindObjectOfType<TugOfWarGameStatus>();
        timer = eventDelay;
    }

    void Update()
    {
        if(!gameStatus.gameOver)
        {
            if(!eventActive)
            {
                timer -= Time.deltaTime;    
            }
            if(timer <= 0)
            {
                float rand = Random.Range(5,15);
                rand = rand/10;
                timer = eventDelay*rand;
                Debug.Log(timer);
                rand = Random.Range(1,2);
                if(rand == 1)
                {
                    StartCoroutine(Event());
                }
            }    
        }
    }

    IEnumerator Event()
    {
    	sprite.SetActive(true);
    	eventActive = true;
    	yield return new WaitForSeconds(eventDuration);
    	if(eventActive)
    	{
    		sprite.SetActive(false);	
    		eventActive = false;
    		rope.SendMessage("Tug", advantage);
    	}
    }

    public void Respond()
    {
    	sprite.SetActive(false);
    	eventActive = false;
    	rope.SendMessage("Tug", advantage*-1);
    }
}
