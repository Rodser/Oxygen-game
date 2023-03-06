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
        [SerializeField] private SoilGenerator[] generators = null;
        [Space(10)]
        [SerializeField] private bool isActiveBiomes = true;
        [SerializeField] private BiomeGenerator[] biomeGenerators = null;

        public Block[,,] StartGeneration(Base baseBlocks, GameObject observer)
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
                SpawnBuilding(blocks, observer);
            }

            Debug.Log("End WorldGenerate");
            return blocks;
        }

        internal Vector3Int GetSize()
        {
            return WorldSize;
        }

        private void SpawnBuilding(Block[,,] blocks, GameObject observer)
        {
            if(startBuilding == null) { return; }
            Vector3Int position = new Vector3Int(WorldSize.x/2, WorldSize.y/2, WorldSize.z/2);
            Construction building = Instantiate(startBuilding, position, Quaternion.identity);
            building.SetObserver(observer);

            int minXPos = position.x + (int)startBuilding.MinPoint.transform.position.x;
            int maxXPos = position.x + (int)startBuilding.MaxPoint.transform.position.x + 1;
            int minYPos = position.y + (int)startBuilding.MinPoint.transform.position.y;
            int maxYPos = position.y + (int)startBuilding.MaxPoint.transform.position.y + 1;
            int minZPos = position.z + (int)startBuilding.MinPoint.transform.position.z;
            int maxZPos = position.z + (int)startBuilding.MaxPoint.transform.position.z + 1;
            
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