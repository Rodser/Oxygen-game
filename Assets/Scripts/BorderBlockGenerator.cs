using System;
using UnityEngine;

namespace Rodlix
{
    [Serializable]
    public class BorderBlockGenerator
    {
        [SerializeField] private BlockInfo blockInfo = null;
        [SerializeField] private int yLower;
        [SerializeField] private int yUpper;
        [SerializeField] private int sideLimiter;

        public void Generate(Block[,,] blocks, byte size)
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        int deviation = UnityEngine.Random.Range(0, 4);

                        if (y < yLower + deviation || 
                            x < sideLimiter + deviation || x > size - (sideLimiter + deviation) || 
                            z < sideLimiter + deviation || z > size - (sideLimiter + deviation))
                        {
                            blocks[x, y, z] = blockInfo.GetPrefab();
                        }
                    }
                }
            }
        }
    } 
}