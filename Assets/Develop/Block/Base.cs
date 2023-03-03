using System;
using UnityEngine;

namespace Rodlix
{
    [CreateAssetMenu(fileName = "Base", menuName = "MyGame/Base", order = 1)]
    public class Base : ScriptableObject
    {
        [SerializeField] private BlockInfo[] Resources;

        internal BlockInfo FindBlock(ElementType type)
        {
            foreach (BlockInfo info in Resources)
            {
                if(info.GetBlockType() == type)
                {
                    return info;
                }
            }
            return null;
        }
    }
}