using System;
using UnityEngine;

namespace Rodlix
{
    [Serializable]
    public class SoilGenerator
    {
        [SerializeField] private BlockInfo blockInfo = null;

        [SerializeField] public float frequency = 1.0f;
        [SerializeField] float amplitude = 1.0f;
        [SerializeField, Range(0, 1)] public float upperThreshold = 1;

        private static int chunkNumber = 100;
        private int blockNumber = 0;

        public void Generate(Block[,,] blocks, Vector3Int size)
        {
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
                            float noiseDown = Mathf.PerlinNoise(xOf, zOf) * amplitude;

                            if (y < Mathf.Lerp(0, upperThreshold * size.y, noiseDown))
                            {
                                blocks[x, y, z] = blockInfo.GetBlock(chunkNumber);
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
