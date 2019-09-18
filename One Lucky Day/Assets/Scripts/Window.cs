using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField] int startingHP = 4;
    [SerializeField] List<Sprite> dmgSprites;
    [SerializeField] GameObject debris;
    [SerializeField] float succStrength = 1f;
    [SerializeField] Transform succDirection;
    [SerializeField] float gameOverDelay = 1.5f;
    [SerializeField] AudioClip crackSound;
    [SerializeField] List<AudioClip> breakSounds;
    [SerializeField] float volume = 0.5f;
    int currentHP;
    SpriteRenderer spRenderer;
    bool gameOver = false;
    float gameOverTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        currentHP = startingHP;
        gameOverTimer = gameOverDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            gameOverTimer -= Time.deltaTime;
            if (gameOverTimer < 0)
            {
                FindObjectOfType<GameManager>().LoadLobby();
                gameOver = false;
            }
        }

    }

    public void Hit()
    {
        AudioSource.PlayClipAtPoint(crackSound, transform.position, volume);
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
        foreach (var sound in breakSounds)
        {
            AudioSource.PlayClipAtPoint(sound, transform.position, volume);
        }
        foreach (Transform item in debris.transform)
        {
            item.gameObject.SetActive(true);
            var rb = item.GetComponent<Rigidbody2D>();
            if (rb == null) continue;
            rb.velocity = (succDirection.position - item.transform.position).normalized * succStrength;
            gameOver = true;
        }
        
    }
}
