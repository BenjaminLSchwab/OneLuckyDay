using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChargeIndicator : MonoBehaviour
{
    [SerializeField] ChargeBar slider;
    DartsPlayer dartsPlayer;
    // Start is called before the first frame update
    void Start()
    {
        dartsPlayer = FindObjectOfType<DartsPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = dartsPlayer.GetCharge();
    }
}
