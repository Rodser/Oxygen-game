using System;
using UnityEngine;

namespace Rodlix
{
    [Serializable]
    public class BlockGenerator
    {
        [SerializeField] private BlockInfo blockInfo = null;
        [SerializeField] private Octave[] octaves = null;

        public void Generate(Block[,,] blocks, Vector3Int size)
        {
            foreach (Octave octave in octaves)
            {
                float frequency = octave.frequency;
                float amplitude = octave.amplitude;
                float lowerThreshold = octave.lowerThreshold;
                float upperThreshold = octave.upperThreshold;
                
                size = GenerateOctave(blocks, size, frequency, amplitude, lowerThreshold, upperThreshold);
            }
        }

        private Vector3Int GenerateOctave(Block[,,] blocks, Vector3Int size, float frequency, float amplitude, float lowerThreshold, float upperThreshold)
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
                                blocks[x, y, z] = blockInfo.GetBlock(99);
                            }
                        }
                    }
                }
            }

            return size;
        }
    }
}
