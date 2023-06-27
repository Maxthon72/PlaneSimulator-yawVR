using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProceduralToolkit.Examples;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;

public class EndlessWater : MonoBehaviour
{
    public const float maxViewDst = 3500;
    public Transform viewer;
    public MeshSettings meshSettings;
    public Material waterMaterial;
    public static Vector2 viewerPosition;
    int chunksVisibleInViewDst;

    Dictionary<Vector2, WaterChunk> waterChunkDictionary = new Dictionary<Vector2, WaterChunk>();
    List<WaterChunk> waterChunksVisibleLastUpdate = new List<WaterChunk>();

    void Start()
    {        
        chunksVisibleInViewDst = Mathf.RoundToInt(maxViewDst / meshSettings.meshWorldSize);
    }

    void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
        UpdateVisibleChunks();
    }

    void UpdateVisibleChunks()
    {

        for (int i = 0; i < waterChunksVisibleLastUpdate.Count; i++)
        {
            waterChunksVisibleLastUpdate[i].SetVisible(false);
        }
        waterChunksVisibleLastUpdate.Clear();

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / meshSettings.meshWorldSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / meshSettings.meshWorldSize);

        if (!meshSettings.isMenu)
        {
            for (int yOffset = -chunksVisibleInViewDst; yOffset <= chunksVisibleInViewDst; yOffset++)
            {
                for (int xOffset = -chunksVisibleInViewDst; xOffset <= chunksVisibleInViewDst; xOffset++)
                {
                    Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

                    if (waterChunkDictionary.ContainsKey(viewedChunkCoord))
                    {
                        waterChunkDictionary[viewedChunkCoord].UpdateTerrainChunk();
                        if (waterChunkDictionary[viewedChunkCoord].IsVisible())
                        {
                            waterChunksVisibleLastUpdate.Add(waterChunkDictionary[viewedChunkCoord]);
                        }
                    }
                    else
                    {
                        waterChunkDictionary.Add(viewedChunkCoord, new WaterChunk(viewedChunkCoord, (int)meshSettings.meshWorldSize, transform, waterMaterial));
                    }

                }
            }
        }
        else
        {
            for (int yOffset = 0; yOffset <= chunksVisibleInViewDst; yOffset++)
            {
                for (int xOffset = -chunksVisibleInViewDst; xOffset <= chunksVisibleInViewDst; xOffset++)
                {
                    Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

                    if (waterChunkDictionary.ContainsKey(viewedChunkCoord))
                    {
                        waterChunkDictionary[viewedChunkCoord].UpdateTerrainChunk();
                        if (waterChunkDictionary[viewedChunkCoord].IsVisible())
                        {
                            waterChunksVisibleLastUpdate.Add(waterChunkDictionary[viewedChunkCoord]);
                        }
                    }
                    else
                    {
                        waterChunkDictionary.Add(viewedChunkCoord, new WaterChunk(viewedChunkCoord, (int)meshSettings.meshWorldSize, transform, waterMaterial));
                    }

                }
            }
        }
    }

    public class WaterChunk
    {

        GameObject meshObject;
        Vector2 position;
        Bounds bounds;

        public WaterChunk(Vector2 coord, int size, Transform parent, Material material)
        {
            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(position.x, 35f, position.y);
            meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            meshObject.layer = 4;
            meshObject.GetComponent<MeshRenderer>().material = material;
            meshObject.transform.position = positionV3;
            meshObject.transform.localScale = Vector3.one * size / 10f;
            meshObject.transform.parent = parent;
            SetVisible(false);
        }

        public void UpdateTerrainChunk()
        {
            float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
            bool visible = viewerDstFromNearestEdge <= maxViewDst;
            SetVisible(visible);
        }

        public void SetVisible(bool visible)
        {
            meshObject.SetActive(visible);
        }

        public bool IsVisible()
        {
            return meshObject.activeSelf;
        }

    }
}