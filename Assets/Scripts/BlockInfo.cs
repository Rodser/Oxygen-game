using System.Collections;
using UnityEngine;

namespace Rodlix
{
    [CreateAssetMenu(fileName = "Block", menuName = "MyGame/Block", order = 3)]
    public class BlockInfo : ScriptableObject
    {
        [SerializeField] private string nameBlock = "Element";
        [Space(10)]
        [SerializeField] private GameObject prefab = null;
        [SerializeField] private Material material = null;
        [SerializeField] private ElementType elementType = ElementType.None;
        [Space(10)]
        [SerializeField] private float temperature = 22.5f;
        [SerializeField] private float weight = 1.0f;

        public Block GetBlock()
        {
            if (prefab == null || elementType == ElementType.None)
                return null;
            
            Block block = new Block(prefab, nameBlock, material, elementType, temperature, weight);

            return block;
        }

        public ElementType GetBlockType()
        {
            return elementType;
        }
    }
}