using UnityEngine;

namespace Rodlix
{
    public class Starter : MonoBehaviour
    {
        [SerializeField] private WorldGenerator generator = null;
        [SerializeField] private Base baseBlocks = null;

        private BlockRenderer blockRenderer = null;

        private void Start()
        {
            Block[,,] blocks = generator.StartGeneration(baseBlocks);

            //  renderer
            Vector3Int worldSize = generator.GetWorldSize();
            float blockScale = generator.GetBlockScale();
            blockRenderer = gameObject.AddComponent<BlockRenderer>();
            blockRenderer.SetWorldSize(worldSize, blockScale);
            _ = blockRenderer.Generate(blocks);

            //  player
        }
    }
}