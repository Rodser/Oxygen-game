using System;
using UnityEngine;

namespace Rodlix
{
    [Serializable]
    public class BlockGenerator
    {
        [SerializeField] private BlockInfo blockInfo = null;
        [SerializeField] private float frequency = 1.0f;
        [SerializeField] private float amplitude = 1.0f;
        [SerializeField, Range(0, 1)] private float lowerThreshold = 0;
        [SerializeField, Range(0, 1)] private float upperThreshold = 1;

        public void Generate(Block[,,] blocks, Vector3Int size)
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    for (int z = 0; z < size.z; z++)
                    {
                        if (blocks[x, y, z] == null)
                        {
                            float xOf = (float)x / size.x * frequency;
                            float zOf = (float)z / size.z * frequency;
                            float noiseDown = Mathf.PerlinNoise(xOf, zOf) * amplitude;
                            float noiseUp = 1 - Mathf.Abs(noiseDown);

                            if (y > Mathf.Lerp(lowerThreshold * size.y, upperThreshold * size.y, noiseDown) &&
                                y < Mathf.Lerp(lowerThreshold * size.y, upperThreshold * size.y, noiseUp))
                            {
                                blocks[x, y, z] = blockInfo.GetBlock();
                            }
                        }
                    }
                }
            }
        }
    }
}
