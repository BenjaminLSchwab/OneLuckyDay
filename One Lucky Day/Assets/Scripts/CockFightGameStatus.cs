using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CockFightGameStatus : MonoBehaviour
{
	//config params

	//state

	//cached component reference
	BetManager betManager;
	Canvas canvas;
	GameObject fighter1;
	GameObject fighter2;
	GameObject healthBar1;
	GameObject healthBarBackground1;
	GameObject healthBar2;
	GameObject healthBarBackground2;

    void Start()
    {
    	Invoke("FindFighters", 0.1f);
        betManager = FindObjectOfType<BetManager>();
        canvas = FindObjectOfType<Canvas>();
        healthBar1 = canvas.transform.Find("Fighter1Health").transform.Find("Bar").gameObject;
        healthBarBackground1 = canvas.transform.Find("Fighter1Health").transform.Find("Background").gameObject;
        healthBar2 = canvas.transform.Find("Fighter2Health").transform.Find("Bar").gameObject;
        healthBarBackground2 = canvas.transform.Find("Fighter2Health").transform.Find("Background").gameObject;
    }

    void FindFighters()
    {
    	fighter1 = betManager.fighter1;
        fighter2 = betManager.fighter2;
    }

    void Update()
    {
        LoadUI();
    }

    void LoadUI()
    {
    	if(fighter1 != null && fighter2 != null)
    	{
    		canvas.transform.Find("Fighter1Health").GetComponent<Image>().sprite = fighter1.GetComponent<SpriteRenderer>().sprite;
    		canvas.transform.Find("Fighter2Health").GetComponent<Image>().sprite = fighter2.GetComponent<SpriteRenderer>().sprite;
	    	float barWidth = ((float)fighter1.GetComponent<CockFightFighter>().health/fighter1.GetComponent<CockFightFighter>().maxHealth)*healthBarBackground1.GetComponent<RectTransform>().localScale.x;
	    	healthBar1.GetComponent<RectTransform>().localScale = new Vector2(barWidth,healthBar1.GetComponent<RectTransform>().localScale.y);	
	    	barWidth = healthBar1.GetComponent<RectTransform>().rect.width * healthBar1.GetComponent<RectTransform>().localScale.x;
	    	float sizeDifference = healthBarBackground1.GetComponent<RectTransform>().rect.width - barWidth;
    		healthBar1.GetComponent<RectTransform>().localPosition = healthBarBackground1.GetComponent<RectTransform>().localPosition - new Vector3(sizeDifference/2,0,0);
    		barWidth = ((float)fighter2.GetComponent<CockFightFighter>().health/fighter2.GetComponent<CockFightFighter>().maxHealth)*healthBarBackground2.GetComponent<RectTransform>().localScale.x;
	    	healthBar2.GetComponent<RectTransform>().localScale = new Vector2(barWidth,healthBar2.GetComponent<RectTransform>().localScale.y);	
	    	barWidth = healthBar2.GetComponent<RectTransform>().rect.width * healthBar2.GetComponent<RectTransform>().localScale.x;
	    	sizeDifference = healthBarBackground2.GetComponent<RectTransform>().rect.width - barWidth;
    		healthBar2.GetComponent<RectTransform>().localPosition = healthBarBackground2.GetComponent<RectTransform>().localPosition - new Vector3(sizeDifference/2,0,0);
    	}
    }
}
