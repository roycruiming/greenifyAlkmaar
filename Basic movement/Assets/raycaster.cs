using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class raycaster : MonoBehaviour
{
    // Start is called before the first frame update

    public int rayLength;
    public LayerMask layerMask;
    public Text textUI;
    public GameObject objCon;

    private InventoryController InventoryController; 



    void Start()
    {
        if (textUI != null) {
            textUI.text = "";
            textUI.gameObject.SetActive(false);
        }

        objCon = GameObject.FindGameObjectWithTag("GameController");
    }


    private void Awake()
    {
        InventoryController = new InventoryController(new Inventory()); 
    }


    void Update()
    {
        
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, rayLength, layerMask, QueryTriggerInteraction.Collide))
        {
            OnScreenDescription description
                = hitInfo.collider.gameObject.GetComponent<OnScreenDescription>();

            if (textUI != null && description != null) {
                textUI.gameObject.SetActive(true);
                textUI.text = description.textToDisplay;
            }

            if (Input.GetKeyDown(KeyCode.F)) {

                Item item = hitInfo.collider.gameObject.GetComponent<Item>();
                if (item != null) {
                    InventoryController.StoreItemAndPlacePreviouslyStoredItemInWorld(item, gameObject.transform);
                    objCon.GetComponent<ObjectivesController>().DeleteItemInListSolar(hitInfo.collider.gameObject.GetComponent<Item>());
                }

                Chest chest = hitInfo.collider.gameObject.GetComponent<Chest>();
                if (chest != null) {
                    Item key = InventoryController.GetItem();
                    if (!chest.OpenChest(key)) return;
                    InventoryController.ClearInventory();
                }

                DoorsOpener doors = hitInfo.collider.gameObject.GetComponent<DoorsOpener>();
                if (doors != null)
                {
                    Item key = InventoryController.GetItem();
                    if (!doors.OpenDoors(key)) return;
                    InventoryController.ClearInventory();
                }

                PuzzleController puzzleController = hitInfo.collider.gameObject.GetComponent<PuzzleController>();
                if (puzzleController != null && hitInfo.collider.transform.GetChild(0).gameObject.activeInHierarchy) {
                    hitInfo.collider.gameObject.GetComponent<PuzzleController>().StartAPuzzle();
                }

                SolarSpot solarSpot = hitInfo.collider.GetComponent<SolarSpot>();
                Item item3 = InventoryController.GetItem();
                if (hitInfo.collider.gameObject.GetComponent<SolarSpot>() != null && item3 != null) {
                    solarSpot.DoShit(item3);
                    InventoryController.ClearInventory();                    
                }

                else if (hitInfo.collider.gameObject.CompareTag("InformationHelper"))
                {
                    //object is gamehelper so showcase this message in the HUD
                    //get the information text from the object and send it to the controller
                    if (hitInfo.collider.gameObject.GetComponent<InformationHelper>() != null && GameObject.FindWithTag("HUDCanvas") != null)
                    {
                        //find the hudcontroller object and call the ShowcaseMessage function with the informationHelper message
                        InformationHelper senderInfo = hitInfo.collider.gameObject.GetComponent<InformationHelper>();
                        if (senderInfo.keyTextIsSentence == false) GameObject.FindWithTag("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(senderInfo.GetTranslatedText(), senderInfo);
                        else GameObject.FindWithTag("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(senderInfo.GetTranslatedText(), senderInfo, senderInfo.GetMultipleTranslatedSentences());
                    }
                    
                }
            }
        }
        else
        {
            if (textUI != null)
            {
                textUI.gameObject.SetActive(false);

            }

            }
        }

    }
