using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniblocks;

public class BlockManager : MonoBehaviour 
{
    public float range = 5;
    private ushort blockID = 0;
    private Transform selectEffect;
    private MeshRenderer selectRender;

    void Start()
    {
        selectEffect = GameObject.Find("selected block graphics").transform;
        selectRender = selectEffect.GetComponent<MeshRenderer>();
        selectRender.enabled = false;
    }

    private void SelectBlock()
    {
        for (ushort i = 0; i < 9; i++)
        {
            if(Input.GetKeyDown(i.ToString()))
            {
                blockID = i;
            }
        }
    }

    private void UpdateSelectEffect(VoxelInfo info)
    {
        if(info !=null)
        {
            selectEffect.position = info.chunk.VoxelIndexToPosition(info.index);
            selectRender.enabled = true;
        }
        else
        {
            selectRender.enabled = false;
        }
    }

	void Update () 
	{
        SelectBlock();

        Transform camTrans = Camera.main.transform;

        VoxelInfo info = Engine.VoxelRaycast(camTrans.position, camTrans.forward, range, false);
        
        if(info != null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Voxel.DestroyBlock(info);
            }

            if(Input.GetMouseButtonDown(1))
            {
                VoxelInfo neighbourInfo = new VoxelInfo(info.adjacentIndex, info.chunk);
                Voxel.PlaceBlock(neighbourInfo, blockID);
            }
        }

        UpdateSelectEffect(info);
	}
}
