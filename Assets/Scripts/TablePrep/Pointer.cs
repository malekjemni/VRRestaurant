using UnityEngine;


public class Pointer : MonoBehaviour
{
    public ResolveInteractor resolver;
    private GameObject interactor;
    public void GetInteractor(GameObject _interactor)
    {
        interactor = _interactor;
        resolver.interactor = interactor;
    }
}
