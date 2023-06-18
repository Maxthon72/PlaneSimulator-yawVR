using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProceduralToolkit.Examples;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;

public class EndlessClouds : MonoBehaviour
{
    public const float maxViewDst = 100;
    public Transform viewer;
    public MeshSettings meshSettings;
    public GameObject clouds;
    public static Vector2 viewerPosition;
    int chunksVisibleInViewDst;

    Dictionary<Vector2, CloudChunk> waterChunkDictionary = new Dictionary<Vector2, CloudChunk>();
    List<CloudChunk> cloudChunksVisibleLastUpdate = new List<CloudChunk>();

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
        cloudChunksVisibleLastUpdate.Clear();

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / meshSettings.meshWorldSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / meshSettings.meshWorldSize);

        for (int yOffset = -chunksVisibleInViewDst; yOffset <= chunksVisibleInViewDst; yOffset++)
        {
            for (int xOffset = -chunksVisibleInViewDst; xOffset <= chunksVisibleInViewDst; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

                if (waterChunkDictionary.ContainsKey(viewedChunkCoord))
                {
                    waterChunkDictionary[viewedChunkCoord].UpdateCloudChunk();
                    if (waterChunkDictionary[viewedChunkCoord].IsVisible())
                    {
                        cloudChunksVisibleLastUpdate.Add(waterChunkDictionary[viewedChunkCoord]);
                    }
                }
                else
                {
                    waterChunkDictionary.Add(viewedChunkCoord, new CloudChunk(viewedChunkCoord, (int)meshSettings.meshWorldSize, transform, clouds));
                }

            }
        }
    }

    public class CloudChunk
    {
        GameObject meshObject;
        GameObject particleSystem;
        Vector2 position;
        Bounds bounds;

        public CloudChunk(Vector2 coord, int size, Transform parent, GameObject clouds)
        {
            this.meshObject = clouds;
            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(position.x, 1000f, position.y);


            meshObject.transform.position = positionV3;
            //meshObject.transform.localScale = Vector3.one * size / 10f;
            meshObject.transform.parent = parent;
        }

        public void UpdateCloudChunk()
        {
            float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
            bool visible = viewerDstFromNearestEdge <= maxViewDst;
        }

        public bool IsVisible()
        {
            return meshObject.activeSelf;
        }

    }
}
