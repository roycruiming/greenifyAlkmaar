using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private Vector2 lastMousePosition;
    private float x, y, z;

    public bool blockX, blockY, blockZ;

    public bool toOrginal;

    public Vector3 orginalPosition;

    DragPuzzle PuzzleScript;

    private void Start() {
        orginalPosition = transform.localPosition;
    }

    public DragAndDrop Init(DragPuzzle connectedPuzzle)
    {
        PuzzleScript = connectedPuzzle;
        return this;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        lastMousePosition = eventData.position;
    }

    //tijdens het slepen
    public void OnDrag(PointerEventData eventData) {
        Vector2 curremtMousePosition = eventData.position;
        Vector2 diff = curremtMousePosition - lastMousePosition;

        RectTransform rectTransform = GetComponent<RectTransform>();

        x = y = z = 0;

        if (!blockX)
            x = diff.x;

        if (!blockY)
            y = diff.y;

        rectTransform.position = rectTransform.position + new Vector3(x, y, z);

        lastMousePosition = curremtMousePosition;
    }

    //wanneer de speler het object lost laat
    public void OnEndDrag(PointerEventData eventData) {
        PuzzleScript.EndDragAction(this);
    }
}
