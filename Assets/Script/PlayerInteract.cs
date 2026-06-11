using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactRadius = 1.5f;
    public LayerMask interactLayer;

    [Header("UI")]
    public GameObject interactPromptUI;

    private Collider2D currentInteractable;

    void Update()
    {
        CheckInteractable();

        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable.CompareTag("Computer"))
            {
                if (UIManager.instance != null)
                {
                    UIManager.instance.OpenTerminal();

                    if (interactPromptUI != null)
                        interactPromptUI.SetActive(false);
                }
            }
        }
    }

    void CheckInteractable()
    {
        currentInteractable = Physics2D.OverlapCircle(transform.position, interactRadius, interactLayer);

        if (interactPromptUI != null)
        {
            bool shouldShowPrompt = currentInteractable != null &&
                                    currentInteractable.CompareTag("Computer") &&
                                    UIManager.instance != null &&
                                    !UIManager.instance.terminalUI.activeSelf;

            interactPromptUI.SetActive(shouldShowPrompt);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}