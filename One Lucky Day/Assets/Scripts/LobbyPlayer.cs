using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayer : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    
    Rigidbody2D rb;
    Animator animator;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
        Vector3 force = new Vector3(Input.GetAxis("Horizontal"), 0);
        rb.velocity = force * speed;
        if (Input.GetButtonDown("Fire1"))
        {
            gameManager.LoadGame();
        }
    }

    void Animate()
    {
        if(Input.GetAxis("Horizontal") > 0)
        {
            animator.SetBool("Walking", true);
            transform.localScale = new Vector3(1,1,1);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            animator.SetBool("Walking", true);
            transform.localScale = new Vector3(-1,1,1);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

}
