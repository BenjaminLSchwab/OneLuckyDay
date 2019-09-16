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
    [SerializeField] float projectileRotateSpeed = 0.2f;
    [SerializeField] float projectileChargeSpeed = 1f;
    [SerializeField] float projectileMaxCharge = 200f;
    [SerializeField] GameObject weaponTip;
    [SerializeField] GameObject firingDirection;

    enum InputMode {Aim, Charge};
    InputMode inputMode = InputMode.Aim;
    bool aimingUp = true;
    bool chargingUp = true;
    float projectileCharge = 0;
    List<GameObject> PlasmaPool;
    
    // Start is called before the first frame update
    void Start()
    {
        PlasmaPool = new List<GameObject>();
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
                if (chargingUp)
                {
                projectileCharge += Time.deltaTime * projectileChargeSpeed;
                    if (projectileCharge > projectileMaxCharge)
                    {
                        chargingUp = false;
                    }
                }
                else
                {
                    projectileCharge -= Time.deltaTime * projectileChargeSpeed;
                    if (projectileCharge < 0)
                    {
                        chargingUp = true;
                    }
                }
                if (Input.GetButtonUp("Fire1"))
                {
                    var proj = Utilities.PullFromPool(PlasmaPool);
                    if (proj == null)
                    {
                        proj = Instantiate(projectile);
                        PlasmaPool.Add(proj);
                    }
                    proj.SetActive(true);
                    proj.transform.position = weaponTip.transform.position;
                    proj.transform.rotation = armPivot.transform.rotation;
                    var rb = proj.GetComponent<Rigidbody2D>();
                    rb.AddForce((firingDirection.transform.position - weaponTip.transform.position) * projectileCharge);
                    rb.AddTorque(-projectileRotateSpeed);
                    inputMode = InputMode.Aim;
                    projectileCharge = 0f;
                }
                break;
        }
    }

    public float GetCharge()
    {
        return Mathf.InverseLerp(0,projectileMaxCharge, projectileCharge);
    }
}
