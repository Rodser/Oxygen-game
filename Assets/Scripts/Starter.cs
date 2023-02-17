using System;
using UnityEngine;

namespace Rodlix
{
    public class Starter : MonoBehaviour
    {
        [SerializeField] private byte size = 10;
        [SerializeField] private BlockGenerator[] generators = null;

        private Block[,,] blocks = null;

        private void Start()
        {
            StartGeneration();
            //  renderer
            //  player
        }

        private void StartGeneration()
        {
            foreach (var generator in generators)
            {
                generator.GenerateBlocks(blocks, size);
            }
        }
    }
}