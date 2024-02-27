using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : XRGrabInteractable
{
    [SerializeField]
    private GameObject handgunModel;
    public bool bulletInChamber = false;

    [Header("Ammo Tracking")]
    [SerializeField] private XRSocketInteractorTag magSocket;
    public AudioSource source;
    public AudioClip fireSound;
    public AudioClip emptyMag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);
        if (bulletInChamber)
        {
            var magazine = magSocket.getMag();
            source.PlayOneShot(fireSound);
            handgunModel.GetComponent<SimpleShoot>().PullTheTrigger();
            bulletInChamber = false;
            magazine.gameObject.GetComponent<Magazine>().rackBullet();
        }
        else
        {
            source.PlayOneShot(emptyMag);
        }
    }
}
