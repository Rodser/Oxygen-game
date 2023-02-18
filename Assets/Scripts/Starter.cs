using UnityEngine;

namespace Rodlix
{
    public class Starter : MonoBehaviour
    {
        [SerializeField] private WorldGenerator generator = null;

        private Block[,,] blocks = null;

        private void Start()
        {
            blocks = generator.StartGeneration();


            //  renderer
            //  player
        }

    }
}