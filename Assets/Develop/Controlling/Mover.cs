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
            Vector2 inputMove = inputActions.Camera.Move.ReadValue<Vector2>();
            transform.Translate(inputMove.x, 0, inputMove.y);
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