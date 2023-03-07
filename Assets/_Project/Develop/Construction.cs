using System;
using UnityEngine;

namespace Rodlix
{
    public class Construction : MonoBehaviour
    {
        [SerializeField] private GameObject minPoint;
        [SerializeField] private GameObject maxPoint;
        [SerializeField] private GameObject player;

        public GameObject MinPoint { get => minPoint; }
        public GameObject MaxPoint { get => maxPoint; }

        internal void SetObserver(vThirdPersonCamera observer)
        {
            if (observer == null) return;

            observer.SetTarget(player.transform);
        }

    }
}