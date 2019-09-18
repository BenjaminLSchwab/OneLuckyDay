using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
	//config params
	[SerializeField] float duration;
	[SerializeField] float coolDown;
	[SerializeField] float damage;

	//state
	bool active;
	bool coolingDown;

	//cached component reference
	[SerializeField] Sprite inactiveSprite;
	[SerializeField] Sprite activeSprite;
	[SerializeField] AudioClip sound;

    void Start()
    {
        
    }

    void OnTriggerStay2D(Collider2D collider)
    {
    	if(active)
    	{
    		if(collider.gameObject.GetComponent<CockFightFighter>() != null)
    		{
    			collider.gameObject.SendMessage("Damage", damage);
    			active = false;
    		}
    	}
    }

    public void Activate()
    {
    	if(!coolingDown)
    	{
    		GetComponent<AudioSource>().clip = sound;
    		GetComponent<AudioSource>().Play();
	    	StartCoroutine("Trigger");
	    	coolingDown = true;
	    	Invoke("CoolDown", coolDown);	
    	}
    }

    void CoolDown()
    {
    	coolingDown = false;
    }

    IEnumerator Trigger()
    {
    	GetComponent<SpriteRenderer>().sprite = activeSprite;
    	active = true;
    	yield return new WaitForSeconds(duration);
    	GetComponent<SpriteRenderer>().sprite = inactiveSprite;
    	active = false;
    }
}
