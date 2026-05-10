using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactRadius = 1.0f;
    public LayerMask interactLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, interactRadius, interactLayer);

            if (hit != null)
            {
                if (hit.CompareTag("Computer"))
                {
                    UIManager.instance.OpenTerminal();
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}