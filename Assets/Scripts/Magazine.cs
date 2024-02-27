using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Magazine : XRGrabInteractable
{
    private int ammoCount = 8;
    private GameObject gun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setGun(GameObject target)
    {
        gun = target;
    }

    public void rackBullet()
    {
        if (ammoCount > 0 && !gun.GetComponent<Gun>().bulletInChamber) 
        {
            gun.GetComponent<Gun>().bulletInChamber = true;
            ammoCount--;
        }
    }

    public void enableCollider()
    {
        this.GetComponent<Collider>().enabled = true;
    }

    public void disableCollider()
    {
        this.GetComponent<Collider>().enabled = false;
    }

}
