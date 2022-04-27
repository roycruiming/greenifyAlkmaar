using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{

    Terrain Terrain; 

    private void Awake()
    {
        Terrain = GetComponent<Terrain>();
    }

    private float[,] GetTerrainDataHeight()
    {
        int xRes = Terrain.terrainData.alphamapWidth;
        int yRes = Terrain.terrainData.alphamapHeight;
        return Terrain.terrainData.GetHeights(0, 0, xRes, yRes);
    }

    private Vector3 ConvertWorldCoordinatesToTerrainCoordinates(Vector3 worldCoordinates)
    {
        Vector3 vectorReturn = new Vector3();
        Vector3 terrainPosition = Terrain.transform.position;
        vectorReturn.x = (worldCoordinates.x - terrainPosition.x) / Terrain.terrainData.size.x * Terrain.terrainData.alphamapWidth;
        vectorReturn.z = (worldCoordinates.z - terrainPosition.z) / Terrain.terrainData.size.z * Terrain.terrainData.alphamapHeight;
        return vectorReturn;
    }




    private void OnCollisionEnter(Collision collision)
    {
        TerrainLowerer lowerer = collision.gameObject.GetComponent<TerrainLowerer>();
        




        if (lowerer != null) {

            float[,] heights = GetTerrainDataHeight();

            foreach (ContactPoint c in collision.contacts) {
                Vector3 terrainCoordinates = ConvertWorldCoordinatesToTerrainCoordinates(c.point);
                int x = Mathf.RoundToInt(terrainCoordinates.x);
                int z = Mathf.RoundToInt(terrainCoordinates.z);
                heights[z, x] = 0.0f;

            }     
        
        }

    }
}
