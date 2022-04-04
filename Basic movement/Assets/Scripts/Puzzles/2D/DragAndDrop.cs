using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler {
    private Vector2 lastMousePosition;
    private float x, y, z;

    public bool blockX, blockY, blockZ;

    public bool toOrginal;

    private Vector3 orginalPosition;

    CleanSolarPanelPuzzle PuzzleScript;

    private void Start() {
        orginalPosition = transform.localPosition;
        PuzzleScript = GameObject.FindGameObjectWithTag("CleanPuzzle").GetComponent<CleanSolarPanelPuzzle>();

    }

    //als de speler er op klikt
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
        if (toOrginal){
            transform.localPosition = orginalPosition;
        }
        if(transform.position.x > (PuzzleScript.transform.position.x + 200) || transform.position.x < (PuzzleScript.transform.position.x - 200) || transform.position.y < (PuzzleScript.transform.position.y - 200) || transform.position.y > (PuzzleScript.transform.position.y + 200))
        {
          PuzzleScript.UpdateProgress();
          Destroy(gameObject);
        }
    }
}
