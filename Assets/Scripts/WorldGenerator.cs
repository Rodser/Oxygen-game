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

        public Block[,,] StartGeneration()
        {
            Block[,,] blocks = new Block[worldSize, worldSize, worldSize];

            borderBlockGenerator.Generate(blocks, worldSize);

            foreach (var generator in generators)
            {
                generator.Generate(blocks, worldSize);
            }

            RenderGenerate(blocks, worldSize);
            return blocks;
        }

        public void RenderGenerate(Block[,,] blocks, byte size)
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        if (blocks[x,y,z] == null)
                        {
                            continue;
                        } 

                        var position = new Vector3(x, y, z);
                        Instantiate(blocks[x, y, z], position, Quaternion.identity);
                    }
                }
            }
        }

        // граница
        // грунт
        // жидкость
        // газ
        // предметы
        // живность
        // игрок
    }
}