using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Rodlix
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class ChunkRenderer : MonoBehaviour
    {
        public List<Block> currentBlocks = new List<Block>();
        public ElementType currentType = ElementType.None;
        public Material material = null;

        private Mesh chunkMesh;
        private List<Vector3> vertices = null;
        private List<Vector2> uvs = null;
        private List<int> triangles = null;
        private Vector3Int size;
        private Block[,,] worldBlocks;

        private float blockScale = 1f;

        public async Task Generate(Vector3Int size, Block[,,] worldBlocks, float blockScale)
        {
            this.size = size;
            this.worldBlocks = worldBlocks;
            this.blockScale = blockScale;

            chunkMesh = new Mesh();
            vertices = new List<Vector3>();
            uvs = new List<Vector2>();
            triangles = new List<int>();

            await RegenerateMesh();
        }

        private async Task RegenerateMesh()
        {
            vertices.Clear();
            // uvs.Clear();
            triangles.Clear();

            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    for (int z = 0; z < size.z; z++)
                    {
                        DrawSidesOfBlock(x, y, z);
                    }
                }
            }

            chunkMesh.triangles = Array.Empty<int>();
            chunkMesh.vertices = vertices.ToArray();
            chunkMesh.triangles = triangles.ToArray();
            // chunkMesh.uv = uvs.ToArray();

            //chunkMesh.Optimize();
            //chunkMesh.RecalculateNormals();
            //chunkMesh.RecalculateBounds();

            GetComponent<MeshFilter>().sharedMesh = chunkMesh;
            GetComponent<MeshCollider>().sharedMesh = chunkMesh;
            GetComponent<MeshRenderer>().material = material;

            await Task.Yield();
        }

        private void DrawSidesOfBlock(int x, int y, int z)
        {
            var blockPosition = new Vector3Int(x, y, z);
            ElementType blockType = worldBlocks[x, y, z].elementType;
            if (blockType != currentType) return;

            if (GetTypeAtPosition(blockPosition + Vector3Int.right) != currentType)
            {
                GenerateRight(blockPosition);
                AddUVs(blockType, Vector3Int.right);
            }
            if (GetTypeAtPosition(blockPosition + Vector3Int.left) != currentType)
            {
                GenerateLeft(blockPosition);
                AddUVs(blockType, Vector3Int.left);
            }
            if (GetTypeAtPosition(blockPosition + Vector3Int.forward) != currentType)
            {
                GenerateFront(blockPosition);
                AddUVs(blockType, Vector3Int.forward);
            }
            if (GetTypeAtPosition(blockPosition + Vector3Int.back) != currentType)
            {
                GenerateBack(blockPosition);
                AddUVs(blockType, Vector3Int.back);
            }
            if (GetTypeAtPosition(blockPosition + Vector3Int.up) != currentType)
            {
                GenerateTop(blockPosition);
                AddUVs(blockType, Vector3Int.up);
            }
            if (GetTypeAtPosition(blockPosition + Vector3Int.down) != currentType)
            {
                GenerateDown(blockPosition);
                AddUVs(blockType, Vector3Int.down);
            }
        }

        private ElementType GetTypeAtPosition(Vector3Int position)
        {
            if (position.x >= 0 && position.x < size.x &&
                position.y >= 0 && position.y < size.y &&
                position.z >= 0 && position.z < size.z)
            {
                return worldBlocks[position.x, position.y, position.z].elementType;
            }
            else
            {
                return ElementType.None;
            }
        }

        private void GenerateRight(Vector3Int position)
        {
            vertices.Add((new Vector3(1, 0, 0) + position) * blockScale);
            vertices.Add((new Vector3(1, 1, 0) + position) * blockScale);
            vertices.Add((new Vector3(1, 0, 1) + position) * blockScale);
            vertices.Add((new Vector3(1, 1, 1) + position) * blockScale);

            AddLastVerticesSquare();
        }

        private void GenerateLeft(Vector3Int position)
        {
            vertices.Add((new Vector3(0, 0, 0) + position) * blockScale);
            vertices.Add((new Vector3(0, 0, 1) + position) * blockScale);
            vertices.Add((new Vector3(0, 1, 0) + position) * blockScale);
            vertices.Add((new Vector3(0, 1, 1) + position) * blockScale);

            AddLastVerticesSquare();
        }

        private void GenerateFront(Vector3Int position)
        {
            vertices.Add((new Vector3(0, 0, 1) + position) * blockScale);
            vertices.Add((new Vector3(1, 0, 1) + position) * blockScale);
            vertices.Add((new Vector3(0, 1, 1) + position) * blockScale);
            vertices.Add((new Vector3(1, 1, 1) + position) * blockScale);

            AddLastVerticesSquare();
        }

        private void GenerateBack(Vector3Int position)
        {
            vertices.Add((new Vector3(0, 0, 0) + position) * blockScale);
            vertices.Add((new Vector3(0, 1, 0) + position) * blockScale);
            vertices.Add((new Vector3(1, 0, 0) + position) * blockScale);
            vertices.Add((new Vector3(1, 1, 0) + position) * blockScale);

            AddLastVerticesSquare();
        }

        private void GenerateTop(Vector3Int position)
        {
            vertices.Add((new Vector3(0, 1, 0) + position) * blockScale);
            vertices.Add((new Vector3(0, 1, 1) + position) * blockScale);
            vertices.Add((new Vector3(1, 1, 0) + position) * blockScale);
            vertices.Add((new Vector3(1, 1, 1) + position) * blockScale);

            AddLastVerticesSquare();
        }

        private void GenerateDown(Vector3Int position)
        {
            vertices.Add((new Vector3(0, 0, 0) + position) * blockScale);
            vertices.Add((new Vector3(1, 0, 0) + position) * blockScale);
            vertices.Add((new Vector3(0, 0, 1) + position) * blockScale);
            vertices.Add((new Vector3(1, 0, 1) + position) * blockScale);

            AddLastVerticesSquare();
        }

        private void AddLastVerticesSquare()
        {
            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 2);

            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 1);
            triangles.Add(vertices.Count - 2);
        }

        private void AddUVs(ElementType blockType, Vector3Int down)
        {
            return;
        }

    }
}