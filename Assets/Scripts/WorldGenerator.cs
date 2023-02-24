using System;
using UnityEngine;

namespace Rodlix
{
    [CreateAssetMenu(fileName = "WorldGenerator", menuName = "MyGame/WorldGenerator", order = 0)]
    public class WorldGenerator : ScriptableObject
    {
        [SerializeField] public Vector3Int WorldSize;
        [SerializeField] private Construction startBuilding = null;
        [Space(10)]
        [SerializeField] private BorderBlockGenerator borderBlockGenerator = null;
        [SerializeField] private BlockGenerator[] generators = null;

        public Block[,,] StartGeneration(Base baseBlocks)
        {
            Block[,,] blocks = new Block[WorldSize.x, WorldSize.y, WorldSize.z];

        // граница
            borderBlockGenerator.Generate(blocks, baseBlocks, WorldSize);

        // грунт
        // жидкость
        // газ
            foreach (var generator in generators)
            {
                generator.Generate(blocks, WorldSize);
            }

            SpawnBuilding(blocks);

            return blocks;
        }

        // предметы
        private void SpawnBuilding(Block[,,] blocks)
        {
            if(startBuilding == null) { return; }
            Vector3Int position = new Vector3Int(WorldSize.x/2, WorldSize.y/2, WorldSize.z/2);
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


        // живность
        // игрок
    }
}