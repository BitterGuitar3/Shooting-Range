using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : XRGrabInteractable
{
    [SerializeField]
    private GameObject handgunModel;

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
        handgunModel.GetComponent<SimpleShoot>().PullTheTrigger();
    }
}
