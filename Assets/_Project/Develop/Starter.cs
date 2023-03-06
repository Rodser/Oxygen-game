using UnityEngine;
using UnityEngine.UI;

namespace Rodlix
{
    public class Starter : MonoBehaviour
    {
        [SerializeField] private vThirdPersonCamera observer = null;
        [SerializeField] private WorldGenerator generator = null;
        [SerializeField] private Base baseBlocks = null;
        [SerializeField] private Image crosshairObject;

        public Sprite crosshairImage;
        public Color crosshairColor = Color.white;

        private Vector3Int worldSize;
        private BlockRenderer blockRenderer = null;
        private Block[,,] blocks = null;

        private async void Start()
        {
            blocks = generator.StartGeneration(baseBlocks, observer);

            CrossHair();
            //  renderer
            worldSize = generator.GetSize();

            blockRenderer = gameObject.AddComponent<BlockRenderer>();
            blockRenderer.SetWorldSize(worldSize);
            await blockRenderer.Generate(blocks);

            //  player
        }

        private void Update()
        {
            CheckInput();
        }

        private void CrossHair()
        {
            Cursor.lockState = CursorLockMode.Locked;
            crosshairObject.sprite = crosshairImage;
            crosshairObject.color = crosshairColor;
        }

        private async void CheckInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = observer.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 blockCenter = hit.point - hit.normal * (0.5f); // * blockScale
                    Vector3Int blockWorldPosition = Vector3Int.FloorToInt(blockCenter);// * blockScale
                    
                    Block selectedBlock = blocks[blockWorldPosition.x, blockWorldPosition.y, blockWorldPosition.z];
                                        
                    if (selectedBlock != null)
                    {
                        await blockRenderer.DestroyBlock(selectedBlock);
                    }                 
                }
            }
        }
    }
}