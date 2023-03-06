using System;
using UnityEngine;

namespace Rodlix
{
    public class Construction : MonoBehaviour
    {
        [SerializeField] private GameObject minPoint;
        [SerializeField] private GameObject maxPoint;
        [SerializeField] private GameObject camPoint;

        public GameObject MinPoint { get => minPoint; }
        public GameObject MaxPoint { get => maxPoint; }
        public GameObject CamPoint { get => camPoint; }

        internal void SetObserver(GameObject observer)
        {
            if (observer == null) return;

            observer.transform.position = camPoint.transform.position;
        }

    }
}