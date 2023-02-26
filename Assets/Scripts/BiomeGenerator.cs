using System;
using UnityEngine;

namespace Rodlix
{
    [Serializable]
    public class BiomeGenerator
    {
        [SerializeField] private BlockInfo blockInfo = null;
        [SerializeField] private int count = 1;
        // [SerializeField] private float amplitude = 1f;
        [SerializeField, Range(1,3)] private float frequency = 1f;
        [SerializeField] private int minSize = 1;
        [SerializeField] private int maxSize = 10;

        [Range(0, 1)] public float lowerThreshold = 0f;
        [Range(0, 1)] public float upperThreshold = 1f;

        private static int chunkNumber = 100;

        public void Generate(Block[,,] blocks, Vector3Int size)
        {
            for (int i = 0; i < count; i++)
            {
                BiomeGen(blocks, size);
                chunkNumber++;
            }
        }

        private void BiomeGen(Block[,,] blocks, Vector3Int size)
        {
            int sizeBiome = UnityEngine.Random.Range(minSize, maxSize);

            int xc = (int)UnityEngine.Random.Range(0, size.x);
            int yc = (int)UnityEngine.Random.Range(size.y * lowerThreshold, size.y * upperThreshold);
            int zc = (int)UnityEngine.Random.Range(0, size.z);

            int xcmin = xc - sizeBiome;
            int xcmax = xc + sizeBiome;
            int xmin = xcmin > 0 ? xcmin : 0;
            int xmax = xcmax < size.x ? xcmax : size.x;

            int ycmin = yc - sizeBiome;
            int ycmax = yc + sizeBiome;
            int ymin = ycmin > 0 ? ycmin : 0;
            int ymax = ycmax < size.y ? ycmax : size.y;

            int zcmin = zc - sizeBiome;
            int zcmax = zc + sizeBiome;
            int zmin = zcmin > 0 ? zcmin : 0;
            int zmax = zcmax < size.z ? zcmax : size.z;

            for (int y = ymin; y < ymax; y++)
            {
                for (int x = xmin; x < xmax; x++)
                {
                    for (int z = zmin; z < zmax; z++)
                    {
                        if(blocks[x, y, z] != null && blocks[x, y, z].elementType == ElementType.Indestructible)
                        {
                            continue;
                        }

                        float xOf = (float)x / sizeBiome * frequency;
                        float yOf = (float)y / sizeBiome * frequency;
                        float zOf = (float)z / sizeBiome * frequency;
                        float noiseX = Mathf.PerlinNoise(yOf, zOf); // * amplitude;
                        float noiseY = Mathf.PerlinNoise(xOf, zOf); // * amplitude;
                        float noiseZ = Mathf.PerlinNoise(xOf, yOf); // * amplitude;

                        if (y > Mathf.Lerp(ymin, yc, noiseY) && y < Mathf.Lerp(yc, ymax, noiseY) &&
                            x > Mathf.Lerp(xmin, xc, noiseX) && x < Mathf.Lerp(xc, xmax, noiseX) &&
                            z > Mathf.Lerp(zmin, zc, noiseZ) && z < Mathf.Lerp(zc, zmax, noiseZ))
                        {
                            blocks[x, y, z] = blockInfo.GetBlock(chunkNumber);
                        }
                    }
                }
            }
        }
    }
}