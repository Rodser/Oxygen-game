using UnityEngine;

namespace Rodlix
{
    public class Block
    {
        private string nameBlock = "Element";
        private ElementType elementType = ElementType.None;
        private Material material = null;
        private Vector3Int positionInt;
        private int chunkNumber = 0;

        private string description = "Description";
        private float temperature = 22.5f;
        private float weight = 1.0f;

        public Block(string nameBlock, Material material, ElementType elementType, Vector3Int positionInt, int chunckNumber)
        {
            this.nameBlock = nameBlock;
            this.elementType = elementType;
            this.material = material;
            this.positionInt = positionInt;
            this.chunkNumber = chunckNumber;
        }

        public Block(ElementType elementType)
        {
            this.elementType = elementType;
        }

        public int ChunkNumber { get => chunkNumber; }
        public float Weight { get => weight; }
        public float Temperature { get => temperature; }
        public string NameBlock { get => nameBlock; }
        public ElementType ElementType { get => elementType; }
        public Material Material { get => material; }
        public Vector3Int PositionInt { get => positionInt; }
        public string Description { get => description; }
    }
}
