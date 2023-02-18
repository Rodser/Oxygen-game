using System.Drawing;
using UnityEngine;

namespace Rodlix
{
    [CreateAssetMenu(fileName = "WorldGenerator", menuName = "MyGame/WorldGenerator", order = 0)]
    public class WorldGenerator : ScriptableObject
    {
        [SerializeField] private byte worldSize = 10;
        [SerializeField] private BlockGenerator[] generators = null;

        public Block[,,] StartGeneration()
        {
            Block[,,] blocks = new Block[worldSize, worldSize, worldSize];
            
            foreach (var generator in generators)
            {
                generator.GenerateBlocks(blocks, worldSize);
            }

            return blocks;
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