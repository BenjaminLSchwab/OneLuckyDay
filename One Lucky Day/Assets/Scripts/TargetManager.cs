using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] GameObject path;
    [SerializeField] List<GameObject> targets;
    [SerializeField] float targetSpeed = 1f;
    [SerializeField] float targetSpacing = 1f;
    List<GameObject> targetPool;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        targetPool = new List<GameObject>();
        gameManager = FindObjectOfType<GameManager>();
        Begin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Begin()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        for (int i = 0; i < targets.Count; i++)
        {

            var target = Instantiate(targets[i], transform.position, Quaternion.identity);
            targetPool.Add(target);
            target.SendMessage("SetSpeed", targetSpeed);
            target.SendMessage("SetPath", path);
            target.SetActive(true);
            yield return new WaitForSeconds(targetSpacing);
        }

    }

    public void CheckForGameOver()
    {
        foreach (var target in targetPool)
        {
            if (target.activeInHierarchy)
            {
                return;
            }
        }
        gameManager.LoadLobby();
    }
}
