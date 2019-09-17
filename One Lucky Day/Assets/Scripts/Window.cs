using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField] int startingHP = 4;
    [SerializeField] List<Sprite> dmgSprites;
    [SerializeField] GameObject debris;
    [SerializeField] float succStrength = 1f;
    int currentHP;
    SpriteRenderer spRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        currentHP = startingHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        currentHP--;
        if (currentHP < 1)
        {
            spRenderer.sprite = dmgSprites[1];
            Succ();
        }
        else if(currentHP < (startingHP / 2))
        {
            spRenderer.sprite = dmgSprites[0];
        }
    }

    void Succ()
    {
        foreach (Transform item in debris.transform)
        {
            item.gameObject.SetActive(true);
            var rb = item.GetComponent<Rigidbody2D>();
            if (rb == null) continue;
            rb.velocity = (transform.position - item.transform.position).normalized * succStrength;
        }
    }
}
