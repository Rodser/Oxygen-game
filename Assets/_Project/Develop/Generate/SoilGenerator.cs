using System;
using UnityEngine;

namespace Rodlix
{
    [Serializable]
    public class SoilGenerator
    {
        [SerializeField] private BlockInfo blockInfo = null;

        [SerializeField] float frequency = 1.0f;
        [SerializeField] int amplitude = 1;
        [SerializeField, Range(0, 1)] public float threshold = 1.0f;

        private static int chunkNumber = 100;
        private int blockNumber = 0;

        public void Generate(Block[,,] blocks, Vector3Int size)
        {
            float downThreshold = threshold * size.y - amplitude;
            float upperThreshold = threshold * size.y + amplitude;

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    for (int z = 0; z < size.z; z++)
                    {
                        if (blocks[x, y, z] == null)
                        {
                            float xOf = (float)x / size.x * frequency;
                            float zOf = (float)z / size.z * frequency;
                            float noise = Mathf.PerlinNoise(xOf, zOf);



                            if (y < Mathf.Lerp(downThreshold, upperThreshold, noise))
                            {
                                blocks[x, y, z] = blockInfo.GetBlock(chunkNumber, new Vector3Int(x,y,z));
                                CheckCapacityofBlocks();
                            }
                        }
                    }
                }
            }
            chunkNumber++;
        }

        private void CheckCapacityofBlocks()
        {
            blockNumber++;
            if (blockNumber > 10000)
            {
                chunkNumber++;
                blockNumber = 0;
            }
        }
    }
}
