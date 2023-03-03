using UnityEditor.Search;
using UnityEngine;

namespace Rodlix
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotateSpeed;
        
        private CameraControl inputActions;

        private void Awake()
        {
            inputActions = new CameraControl();
            inputActions.Camera.Enable();
        }

        private void Update()
        {
            Move();
            LookAt();
        }

        private void Move()
        {
            Vector3 inputMove = inputActions.Camera.Move.ReadValue<Vector3>();
            Vector3 moveDirection = new Vector3(inputMove.x, inputMove.y, inputMove.z);
            transform.position += moveSpeed * Time.deltaTime * moveDirection;
        }

        private void LookAt()
        {
            float X = inputActions.Camera.Look.ReadValue<Vector2>().x * rotateSpeed * Time.deltaTime;
            float Y = -inputActions.Camera.Look.ReadValue<Vector2>().y * rotateSpeed * Time.deltaTime;
            float eulerX = (transform.rotation.eulerAngles.x + Y) % 360;
            float eulerY = (transform.rotation.eulerAngles.y + X) % 360;
            transform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
        }
    }
}