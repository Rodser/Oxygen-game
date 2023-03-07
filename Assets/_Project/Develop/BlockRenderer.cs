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

        public void SetWorldSize(Vector3Int worldSize, float scale)
        {
            this.worldSize = worldSize;
            this.blockScale = scale;
            chunks = new List<ChunkRenderer>();
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
                            ChunkRenderer chunk = new GameObject(block.NameBlock + block.ChunkNumber).AddComponent<ChunkRenderer>();
                            chunk.currentType = block.ElementType;
                            chunk.material = block.Material;
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
                await chunkRenderer.Generate(worldSize, blocks, blockScale);

                Debug.Log("Render chunk" + chunkRenderer.currentType);
            }
        }

        public async Task DestroyBlock(Block block)
        {
            foreach (ChunkRenderer chunk in chunks)
            {
                await chunk.DestroyBlock(block);
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