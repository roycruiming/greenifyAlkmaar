
        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;
        using UnityEngine.UI;

public class MirrorPuzzleScript : MonoBehaviour, DragPuzzle
{

    public GameObject PuzzlePanel;
    public List<GameObject> places;
    public GameObject ParentPanel;

    public int SpreadAmount = 400;

    public static bool IsPlaying = false;

    public List<GameObject> mirrors;

    public GameObject parent;


    public void StartPuzzle()
    {
        IsPlaying = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Debug.Log("what");

        DragMirrors(mirrors);

        PuzzlePanel.SetActive(true);
    }


    public void DragMirrors(List<GameObject> mirrorsToDrag)
    {
        foreach(GameObject mirror in mirrorsToDrag)
        {
            mirror.AddComponent<DragAndDrop>().Init(this);
        }
    }

    public void UpdateProgress()
    {


        CheckCompletion();
    }

    void CheckCompletion()
    {


        StartCoroutine(StopPuzzle());

    }

    IEnumerator StopPuzzle()
    {
        yield return new WaitForSeconds(5);

        IsPlaying = false;
        Cursor.visible = false;

        PuzzlePanel.SetActive(false);
    }

    public void EndDragAction(DragAndDrop currentObject)
    {
        currentObject.toOrginal = true;

        foreach (GameObject placement in places)
        {
            if (placement.GetComponent<BoxCollider2D>().bounds.Intersects(currentObject.GetComponent<BoxCollider2D>().bounds))
            {
                if (!placement.GetComponent<MirrorPlace>().IsFull)
                {
                    currentObject.transform.position = placement.transform.position;
                    currentObject.toOrginal = false;
                    placement.GetComponent<MirrorPlace>().IsFull = true;
                }
            }             
        }
       
        if (currentObject.toOrginal == true) {
            currentObject.transform.localPosition = currentObject.orginalPosition;
        }
    }



}

