
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

    public void StartPuzzle(int difficulty)
    {
        IsPlaying = true;
        Cursor.visible = true;
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
            if (placement.GetComponent<Collider>().bounds.Intersects(currentObject.GetComponent<Collider>().bounds))
            {
                currentObject.transform.position = placement.transform.position;
                currentObject.toOrginal = false;
            }
        }
       
        if (currentObject.toOrginal == true) {
            transform.localPosition = currentObject.orginalPosition;
        }
    }



}

