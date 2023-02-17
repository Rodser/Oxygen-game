using UnityEngine;

namespace Rodlix
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private string nameBlock = "Element";
        [Space(10)]
        [SerializeField] private ElementType elementType = ElementType.None;
        [Space(10)]
        [SerializeField] private float temperature = 22.5f;
        [SerializeField] private float weight = 1.0f;

        public void SetParameters()
        {

        }

        public ElementType GetBlockType()
        {
            return elementType;
        }
    }
}
