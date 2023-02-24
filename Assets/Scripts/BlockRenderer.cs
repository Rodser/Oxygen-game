using UnityEngine;

namespace Rodlix
{
    public class BlockRenderer : MonoBehaviour
    {
        private Vector3Int worldSize;

        public void SetWorldSize(Vector3Int worldSize)
        {
            this.worldSize = worldSize;
        }

        public GameObject[,,] Generate(Block[,,] blocks)
        {
            GameObject[,,] gameObjects = new GameObject[worldSize.x, worldSize.y, worldSize.z];

            for (int y = 0; y < worldSize.y; y++)
            {
                for (int x = 0; x < worldSize.x; x++)
                {
                    for (int z = 0; z < worldSize.z; z++)
                    {
                        Block block = blocks[x, y, z];
                        GameObject prefab = block?.prefab;
                        if (prefab != null)
                        {
                            Vector3 position = new Vector3(x, y, z);
                            GameObject gameObject = Instantiate(prefab, position, Quaternion.identity);
                            gameObject.GetComponent<Renderer>().material = block.material;
                            gameObjects[x, y, z] = gameObject;
                        }
                    }
                }
            }
            return gameObjects;
        }
    }
}