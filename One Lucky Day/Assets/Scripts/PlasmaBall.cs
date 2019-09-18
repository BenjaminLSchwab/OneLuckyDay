using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBall : MonoBehaviour
{
    [SerializeField] AudioClip hitSound;
    [SerializeField] float hitSoundVolume = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(hitSound, transform.position, hitSoundVolume);
        collision.SendMessage("Hit");
        gameObject.SetActive(false);
    }
}
