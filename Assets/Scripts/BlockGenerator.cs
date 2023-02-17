using UnityEngine;

namespace Rodlix
{
    [CreateAssetMenu(fileName = "BlockGenerator", menuName = "MyGame/BlockGenerator", order = 2)]
    public class BlockGenerator : ScriptableObject
    {
        [SerializeField] private BlockInfo[] blocksList = null;
        [SerializeField] private float frequency = 1.0f;
        [SerializeField] private float amplitude = 1.0f;
        [SerializeField, Range(0, 1)] private float lowerThreshold = 0;
        [SerializeField, Range(0, 1)] private float upperThreshold = 1;

        internal void GenerateBlocks(Block[,,] blocks, byte size)
        {
            if (blocksList is null || blocksList.Length < 1) return;

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        if (blocks[x, y, z] != null)
                        {
                            continue;
                        }

                        float xOf = (float)x / size * frequency;
                        float zOf = (float)z / size * frequency;


                        float noise = Mathf.PerlinNoise(xOf, zOf) * amplitude;

                        if (y < Mathf.Lerp(lowerThreshold * size, size, noise) || 
                            y > Mathf.Lerp(upperThreshold * size, size, 1 - noise))
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
}
