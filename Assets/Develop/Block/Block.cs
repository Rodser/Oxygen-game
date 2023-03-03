using UnityEngine;

namespace Rodlix
{
    public class Block
    {
        private int chunkNumber = 0;

        public string nameBlock = "Element";
        public ElementType elementType = ElementType.None;
        public Material material = null;
        public Vector3Int positionInt;

        public float temperature = 22.5f;
        public float weight = 1.0f;

        public int ChunkNumber { get => chunkNumber; private set => chunkNumber = value; }

        public Block(string nameBlock, Material material, ElementType elementType, int chunckNumber)
        {
            this.nameBlock = nameBlock;
            this.elementType = elementType;
            this.material = material;
            ChunkNumber = chunckNumber;
        }

        public Block(ElementType elementType)
        {
            this.elementType = elementType;
        }

        public ElementType GetBlockType()
        {
            return elementType;
        }
    }
}
