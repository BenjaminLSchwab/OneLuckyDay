using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifferenceDisplay : MonoBehaviour
{
    [SerializeField] float displayTime = 2f;
    public Text displayText;
    float displayTimer = 0;
    bool displayActive = false;
    // Start is called before the first frame update
    void Start()
    {
        displayTimer = displayTime;
    }


    // Update is called once per frame
    void Update()
    {
        if (displayActive)
        {
            displayTimer -= Time.deltaTime;
            if (displayTimer < 0)
            {
                displayText.gameObject.SetActive(false);
                displayActive = false;
                displayTimer = displayTime;
            }
        }

    }

    public void SetDisplayActive()
    {
        displayActive = true;
        displayText.gameObject.SetActive(true);
    }

}
