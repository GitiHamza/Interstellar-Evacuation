using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using System.Collections.Generic;
using Unity.Cinemachine;

public class PlayerInteractions : MonoBehaviour
{

    public List<Interactable> interactables = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactable interactable = collision.GetComponent<Interactable>();

        if (interactable != null)
        {
            // Optional: nur einmal aufnehmen
            if (interactables.Contains(interactable)) return;

            interactables.Add(interactable);
            interactable.OnSelected?.Invoke();

            // 🔥 AUTOMATISCHES AUFNEHMEN
            interactable.Interact();

            // Falls das Objekt nach Interact zerstört wird
            interactables.Remove(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Interactable interactable = collision.GetComponent<Interactable>();

        if (interactable != null)
        {
            interactable.OnDeselected?.Invoke();
            interactables.Remove(interactable);
        }
    }
}

