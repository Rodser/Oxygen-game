using UnityEngine;

namespace Rodlix
{
    [CreateAssetMenu(fileName = "BlockGenerator", menuName = "MyGame/BlockGenerator", order = 2)]
    public class BlockGenerator : ScriptableObject
    {
        [SerializeField] private BlockInfo[] blocksList = null;
        [SerializeField, Range(0.0f, 2.0f)] private float amplitude;
        [SerializeField, Range(0.0f, 1.0f)] private float ofs;

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

                        float xOf = (float)x / size;
                        float zOf = (float)z / size;


                        float noise = Mathf.PerlinNoise(xOf, zOf) * ofs;

                        Debug.Log(xOf);
                        Debug.Log(noise);
                        Debug.Log(Mathf.Lerp(0.0f, size * amplitude, noise));


                        if (y < Mathf.Lerp(0.0f, size * amplitude, noise))
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
