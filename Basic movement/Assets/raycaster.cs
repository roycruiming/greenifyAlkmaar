using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro; 

public class raycaster : MonoBehaviour
{
    // Start is called before the first frame update

    public int rayLength;
    public LayerMask layerMask;
    private GameObject pressFContainer; 
    private TextMeshProUGUI textMeshPro; 
    public GameObject objCon;
    private InventoryController InventoryController;

    public bool seenTreeInformation = false; 



    void Start()
    {


        objCon = GameObject.FindGameObjectWithTag("GameController");
        if (objCon == null) objCon = GameObject.Find("HUDCanvas");

    }


    private void Awake()
    {
         

        pressFContainer = GameObject.Find("PressFContainer");
        textMeshPro = GameObject.Find("ScreenDisplayText").GetComponent<TextMeshProUGUI>();

        if(pressFContainer != null) 
        pressFContainer.SetActive(false); 

        InventoryController = new InventoryController(new Inventory());
    }


    void Update()
    {
        
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, rayLength, layerMask, QueryTriggerInteraction.Collide))
        {
            bool hasItem = hitInfo.collider.gameObject.GetComponent<Item>() != null;
            bool  hasInformationHelper = hitInfo.collider.gameObject.GetComponent<InformationHelper>() != null;
            //bool hasItem = hitInfo.collider.gameObject.GetComponent<Item>();



            //= hitInfo.collider.gameObject.GetComponent<OnScreenDescription>();

            if (textMeshPro != null && (hasInformationHelper || hasItem))
            {
                if (textMeshPro != null && pressFContainer != null) { 
                    textMeshPro.text = GlobalGameHandler.GetTextByDictionaryKey("press 'f' to interact");
                    pressFContainer.SetActive(true);
                }
            }
            else if (textMeshPro != null && pressFContainer != null)  pressFContainer.SetActive(false);
            

            if (Input.GetKeyDown(KeyCode.F)) {
               Item item = hitInfo.collider.gameObject.GetComponent<Item>();
                if (item != null) {
                    if (item.HudImage != null)
                    {
                        GameObject.FindWithTag("HUDCanvas").GetComponent<HUDController>().SetInventorySprite(item.HudImage);
                    }

                    InventoryController.StoreItemAndPlacePreviouslyStoredItemInWorld(item, gameObject.transform);

                    ObjectivesController objc = objCon.GetComponent<ObjectivesController>();
                    objc.DeleteItemInListSolar(hitInfo.collider.gameObject.GetComponent<Item>());
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

                Treespot treeSpot = hitInfo.collider.gameObject.GetComponent<Treespot>();
                if (treeSpot != null) {
                    Item tree = InventoryController.GetItem();
                    if (!treeSpot.PlantTree(tree)) return;
                    InventoryController.ClearInventory();
                }


                ChargerSpot chargerSpot = hitInfo.collider.gameObject.GetComponent<ChargerSpot>();
                if (chargerSpot != null)
                {
                    Item charger = InventoryController.GetItem();
                    if (!chargerSpot.InstallCharger(charger)) return;
                    InventoryController.ClearInventory();
                }



                PuzzleController puzzleController = hitInfo.collider.gameObject.GetComponent<PuzzleController>();
                print(hitInfo.collider.gameObject.name);
                if (puzzleController != null) {
                    hitInfo.collider.gameObject.GetComponent<PuzzleController>().StartAPuzzle();
                }

                SolarSpot solarSpot = hitInfo.collider.GetComponent<SolarSpot>();
                Item item3 = InventoryController.GetItem();
                if (hitInfo.collider.gameObject.GetComponent<SolarSpot>() != null && item3 != null &&  item3.tag == "SolarPanel") {
                    solarSpot.DoShit(item3);
                    InventoryController.ClearInventory();
                }

                 if (hitInfo.collider.gameObject.CompareTag("InformationHelper"))
                {

                    

              



                    //object is gamehelper so showcase this message in the HUD
                    //get the information text from the object and send it to the controller
                    if (hitInfo.collider.gameObject.GetComponent<InformationHelper>() != null && GameObject.FindWithTag("HUDCanvas") != null)
                    {
                       
                        //
                        if (hitInfo.collider.gameObject.GetComponent<InformationHelper>().isTree && !seenTreeInformation)
                        {
                            seenTreeInformation = true;
                        }

                        else if (hitInfo.collider.gameObject.GetComponent<InformationHelper>().isTree && seenTreeInformation) return; 



                        //else return; 


                        //find the hudcontroller object and call the ShowcaseMessage function with the informationHelper message
                        InformationHelper senderInfo = hitInfo.collider.gameObject.GetComponent<InformationHelper>();

                        //check for wavescript in order to stop wave animation when active
                        WaveScript waveScript = hitInfo.collider.gameObject.GetComponent<WaveScript>(); 
                        if (waveScript != null) {
                            waveScript.SetInterActionComplete(); 
                        
                        }

                        if (senderInfo.keyTextIsSentence == false) GameObject.FindWithTag("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(senderInfo.GetTranslatedText(), senderInfo);
                        else GameObject.FindWithTag("HUDCanvas").GetComponent<HUDController>().ShowcaseMessage(senderInfo.GetTranslatedText(), senderInfo, senderInfo.GetMultipleTranslatedSentences());
                    }

                }

                else if (hitInfo.collider.gameObject.CompareTag("Objectives"))
                {
                    gameObject.transform.Find("Geometry").gameObject.transform.gameObject.tag = "ObjectiveDone";

                }
            }
        }
        else
        {
            //if (textUI != null)
            //{
            //    textUI.gameObject.SetActive(false);

            //}

            }
        }

    }

