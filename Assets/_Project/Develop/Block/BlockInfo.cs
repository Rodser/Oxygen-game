using System.Collections;
using UnityEngine;

namespace Rodlix
{
    [CreateAssetMenu(fileName = "Block", menuName = "MyGame/Block", order = 3)]
    public class BlockInfo : ScriptableObject
    {
        [SerializeField] private string nameBlock = "Element";
        [Space(10)]
        [SerializeField] private Material material = null;
        [SerializeField] private ElementType elementType = ElementType.None;
        [Space(10)]
        [SerializeField] private float temperature = 22.5f;
        [SerializeField] private float weight = 1.0f;

        public Block GetBlock(int chunckNumber, Vector3Int positionInt)
        {            
            Block block = new Block(nameBlock, material, elementType, positionInt, chunckNumber);

            return block;
        }

        public ElementType GetBlockType()
        {
            return elementType;
        }
    }
}