using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartsPlayer : MonoBehaviour
{
    [SerializeField] GameObject armPivot;
    [SerializeField] GameObject armSprite;
    [SerializeField] GameObject projectile;
    [SerializeField] float armMaxAngle = 45f;
    [SerializeField] float armMinAngle = 340f;
    [SerializeField] float armRotateSpeed = 1f;
    [SerializeField] float projectileChargeSpeed = 1f;
    [SerializeField] Vector3 projectileForceOffset;
    [SerializeField] Vector3 projectilePositionOffset;

    enum InputMode {Aim, Charge};
    InputMode inputMode = InputMode.Aim;
    bool aimingUp = true;
    float projectileCharge = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            inputMode = InputMode.Charge;
        }
        switch (inputMode)
        {
            case InputMode.Aim:
                if (aimingUp)
                {
                    armPivot.transform.Rotate(0, 0, Time.deltaTime * armRotateSpeed);
                    if (armPivot.transform.rotation.eulerAngles.z > armMaxAngle && armPivot.transform.rotation.eulerAngles.z < 90)
                    {
                        aimingUp = false;
                    }
                }
                else
                {
                    armPivot.transform.Rotate(0, 0, Time.deltaTime * armRotateSpeed * -1);
                    if (armPivot.transform.rotation.eulerAngles.z > 250 && armPivot.transform.rotation.eulerAngles.z < armMinAngle)
                    {
                        aimingUp = true;
                    }
                }
                break;

            case InputMode.Charge:
                projectileCharge += Time.deltaTime * projectileChargeSpeed;
                if (Input.GetButtonUp("Fire1"))
                {
                    var proj = Instantiate(projectile, armSprite.transform.position + projectilePositionOffset, armPivot.transform.rotation);
                    var rb = proj.GetComponent<Rigidbody2D>();
                    rb.AddForce(((armSprite.transform.position + projectileForceOffset ) - armPivot.transform.position) * projectileCharge);
     
                    inputMode = InputMode.Aim;
                    projectileCharge = 0f;
                }
                break;
        }
    }
}
