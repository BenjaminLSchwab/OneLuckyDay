using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlideshowObject : MonoBehaviour
{
    [SerializeField] float time = 5f;
    [SerializeField] GameObject nextObject;
    [SerializeField] bool loadLobbyWhenDone = false;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = time;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (nextObject)
            {
                nextObject.SetActive(true);
            }
            if (loadLobbyWhenDone)
            {
                SceneManager.LoadScene("Lobby");
            }
            gameObject.SetActive(false);
        }
    }
}
