using UnityEngine;
using UnityEngine.Events;
public class Interactable : MonoBehaviour
{
    public UnityEvent OnInteract;
    public UnityEvent OnSelected;
    public UnityEvent OnDeselected;
    

    public bool destroyAfterUse;
    private bool _alreadyInteracted;

    public void Interact()
    {
        OnInteract?.Invoke();
        _alreadyInteracted = true;
        
        if(destroyAfterUse) Destroy(gameObject);
    }
}
