using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniblocks;

public class MapManager : MonoBehaviour 
{
    private Transform playerTrans;
    private Index lastIndex;

	void Start () 
	{
        playerTrans = GameObject.FindWithTag("Player").transform;

        InvokeRepeating("GenerateMap", 1, 0.5f);
	}

    private void GenerateMap()
    {
        if (Engine.Initialized == false || ChunkManager.Initialized == false) return;

        Index currentIndex = Engine.PositionToChunkIndex(playerTrans.position);

        if (lastIndex != currentIndex)
        {
            ChunkManager.SpawnChunks(playerTrans.position);
            lastIndex = currentIndex;
        }
    }

    private void SaveMap()
    {
        Engine.SaveWorld();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            SaveMap();
    }
}
