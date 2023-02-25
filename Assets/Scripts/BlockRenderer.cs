using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Rodlix
{
    public class BlockRenderer : MonoBehaviour
    {
        private Vector3Int worldSize;
        private List<ChunkRenderer> chunks;
        private float blockScale;

        public void SetWorldSize(Vector3Int worldSize, float blockScale)
        {
            this.worldSize = worldSize;
            this.blockScale = blockScale;
            chunks = new List<ChunkRenderer>();
        }

        public async Task Generate(Block[,,] blocks)
        {
            Block[,,] currentBlocks = blocks;

            for (int y = 0; y < worldSize.y; y++)
            {
                for (int x = 0; x < worldSize.x; x++)
                {
                    for (int z = 0; z < worldSize.z; z++)
                    {
                        Block block = currentBlocks[x, y, z];

                        if(block == null)
                        {
                            block = new Block(ElementType.None);
                            currentBlocks[x, y, z] = block;
                            continue;
                        }

                        if (CheckBlockinChunk(block) == false)
                        {
                            ChunkRenderer chunk = new GameObject(block.nameBlock).AddComponent<ChunkRenderer>();
                            chunk.currentType = block.elementType;
                            chunk.material = block.material;
                            chunk.currentBlocks.Add(block);

                            chunks.Add(chunk);
                        }
                    }
                }
            }
            Debug.Log("Add blocks in chunks");

            foreach (ChunkRenderer chunkRenderer in chunks)
            {
                await chunkRenderer.Generate(worldSize, currentBlocks, blockScale);

                Debug.Log("Render chunk" + chunkRenderer.currentType);
            }
        }



        private bool CheckBlockinChunk(Block block)
        {
            foreach (ChunkRenderer chunk in chunks)
            {
                if (chunk.currentType == block.elementType)
                {
                    chunk.currentBlocks.Add(block);
                    return true;
                }
            }

            return false;
        }
    }
}