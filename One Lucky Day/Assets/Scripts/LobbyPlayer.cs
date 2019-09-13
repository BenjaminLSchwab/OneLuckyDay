using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayer : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    
    Rigidbody2D rb;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 force = new Vector3(Input.GetAxis("Horizontal"), 0);
        rb.velocity = force * speed;
        if (Input.GetButtonDown("Fire1"))
        {
            gameManager.LoadGame();
        }
    }



}
