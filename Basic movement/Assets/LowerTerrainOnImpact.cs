using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerTerrainOnImpact : MonoBehaviour
{
    private Terrain Terrain;

    public GameObject gj; 

    private void Awake()
    {
        Terrain = GetComponent<Terrain>();
        SetTerrainHeight();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
             InventoryController  invc = collision.collider.gameObject.GetComponent<raycaster>().getInvController();
            if (invc.GetItem().unlockAbles.Contains(this.gameObject)) {
                LowerTerrain(collision);
                SpawnInteractable(collision.GetContact(0).point);
                invc.ClearInventory(); 

            }       
        }    
    }

    public void SetTerrainOk() {
        SetTerrainHeight();
    }


    private void SpawnInteractable(Vector3 point)
    {
        Instantiate(gj, point, Quaternion.identity);    
    }

    private void LowerTerrain(Collision collision)
    {
        DigCollisionSq(collision); 
    }

    private void DigCircle(Collision collision)
    {
        float[,] heights = GetTerrainDataHeight();
        Terrain.terrainData.SetHeights(0, 0, heights);
    }

    private void DigCollisionPoint(Collision collision)
    {
        float[,] heights = GetTerrainDataHeight();
        foreach (ContactPoint contactPoint in collision.contacts)
        {
            Vector3 terrainCoordinates= ConvertWorldCoordinatesToTerrainCoordinates(contactPoint.point);
            int x= Mathf.RoundToInt(terrainCoordinates.x);
            int z = Mathf.RoundToInt(terrainCoordinates.z);
            heights[z, x] = 0.0f;
        }
        Terrain.terrainData.SetHeights(0, 0, heights);
    }


    private void DigCollisionSq(Collision collision)
    {

        Vector3 terrainCoordinate = ConvertWorldCoordinatesToTerrainCoordinates(collision.GetContact(0).point);

        float[,] heights = GetTerrainDataHeight();
        float minX =  terrainCoordinate.x - GetRange(1f);
        if (minX < 0) { minX = 0; }
        float maxX = terrainCoordinate.x + GetRange(1f);
        if (maxX > heights.GetLength(0) ) { maxX = heights.GetLength(0);}

        float minZ = terrainCoordinate.z - GetRange(1f);
        if (minZ < 0) { minZ = 0; }
        float maxZ = terrainCoordinate.z + GetRange(1f);
        if (maxX > heights.GetLength(1)) { maxX = heights.GetLength(1); }



        for (int i = Mathf.RoundToInt(minX); i < Mathf.RoundToInt(maxX) ; i++)
        {
            for (int j = Mathf.RoundToInt(minZ); j < Mathf.RoundToInt(maxZ); j++) {
                heights[j, i] = 0.0f;
            }
        }
        Terrain.terrainData.SetHeights(0, 0, heights);
    }


    private void SetTerrainHeight()
    {
        float[,] heights = GetTerrainDataHeight();

        for (int i = 0; i < heights.GetLength(0); i++)
        {
            for (int j = 0; j < heights.GetLength(1); j++)
            {
                heights[i, j] = 0.002929866f;
            }
        }
        Terrain.terrainData.SetHeights(0, 0, heights);
    }

    private float[,] GetTerrainDataHeight()
    {
        int xRes = Terrain.terrainData.alphamapWidth;
        int yRes = Terrain.terrainData.alphamapHeight;
        return Terrain.terrainData.GetHeights(0, 0, xRes, yRes);
    }

    private float GetRange(float f) {
         return f / Terrain.terrainData.size.x * Terrain.terrainData.alphamapWidth;
    }

    private Vector3 ConvertWorldCoordinatesToTerrainCoordinates(Vector3 worldCoordinates)
    {
         Vector3 vectorReturn = new Vector3();
        Vector3 terrainPosition = Terrain.transform.position;
        vectorReturn.x = (worldCoordinates.x - terrainPosition.x) / Terrain.terrainData.size.x * Terrain.terrainData.alphamapWidth;
        vectorReturn.z = (worldCoordinates.z - terrainPosition.z) / Terrain.terrainData.size.z * Terrain.terrainData.alphamapHeight;
        return vectorReturn;
    }

    private Vector3 ConvertTerrainToWorldCoordinates(Vector3 terrainCoordinates)
    {
        Vector3 vectorReturn = new Vector3();
        Vector3 terrainPosition = Terrain.transform.position;
        vectorReturn.x = (terrainCoordinates.x) * Terrain.terrainData.size.x / Terrain.terrainData.alphamapWidth + terrainPosition.x;
        vectorReturn.z = (terrainCoordinates.z) * Terrain.terrainData.size.z / Terrain.terrainData.alphamapHeight +  terrainPosition.z;
        return vectorReturn;
    }
}
