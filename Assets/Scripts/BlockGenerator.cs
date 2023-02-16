using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    [SerializeField] private byte size = 10;
    [SerializeField] private Block[] blocks = null;

    private void Start()
    {
        if (blocks is null) return;
        
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                for (int z = 0; z < size; z++)
                {
                    int blockPlace = Random.Range(0, blocks.Length);
                    var block = blocks[blockPlace].GetPrefab();
                    var position = new Vector3(x, y, z);

                    Instantiate(block, position, Quaternion.identity);
                }
            }
        }
    }
}
