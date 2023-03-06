using System;
using UnityEngine;

namespace Rodlix
{
    [Serializable]
    public class BorderBlockGenerator
    {
        [SerializeField] private float amplitude;
        [SerializeField] private float frequency;
        [SerializeField] private int min;
        [SerializeField] private int max;
        [SerializeField] private int countLayers = 7;

        private BlockInfo blockInfo = null;
        private int chunkNumber = 0;
        private int layer = 0;

        public void Generate(Block[,,] blocks, Base baseBlocks, Vector3Int size)
        {
            blockInfo = baseBlocks.FindBlock(ElementType.Indestructible);
            for (int y = 0; y < size.y; y++)
            {
                layer = (int)((float)y / size.y * countLayers) + countLayers;

                for (int x = 0; x < size.x; x++)
                {
                    for (int z = 0; z < size.z; z++)
                    {
                        float xOf = (float)x / size.x * frequency;
                        float yOf = (float)y / size.y * frequency;
                        float zOf = (float)z / size.z * frequency;
                        float noiseX = Mathf.PerlinNoise(yOf, zOf) * amplitude;
                        float noiseY = Mathf.PerlinNoise(xOf, zOf) * amplitude;
                        float noiseZ = Mathf.PerlinNoise(xOf, yOf) * amplitude;

                        if (y < Mathf.Lerp(min, max + 1, noiseY) || 
                            x < Mathf.Lerp(min, max + 1, noiseX) || x > Mathf.Lerp(size.x - max, size.x - min + 1, noiseX) || 
                            z < Mathf.Lerp(min, max + 1, noiseZ) || z > Mathf.Lerp(size.z - max, size.z - min + 1, noiseZ))
                        {
                            if(layer == countLayers)
                            {
                                chunkNumber = (int)((float)x/size.x * countLayers);
                            }
                            else
                            {
                                chunkNumber = layer;
                            }

                            blocks[x, y, z] = blockInfo.GetBlock(chunkNumber);
                        }
                    }
                }
            }
        }
    } 
}