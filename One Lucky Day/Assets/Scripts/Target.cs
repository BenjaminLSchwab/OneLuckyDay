using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] int targetWorth = 10;
    GameObject path;
    public List<Transform> waypoints;
    float speed = 1f;
    int waypointIndex = 0;
    TargetManager targetManager;
    [SerializeField] AudioClip hitSound;
    [SerializeField] float hitSoundVolume = 1f;
    // Start is called before the first frame update
    void Start()
    {
        targetManager = FindObjectOfType<TargetManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (path)
        {
            var targetPosition = waypoints[waypointIndex].position;
            var movementThisFrame = Time.deltaTime * speed;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            if (transform.position == targetPosition)
            {
                if (waypointIndex == waypoints.Count - 1)
                {
                    waypointIndex = 0;
                }
                else
                {
                    waypointIndex++;
                }
            }


        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetPath(GameObject newPath)
    {
        path = newPath;
        waypoints = new List<Transform>();
        foreach (Transform point in path.transform)
        {
            waypoints.Add(point);
        }
    }

    public void Hit()
    {
        AudioSource.PlayClipAtPoint(hitSound, transform.position, hitSoundVolume);
        FindObjectOfType<GameManager>().AddToWinnings(targetWorth);
        gameObject.SetActive(false);
        targetManager.CheckForGameOver();
    }
}
