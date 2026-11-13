using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject interactButton; 

    private Interface currentInteractable;

    private void Start()
    {
        if (interactButton != null)
        {
            interactButton.SetActive(false);
        }
    }

    public void OnInteractButtonPressed()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
            
            if (interactButton != null) interactButton.SetActive(false);
            currentInteractable = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Interface interactable = other.GetComponent<Interface>();
        if (interactable != null)
        {
            currentInteractable = interactable;
            if (interactButton != null) interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interface interactable = other.GetComponent<Interface>();
        if (interactable != null && interactable == currentInteractable)
        {
            currentInteractable = null;
            if (interactButton != null) interactButton.SetActive(false);
        }
    }
}