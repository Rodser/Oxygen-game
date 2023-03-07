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

        private float temperature = 22.5f;
        private float weight = 1.0f;
        private string description = "Description";

        public Block(string nameBlock, Material material, ElementType elementType, Vector3Int positionInt,
            int chunckNumber, float temperature, float weight, string description)
        {
            this.nameBlock = nameBlock;
            this.elementType = elementType;
            this.material = material;
            this.positionInt = positionInt;
            this.chunkNumber = chunckNumber;
            this.temperature = temperature;
            this.weight = weight;
            this.description = description;
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
