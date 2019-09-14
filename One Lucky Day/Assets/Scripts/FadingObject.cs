using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingObject : MonoBehaviour
{
	//config params
	[SerializeField] float duration;

    void Start()
    {
        Destroy(gameObject, duration);
    }

}
