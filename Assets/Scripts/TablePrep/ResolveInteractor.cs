using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResolveInteractor : MonoBehaviour
{
 
    public GameObject interactor;


    public void FoldItem()
    {
        if (interactor.GetComponent<XRGrabInteractable>().enabled)
        {
            interactor.GetComponent<MeshSwap>().refoldCloth();
        }
        
    }
    public void UnfoldItem()
    {
        interactor.GetComponent<MeshSwap>().UnfoldCloth();
    }

    public void RemoveGrabe()
    {
        interactor.GetComponent<XRGrabInteractable>().enabled = false;
    }
    public void ResetGrabe()
    {
        interactor.GetComponent<XRGrabInteractable>().enabled = true;
    }
}
