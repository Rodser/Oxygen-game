using UnityEngine;

namespace Rodlix
{
    public class Block
    {
        public string nameBlock = "Element";
        public GameObject prefab = null;
        public ElementType elementType = ElementType.None;
        public Material material = null;
        public float temperature = 22.5f;
        public float weight = 1.0f;


        public Block(GameObject prefab, string nameBlock, Material material, ElementType elementType, float temperature, float weight)
        {
            this.prefab = prefab;
            this.nameBlock = nameBlock;
            this.elementType = elementType;
            this.material = material;
            this.temperature = temperature;
            this.weight = weight;
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
