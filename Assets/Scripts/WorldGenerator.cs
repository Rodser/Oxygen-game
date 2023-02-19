using UnityEngine;

namespace Rodlix
{
    [CreateAssetMenu(fileName = "WorldGenerator", menuName = "MyGame/WorldGenerator", order = 0)]
    public class WorldGenerator : ScriptableObject
    {
        [SerializeField] private byte worldSize = 10;
        [SerializeField] private Block prefab = null;
        [Space(10)]
        [SerializeField] private BorderBlockGenerator borderBlockGenerator = null;
        [SerializeField] private BlockGenerator[] generators = null;

        public GameObject[,,] StartGeneration(Base baseBlocks)
        {
            Block[,,] blocks = new Block[worldSize, worldSize, worldSize];

        // граница
            borderBlockGenerator.Generate(blocks, baseBlocks, worldSize);

        // грунт
        // жидкость
        // газ
            foreach (var generator in generators)
            {
                generator.Generate(blocks, worldSize);
            }

            return RenderGenerate(blocks, worldSize);
        }

        public GameObject[,,] RenderGenerate(Block[,,] blocks, byte size)
        {
            GameObject[,,] gameObjects = new GameObject[worldSize, worldSize, worldSize];

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int z = 0; z < size; z++)
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

        // предметы
        // живность
        // игрок
    }
}