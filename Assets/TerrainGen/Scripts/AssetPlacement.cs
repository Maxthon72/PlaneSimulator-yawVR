using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetPlacement : MonoBehaviour
{
    public static void SpawnAssetsOnChunkVerts(Vector3 pos, Vector2 centre, float assetOffSet, Vector2 offsetMultiplier, GameObject[] treePrefabs, Transform treeParent)
    {
        if (pos.y >= 85f && pos.y <= 210f && Random.Range(0, 2200) == 1) 
        {
                GameObject treeXY = Object.Instantiate(treePrefabs[0], new Vector3(pos.x + assetOffSet * offsetMultiplier.x, pos.y, pos.z + assetOffSet * offsetMultiplier.y), Quaternion.identity);
                treeXY.name = "Tree0";
                treeXY.transform.localScale = Vector3.one * Random.Range(10, 25);
                treeXY.transform.Rotate(0, Random.Range(-90, 90), 0);
                treeXY.transform.parent = treeParent.transform;
        }
        else if (pos.y >= 39f && pos.y <= 90f && Random.Range(0, 2800) == 1)
        {
                GameObject treeXY = Object.Instantiate(treePrefabs[1], new Vector3(pos.x + assetOffSet * offsetMultiplier.x, pos.y, pos.z + assetOffSet * offsetMultiplier.y), Quaternion.identity);
                treeXY.name = "Tree1";
                treeXY.transform.localScale = Vector3.one * Random.Range(10, 20);
                treeXY.transform.Rotate(0, Random.Range(-90, 90), 0);
                treeXY.transform.parent = treeParent.transform;
        }
        else if (pos.y >= 34f && pos.y <= 42f && Random.Range(0, 3300) == 1)
        {
                GameObject treeXY = Object.Instantiate(treePrefabs[2], new Vector3(pos.x + assetOffSet * offsetMultiplier.x, pos.y, pos.z + assetOffSet * offsetMultiplier.y), Quaternion.identity);
                treeXY.name = "Tree2";
                treeXY.transform.localScale = Vector3.one * Random.Range(10, 20);
                treeXY.transform.Rotate(0, Random.Range(-90, 90), 0);
                treeXY.transform.parent = treeParent.transform;
        }
    }
}
