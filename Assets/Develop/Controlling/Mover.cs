using UnityEngine;

namespace Rodlix
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float sensitivity = 5.0f;
        [SerializeField] private float smoothing = 2.0f;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotateSpeed;

        private Vector2 mouseLook;
        private Vector2 smoothV;
        private CameraControl inputActions;
        private GameObject character;

        private void Awake()
        {
            inputActions = new CameraControl();
            inputActions.Camera.Enable();
        }

        void Start()
        {
            character = transform.parent.gameObject;
        }

        private void FixedUpdate()
        {
            Move();
            LookAt();
        }

        private void Move()
        {
            Vector2 inputMove = inputActions.Camera.Move.ReadValue<Vector2>();
            character.transform.Translate(inputMove.x, 0, inputMove.y);
        }

        private void LookAt()
        {
            Vector2 inputLook = new Vector2(inputActions.Camera.Look.ReadValue<Vector2>().x, inputActions.Camera.Look.ReadValue<Vector2>().y);

            inputLook = Vector2.Scale(inputLook, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            // the interpolated float result between the two float values
            smoothV.x = Mathf.Lerp(smoothV.x, inputLook.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, inputLook.y, 1f / smoothing);
            // incrementally add to the camera look
            mouseLook += smoothV;

            // vector3.right means the x-axis
            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        }
    }
}