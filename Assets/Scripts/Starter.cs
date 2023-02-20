using UnityEngine;

namespace Rodlix
{
    public class Starter : MonoBehaviour
    {
        [SerializeField] private WorldGenerator generator = null;
        [SerializeField] private Base baseBlocks = null;

        private GameObject[,,] gameObjects = null;

        private void Start()
        {
            gameObjects = generator.StartGeneration(baseBlocks);


            //  renderer
            //  player
        }

    }
}