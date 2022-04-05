
        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;
        using UnityEngine.UI;

public class MirrorPuzzleScript : MonoBehaviour, DragPuzzle
    {

        public GameObject PuzzlePanel;
        public List<Sprite> SpriteSources;
        public GameObject ParentPanel;
        public Text PercentText;

        public int SpreadAmount = 400;
        int PercentCompleted;
        int PercentAmount;
        int AmountCompleted;
        int TotalTrash;

        Color32 PercentColor;
        Sprite LastSprite;
        public static bool IsPlaying = false;


        public void StartPuzzle(int difficulty)
        {
            IsPlaying = true;
            Cursor.visible = true;

            PercentCompleted = Random.Range(3, 24);

            PercentAmount = (100 - PercentCompleted) / difficulty;
            TotalTrash = difficulty;

            CreateImages(difficulty);
            PuzzlePanel.SetActive(true);
        }

        void CreateImages(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject NewObj = new GameObject();
                NewObj.name = "Trash";

                Image NewImage = NewObj.AddComponent<Image>();
                NewImage.sprite = SpriteSources[Random.Range(0, SpriteSources.Count)];

                NewObj.AddComponent<BoxCollider2D>();
                NewObj.AddComponent<DragAndDrop>().Init(this);

                NewObj.GetComponent<RectTransform>().SetParent(ParentPanel.transform);
                NewObj.transform.position = ParentPanel.transform.position;
                NewObj.transform.Translate(new Vector3(Random.Range(-SpreadAmount, SpreadAmount), Random.Range(-SpreadAmount, SpreadAmount), 0));
                NewObj.SetActive(true);
            }
        }      


        public void UpdateProgress()
        {
            PercentCompleted += PercentAmount;

            AmountCompleted += 1;
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
            if (currentObject.toOrginal)
            {
                transform.localPosition = currentObject.orginalPosition;
            }
            if (transform.position.x > (currentObject.transform.position.x + 200) || transform.position.x < (currentObject.transform.position.x - 200) || transform.position.y < (currentObject.transform.position.y - 200) || transform.position.y > (currentObject.transform.position.y + 200))
            {
                UpdateProgress();
                Destroy(gameObject);
            }
        }


    }

