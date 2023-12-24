using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSwap : MonoBehaviour
{
    public Mesh mesh;  

    public void UnfoldCloth(GameObject target)
    {
        target.transform.localScale = Vector3.one;
        target.GetComponent<MeshFilter>().mesh = mesh;
        target.GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
