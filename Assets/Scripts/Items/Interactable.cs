using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;
    private bool isFocus = false;
    private Transform player;
    private bool done = false;
    private Inventory inventoryObjetivo;

    public virtual void interact(Inventory inventoryObjetivo)
    {
        Debug.Log("Interaccion " + transform.name);
    }
    private void Update()
    {
        if (isFocus && !done)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance<=radius)
            {
                interact(inventoryObjetivo);
            }
                done = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
    public void OnFocused(Transform playerTransform, Inventory inventory)
    {
        isFocus = true;
        player = playerTransform;
        done = false;
        inventoryObjetivo = inventory;
    }
}
