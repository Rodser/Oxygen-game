using UnityEngine;

namespace Rodlix
{
    public class Construction : MonoBehaviour
    {
        public GameObject minPoint;
        public GameObject maxPoint;
        public GameObject camPoint;

        private void Start()
        {
            Camera.main.transform.position = camPoint.transform.position;
        }
    }
}