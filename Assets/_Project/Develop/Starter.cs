using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Rodlix
{
    public class Starter : MonoBehaviour
    {
        [SerializeField] private vThirdPersonCamera observer = null;
        [SerializeField] private WorldGenerator generator = null;
        [SerializeField] private Base baseBlocks = null;
        [SerializeField] private float blockScale;
        [Space(10)]
        [SerializeField] private InfoOfBlockUI infoOFBlock;

        private Vector3Int worldSize;
        private BlockRenderer blockRenderer = null;
        private Block[,,] blocks = null;

        private async void Start()
        {
            blocks = generator.StartGeneration(baseBlocks, observer, blockScale);

            CrossHair();
            //  renderer
            worldSize = generator.GetSize();

            blockRenderer = gameObject.AddComponent<BlockRenderer>();
            blockRenderer.SetWorldSize(worldSize, blockScale);
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
        }

        private async void CheckInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = observer.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 blockCenter = hit.point - hit.normal * (0.5f * blockScale);
                    Vector3Int blockWorldPosition = Vector3Int.FloorToInt(blockCenter / blockScale);
                    
                    Block selectedBlock = blocks[blockWorldPosition.x, blockWorldPosition.y, blockWorldPosition.z];
                                        
                    if (selectedBlock != null)
                    {
                        await blockRenderer.DestroyBlock(selectedBlock);
                    }                 
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = observer.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 blockCenter = hit.point - hit.normal * (0.5f * blockScale);
                    Vector3Int blockWorldPosition = Vector3Int.FloorToInt(blockCenter / blockScale);

                    Block selectedBlock = blocks[blockWorldPosition.x, blockWorldPosition.y, blockWorldPosition.z];

                    if (selectedBlock != null)
                    {
                        infoOFBlock.InfoPanel.gameObject.SetActive(true);
                        infoOFBlock.Name.text = selectedBlock.NameBlock;
                        infoOFBlock.Description.text = selectedBlock.Description;
                        infoOFBlock.Temperature.text = $"t {selectedBlock.Temperature}\u00B0C";
                        infoOFBlock.Weight.text = $"{selectedBlock.Weight}kg"; 
                    }
                }
            }

            if(Input.GetMouseButtonUp(1))
            {
                infoOFBlock.InfoPanel.gameObject.SetActive(false);
            }
        }
    }
}