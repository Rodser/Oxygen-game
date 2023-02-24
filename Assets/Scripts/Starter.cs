using UnityEngine;

namespace Rodlix
{
    public class Starter : MonoBehaviour
    {
        [SerializeField] private WorldGenerator generator = null;
        [SerializeField] private Base baseBlocks = null;

        private BlockRenderer blockRenderer = null;
        private GameObject[,,] gameObjects = null;

        private void Start()
        {
            Block[,,] blocks = generator.StartGeneration(baseBlocks);

            //  renderer
            Vector3Int worldSize = generator.WorldSize;
            blockRenderer = gameObject.AddComponent<BlockRenderer>();
            blockRenderer.SetWorldSize(worldSize);
            gameObjects = blockRenderer.Generate(blocks);

            //  player
        }
    }
}