using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketInteractorTag : XRSocketInteractor
{
    public string targetTag;
    [Tooltip("This is the gun the socket is attatched to")][SerializeField] private GameObject gun;
    private GameObject currMag;

    public override bool CanHover(XRBaseInteractable interactable)
    {
        return base.CanHover(interactable) && MatchUsingTag(interactable);
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        return base.CanSelect(interactable) && MatchUsingTag(interactable);
    }

    private bool MatchUsingTag(XRBaseInteractable interactable)
    {
        return interactable.CompareTag(targetTag);
    }

    public GameObject getMag()
    {
        return currMag;
    }

    protected override void OnSelectEntered(XRBaseInteractable interactable)
    {
        base.OnSelectEntered(interactable);
        currMag = interactable.gameObject;
        currMag.GetComponent<Magazine>().setGun(gun);
        currMag.GetComponent<Magazine>().rackBullet();
    }

    protected override void OnSelectExited(XRBaseInteractable interactable)
    {
        base.OnSelectExited(interactable);
        StartCoroutine(Delay(0.25f));
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

}

    
