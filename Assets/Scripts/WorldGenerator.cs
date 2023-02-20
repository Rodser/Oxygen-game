using System;
using UnityEngine;

namespace Rodlix
{
    [CreateAssetMenu(fileName = "WorldGenerator", menuName = "MyGame/WorldGenerator", order = 0)]
    public class WorldGenerator : ScriptableObject
    {
        [SerializeField] private Vector3Int worldSize;
        [SerializeField] private Construction startBuilding = null;
        [Space(10)]
        [SerializeField] private BorderBlockGenerator borderBlockGenerator = null;
        [SerializeField] private BlockGenerator[] generators = null;

        public GameObject[,,] StartGeneration(Base baseBlocks)
        {
            Block[,,] blocks = new Block[worldSize.x, worldSize.y, worldSize.z];

        // граница
            borderBlockGenerator.Generate(blocks, baseBlocks, worldSize);

        // грунт
        // жидкость
        // газ
            foreach (var generator in generators)
            {
                generator.Generate(blocks, worldSize);
            }

            SpawnBuilding(blocks);

            return RenderGenerate(blocks);
        }

        // предметы
        private void SpawnBuilding(Block[,,] blocks)
        {
            Vector3Int position = new Vector3Int(worldSize.x/2, worldSize.y/2, worldSize.z/2);
            GameObject obj = Instantiate(startBuilding.gameObject, position, Quaternion.identity);

            int minXPos = position.x + startBuilding.minPosition.x;
            int maxXPos = position.x + startBuilding.maxPosition.x + 1;
            int minYPos = position.y + startBuilding.minPosition.y;
            int maxYPos = position.y + startBuilding.maxPosition.y + 1;
            int minZPos = position.z + startBuilding.minPosition.z;
            int maxZPos = position.z + startBuilding.maxPosition.z + 1;

            for (int x = minXPos; x < maxXPos; x++)
            {
                for (int y = minYPos; y < maxYPos; y++)
                {
                    for(int z = minZPos; z < maxZPos; z++)
                    {
                        blocks[x, y, z] = null;
                    }
                }
            }

        }

        public GameObject[,,] RenderGenerate(Block[,,] blocks)
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

        // живность
        // игрок
    }
}