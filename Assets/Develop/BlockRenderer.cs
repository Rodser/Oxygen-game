using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Rodlix
{
    public class BlockRenderer : MonoBehaviour
    {
        private Vector3Int worldSize;
        private List<ChunkRenderer> chunks;

        public void SetWorldSize(Vector3Int worldSize)
        {
            this.worldSize = worldSize;
            chunks= new List<ChunkRenderer>();
        }

        public async Task Generate(Block[,,] blocks)
        {

            for (int y = 0; y < worldSize.y; y++)
            {
                for (int x = 0; x < worldSize.x; x++)
                {
                    for (int z = 0; z < worldSize.z; z++)
                    {
                        Block block = blocks[x, y, z];

                        if(block == null)
                        {
                            block = new Block(ElementType.None);
                            blocks[x, y, z] = block;
                            continue;
                        }

                        if (CheckBlockinChunk(block) == false)
                        {
                            ChunkRenderer chunk = new GameObject(block.nameBlock + block.ChunkNumber).AddComponent<ChunkRenderer>();
                            chunk.currentType = block.elementType;
                            chunk.material = block.material;
                            chunk.number = block.ChunkNumber;
                            chunk.currentBlocks.Add(block);

                            chunks.Add(chunk);
                        }
                    }
                }
            }
            Debug.Log("Add blocks in chunks");

            foreach (ChunkRenderer chunkRenderer in chunks)
            {
                await chunkRenderer.Generate(worldSize, blocks);

                Debug.Log("Render chunk" + chunkRenderer.currentType);
            }
        }

        private bool CheckBlockinChunk(Block block)
        {
            foreach (ChunkRenderer chunk in chunks)
            {
                if (chunk.number == block.ChunkNumber)
                {
                    chunk.currentBlocks.Add(block);
                    return true;
                }
            }

            return false;
        }
    }
}