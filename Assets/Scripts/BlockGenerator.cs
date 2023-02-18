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

        public void Generate(Block[,,] blocks, byte size)
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        if (blocks[x, y, z] == null)
                        {
                            float xOf = (float)x / size * frequency;
                            float zOf = (float)z / size * frequency;
                            float noise = Mathf.PerlinNoise(xOf, zOf) * amplitude;

                            if (y < Mathf.Lerp(lowerThreshold * size, size, noise) ||
                                y > Mathf.Lerp(upperThreshold * size, size, 1 - noise))
                            {
                                blocks[x, y, z] = blockInfo.GetPrefab();
                            }
                        }
                    }
                }
            }
        }
    }
}
