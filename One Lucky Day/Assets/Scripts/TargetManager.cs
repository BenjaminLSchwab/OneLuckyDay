using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] GameObject path;
    [SerializeField] List<GameObject> targets;
    [SerializeField] float targetSpeed = 1f;
    [SerializeField] float targetSpacing = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Begin()
    {

    }

    IEnumerator SpawnLoop()
    {
        for (int i = 0; i < targets.Count; i++)
        {

            var target = Instantiate(targets[i], transform.position, Quaternion.identity);
            target.SendMessage("SetSpeed", targetSpeed);
            target.SendMessage("SetPath", path);
            yield return new WaitForSeconds(targetSpacing);
        }

    }
}
