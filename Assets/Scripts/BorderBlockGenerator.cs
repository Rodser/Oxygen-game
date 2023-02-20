using System;
using UnityEngine;

namespace Rodlix
{
    [Serializable]
    public class BorderBlockGenerator
    {
        private BlockInfo blockInfo = null;
        [SerializeField] private int yLower;
        [SerializeField] private int sideLimiter;

        public void Generate(Block[,,] blocks, Base baseBlocks, Vector3Int size)
        {
            blockInfo = baseBlocks.FindBlock(ElementType.Indestructible);

            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    for (int z = 0; z < size.z; z++)
                    {
                        int deviation = UnityEngine.Random.Range(0, 4);

                        if (y < yLower + deviation || 
                            x < sideLimiter + deviation || x > size.x - (sideLimiter + deviation) || 
                            z < sideLimiter + deviation || z > size.z - (sideLimiter + deviation))
                        {
                            blocks[x, y, z] = blockInfo.GetBlock();
                        }
                    }
                }
            }
        }
    } 
}