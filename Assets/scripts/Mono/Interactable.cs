using UnityEngine;

namespace XEntity
{
    //우리가 만질일 없을 듯함

    //The base class for all objects that the interactor can interact with.
    public abstract class Interactable : MonoBehaviour
    {
        //This method is called when the interactor attempts to interact with the object.
        //The passed in Interactor is the interactor that is attempting to interact.
        public virtual void OnInteract(Interactor interactor)
        {
            Debug.Log("Interacting...");
        }
    }
}
