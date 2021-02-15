using UnityEngine;
using UnityEngine.EventSystems;

public class MoveUI : MonoBehaviour
{
    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    private void OnDragDelegate(PointerEventData data)
    {
        Debug.Log("Dragging.");
        transform.parent.transform.position = Input.mousePosition;
        RectTransform rt = (RectTransform)transform.parent.transform;
        transform.parent.transform.position -= new Vector3(0, rt.rect.height/2, 0);
    }
}
