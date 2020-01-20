using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool fireEnabled = false;
    public int shots = 0;
    public GameManagerLogic manager = null;
    public Camera playerCamera = null;
    public GameObject impactEffect = null;

    private void Update()
    {
        if (fireEnabled)
        {
            Fire();
        }
    }

    void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (shots < 1)
            {
                manager.NoMoreShots();
                return;
            }

            RaycastHit hit;

            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.transform.tag.ToString());
                if (hit.transform.tag == "Answer")
                {
                    manager.total--;
                }
            }

            GameObject effect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(effect, 0.5f);
            shots--;
        }
    }
}
