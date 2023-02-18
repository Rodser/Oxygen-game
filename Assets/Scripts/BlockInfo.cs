using System.Collections;
using UnityEngine;

namespace Rodlix
{
    [CreateAssetMenu(fileName = "Block", menuName = "MyGame/Block", order = 1)]
    public class BlockInfo : ScriptableObject
    {
        [SerializeField] private string nameBlock = "Element";
        [Space(10)]
        [SerializeField] private Block prefab = null;
        [SerializeField] private Material material = null;
        [SerializeField] private ElementType elementType = ElementType.None;
        [Space(10)]
        [SerializeField] private float temperature = 22.5f;
        [SerializeField] private float weight = 1.0f;

        public Block GetPrefab()
        {
            if (prefab == null || elementType == ElementType.None)
                return null;
            
            var outPrefab = prefab;
           // outPrefab.GetComponent<Renderer>().material = material;

            return outPrefab;
        }

        public Material GetMaterial() { return material; }
    }
}