using UnityEngine;

namespace Rodlix
{
    public class BlockGenerator : MonoBehaviour
    {
        [SerializeField] private byte size = 10;
        [SerializeField] private BlockInfo[] blocksList = null;

        private Block[,,] blocks = null;

        internal void GenerateBlocks(Block[,,] blocks, byte size)
        {
            if (blocksList is null || blocksList.Length < 1) return;

            blocks = new Block[size, size, size];

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        int blockPlace = Random.Range(0, blocksList.Length);
                        var blockInfo = blocksList[blockPlace];
                        var position = new Vector3(x, y, z);

                        var block = blockInfo.GetPrefab();

                        if (block == null)
                        {
                            continue;
                        }

                        blocks[x, y, z] = Instantiate(block, position, Quaternion.identity);
                    }
                }
            }
        }
    }
}
