using System;
using UnityEngine;

namespace Rodlix
{
    [CreateAssetMenu(fileName = "WorldGenerator", menuName = "MyGame/WorldGenerator", order = 0)]
    public class WorldGenerator : ScriptableObject
    {
        [SerializeField] private Vector3Int WorldSize;
        [Space(10)]
        [SerializeField] private bool isActiveBuildings = true;
        [SerializeField] private Construction startBuilding = null;
        [Space(10)]
        [SerializeField] private bool isActiveBorder = true;
        [SerializeField] private BorderBlockGenerator borderBlockGenerator = null;
        [Space(10)]
        [SerializeField] private bool isActiveSoilLayers = true;
        [SerializeField] private BlockGenerator[] generators = null;
        [Space(10)]
        [SerializeField] private bool isActiveBiomes = true;
        [SerializeField] private BiomeGenerator[] biomeGenerators = null;

        public Block[,,] StartGeneration(Base baseBlocks)
        {
            Block[,,] blocks = new Block[WorldSize.x, WorldSize.y, WorldSize.z];

            // граница
            if (isActiveBorder)
            {
                borderBlockGenerator.Generate(blocks, baseBlocks, WorldSize);
            }

            if (isActiveBiomes)
            {
                foreach (var generator in biomeGenerators)
                {
                    generator.Generate(blocks, WorldSize);
                }
            }

            // грунт
            // жидкость
            // газ
            if (isActiveSoilLayers)
            {
                foreach (var generator in generators)
                {
                    generator.Generate(blocks, WorldSize);
                }
            }

            // предметы
            if (isActiveBuildings)
            {
                SpawnBuilding(blocks);
            }

            Debug.Log("End WorldGenerate");
            return blocks;
        }

        internal Vector3Int GetSize()
        {
            return WorldSize;
        }

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