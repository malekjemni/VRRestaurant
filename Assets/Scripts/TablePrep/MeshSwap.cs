using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MeshSwap : MonoBehaviour
{
    public GameObject Visual;  
       
    public void UnfoldCloth()
    {
        Visual.GetComponent<MeshRenderer>().enabled = false;
        Visual.GetComponent<BoxCollider>().enabled = true;
    }
    public void refoldCloth()
    {
        Visual.GetComponent<MeshRenderer>().enabled = true;
        Visual.GetComponent<BoxCollider>().enabled = false;
    }
 
}
