using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBar : MonoBehaviour
{
    [SerializeField] float maxYScale;
    [SerializeField] float minYScale;
    public float value;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1, Mathf.Lerp(minYScale, maxYScale, value), 1);
    }
}
