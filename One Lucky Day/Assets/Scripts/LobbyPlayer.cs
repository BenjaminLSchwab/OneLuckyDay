using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayer : MonoBehaviour
{
    private string selectedGame = "";
    Rigidbody2D rb;
    [SerializeField] float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 force = new Vector3(Input.GetAxis("Horizontal"), 0);
        rb.velocity = force * speed;
    }



}
