using UnityEngine;

namespace Rodlix
{
    public class Block
    {
        public string nameBlock = "Element";
        public ElementType elementType = ElementType.None;
        public Material material = null;
        public Vector3Int positionInt;

        public float temperature = 22.5f;
        public float weight = 1.0f;


        public Block(string nameBlock, Material material, ElementType elementType)
        {
            this.nameBlock = nameBlock;
            this.elementType = elementType;
            this.material = material;
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
