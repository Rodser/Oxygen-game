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
        
        private Construction building;

        public Block[,,] StartGeneration(Base baseBlocks, vThirdPersonCamera observer, float scale)
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
                building = SpawnBuilding(blocks, scale);
            }

            if (building != null)
            {
                building.SetObserver(observer);
            }

            Debug.Log("End WorldGenerate");
            return blocks;
        }

        internal Vector3Int GetSize()
        {
            return WorldSize;
        }

        private Construction SpawnBuilding(Block[,,] blocks, float scale)
        {
            if(startBuilding == null) { return null; }

            int wsx = (int)(WorldSize.x * 0.5f);
            int wsy = (int)(WorldSize.y * 0.5f);
            int wsz = (int)(WorldSize.z * 0.5f);

            Vector3Int position = new Vector3Int(wsx, wsy, wsz);

            int minXPos = position.x + (int)startBuilding.MinPoint.transform.position.x;
            int minYPos = position.y + (int)startBuilding.MinPoint.transform.position.y;
            int minZPos = position.z + (int)startBuilding.MinPoint.transform.position.z;

            int maxXPos = position.x + (int)startBuilding.MaxPoint.transform.position.x + 1;
            int maxYPos = position.y + (int)startBuilding.MaxPoint.transform.position.y + 1;
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

            Vector3 instPosition = new Vector3(position.x * scale, position.y * scale, position.z * scale);
            Construction building = Instantiate(startBuilding, instPosition, Quaternion.identity);

            return building;
        }


        // живность
        // игрок
    }
}