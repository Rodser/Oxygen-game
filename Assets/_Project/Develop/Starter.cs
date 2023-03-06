using UnityEngine;

namespace Rodlix
{
    public class Starter : MonoBehaviour
    {
        [SerializeField] private GameObject observer = null;
        [SerializeField] private WorldGenerator generator = null;
        [SerializeField] private Base baseBlocks = null;

        private BlockRenderer blockRenderer = null;
        private GameObject[,,] gameObjects = null;

        private async void Start()
        {
            Block[,,] blocks = generator.StartGeneration(baseBlocks, observer);

            //  renderer
            Vector3Int worldSize = generator.GetSize();
            blockRenderer = gameObject.AddComponent<BlockRenderer>();
            blockRenderer.SetWorldSize(worldSize);
            await blockRenderer.Generate(blocks);

            //  player
        }
    }
}