using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace M7459.Controllers
{
    /// <summary>
    /// Class <c>CameraController</c> allows the player to control the camera.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        #region Input and Camera
        
            /// <value>Property <c>_cameraActions</c> represents the camera control actions.</value>
            private CameraControlActions _cameraActions;
            
            /// <value>Property <c>_movement</c> represents the movement action.</value>
            private InputAction _movement;
            
            /// <value>Property <c>_camera</c> represents the camera.</value>
            private Camera _camera;
            
            /// <value>Property <c>_cameraTransform</c> represents the camera transform.</value>
            private Transform _cameraTransform;
            
            /// <value>Property <c>_virtualCamera</c> represents the virtual camera.</value>
            private CinemachineVirtualCamera _virtualCamera;
        
        #endregion
        
        #region Movement
        
            /// <value>Property <c>leftLimit</c> represents the left limit of the camera.</value>
            [Header("Limits")]
            [SerializeField]
            private float leftLimit = -30f;
            
            /// <value>Property <c>rightLimit</c> represents the right limit of the camera.</value>
            [SerializeField]
            private float rightLimit = 30f;
            
            /// <value>Property <c>topLimit</c> represents the top limit of the camera.</value>
            [SerializeField]
            private float topLimit = 20f;
            
            /// <value>Property <c>bottomLimit</c> represents the bottom limit of the camera.</value>
            [SerializeField]
            private float bottomLimit = -20f;

            /// <value>Property <c>maxSpeed</c> represents the maximum speed of the camera.</value>
            [Header("Movement")]
            [SerializeField]
            private float maxSpeed = 10f;
            
            /// <value>Property <c>_speed</c> represents the current speed of the camera.</value>
            private float _speed;
            
            /// <value>Property <c>acceleration</c> represents the acceleration of the camera.</value>
            [SerializeField]
            private float acceleration = 25f;
            
            /// <value>Property <c>damping</c> represents the damping of the camera.</value>
            [SerializeField]
            private float damping = 5f;
            
            /// <value>Property <c>_horizontalVelocity</c> represents the current horizontal velocity of the camera.</value>
            private Vector3 _horizontalVelocity;

        #endregion
        
        #region Zoom
        
            /// <value>Property <c>stepSize</c> represents the step size of the camera.</value>
            [Header("Zoom")]
            [SerializeField]
            private float stepSize = 2f;
            
            /// <value>Property <c>zoomDampening</c> represents the zoom dampening of the camera.</value>
            [SerializeField]
            private float zoomDampening = 5f;
            
            /// <value>Property <c>minHeight</c> represents the minimum height of the camera.</value>
            [SerializeField]
            private float minHeight = 2f;
            
            /// <value>Property <c>maxHeight</c> represents the maximum height of the camera.</value>
            [SerializeField]
            private float maxHeight = 18f;
            
            /// <value>Property <c>_zoomHeight</c> represents the current zoom height of the camera.</value>
            private float _zoomHeight;
        
        #endregion
        
        #region Edge Movement
        
            /// <value>Property <c>edgeTolerance</c> represents the edge tolerance of the camera.</value>
            [Header("Edge Movement")]
            [SerializeField]
            [Range(0f,0.1f)]
            private float edgeTolerance = 0.05f;
        
        #endregion
        
        #region Focus
        
            /// <value>Property <c>focus</c> represents the focus of the camera.</value>
            private bool _focusActive;
            
        #endregion
        
        #region Reference Values
        
            /// <value>Property <c>_targetPosition</c> represents the target position of the camera.</value>
            private Vector3 _targetPosition;
            
            /// <value>Property <c>_lastPosition</c> represents the last position of the camera.</value>
            private Vector3 _lastPosition;
            
        #endregion
    
        /// <summary>
        /// Method <c>Awake</c> is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            // Get the camera actions and transform
            _cameraActions = new CameraControlActions();
            _camera ??= GetComponentInChildren<Camera>();
            _cameraTransform ??= _camera.transform;
            _virtualCamera ??= GetComponentInChildren<CinemachineVirtualCamera>();
        }

        /// <summary>
        /// Method <c>OnEnable</c> is called when the object becomes enabled and active.
        /// </summary>
        private void OnEnable()
        {
            // Set the initial value for the last position, the movement and the zoom height
            _lastPosition = transform.position;
            _movement = _cameraActions.Camera.Movement;
            _zoomHeight = _virtualCamera.m_Lens.OrthographicSize;
            
            // Enable the camera actions
            _cameraActions.Enable();
            
            // Subscribe to the zoom event
            _cameraActions.Camera.ZoomCamera.performed += ZoomCamera;
            
            // Subscribe to the focus event
            _cameraActions.Camera.FocusCamera.performed += _ => ToggleCameraFocus();
        }

        /// <summary>
        /// Method <c>OnDisable</c> is called when the behaviour becomes disabled.
        /// </summary>
        private void OnDisable()
        {
            // Disable the camera actions
            _cameraActions.Disable();
            
            // Unsubscribe from the zoom event
            _cameraActions.Camera.ZoomCamera.performed -= ZoomCamera;
        }

        /// <summary>
        /// Method <c>Update</c> is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            // Check if the camera is focused
            if (_focusActive)
                return;

            // Get the keyboard movement
            GetKeyboardMovement();
            
            // Check if the mouse is at the screen edge
            CheckMouseAtScreenEdge();

            // Update the horizontal velocity
            UpdateVelocity();
            
            // Update the base position
            UpdateBasePosition();
            
            // Update the camera size
            UpdateCameraSize();
        }
        
        /// <summary>
        /// Method <c>UpdateVelocity</c> updates the horizontal velocity of the camera.
        /// </summary>
        private void UpdateVelocity()
        {
            var position = transform.position;
            _horizontalVelocity = (position - _lastPosition) / Time.deltaTime;
            _horizontalVelocity.y = 0f;
            _lastPosition = position;
        }

        /// <summary>
        /// Method <c>GetKeyboardMovement</c> gets the keyboard movement of the camera.
        /// </summary>
        private void GetKeyboardMovement()
        {
            var inputValue = _movement.ReadValue<Vector2>().x * GetCameraRight()
                                + _movement.ReadValue<Vector2>().y * GetCameraForward();
            inputValue = inputValue.normalized;
            if (inputValue.sqrMagnitude > 0.1f)
            {
                _targetPosition += inputValue;
            }
        }
        
        /// <summary>
        /// Method <c>GetCameraRight</c> gets the right position of the camera.
        /// </summary>
        private Vector3 GetCameraRight()
        {
            var right = _cameraTransform.right;
            right.y = 0f;
            return right;
        }
        
        /// <summary>
        /// Method <c>GetCameraForward</c> gets the forward position of the camera.
        /// </summary>
        private Vector3 GetCameraForward()
        {
            var forward = _cameraTransform.forward;
            forward.y = 0f;
            return forward;
        }
        
        /// <summary>
        /// Method <c>UpdateBasePosition</c> updates the base position of the base rig.
        /// </summary>
        private void UpdateBasePosition()
        {
            // Get the new position
            var newPosition = transform.position;
            if (_targetPosition.sqrMagnitude > 0.1f)
            {
                _speed = Mathf.Lerp(_speed, maxSpeed, acceleration * Time.deltaTime);
                newPosition += _targetPosition * (_speed * Time.deltaTime);
            }
            else
            {
                _horizontalVelocity = Vector3.Lerp(_horizontalVelocity, Vector3.zero, damping * Time.deltaTime);
                newPosition += _horizontalVelocity * Time.deltaTime;
            }

            // Clamp the position to the limits
            transform.position = new Vector3(
                Mathf.Clamp(newPosition.x, leftLimit, rightLimit),
                newPosition.y,
                Mathf.Clamp(newPosition.z, bottomLimit, topLimit));
            
            // Reset the target position
            _targetPosition = Vector3.zero;
        }
        
        /// <summary>
        /// Method <c>ZoomCamera</c> zooms the camera.
        /// </summary>
        private void ZoomCamera(InputAction.CallbackContext inputValue)
        {
            var value = -inputValue.ReadValue<Vector2>().y / 100f;
            if (Mathf.Abs(value) > 0.1f)
            {
                _zoomHeight = Mathf.Clamp(_virtualCamera.m_Lens.OrthographicSize + value * stepSize, minHeight, maxHeight);
            }
        }
        
        /// <summary>
        /// Method <c>UpdateCameraSize</c> updates the camera size.
        /// </summary>
        private void UpdateCameraSize()
        {
            _virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(_virtualCamera.m_Lens.OrthographicSize, _zoomHeight, zoomDampening * Time.deltaTime);
        }
        
        /// <summary>
        /// Method <c>CheckMouseAtScreenEdge</c> checks if the mouse is at the screen edge.
        /// </summary>
        private void CheckMouseAtScreenEdge()
        {
            // Get the mouse position and the move direction
            var mousePosition = Mouse.current.position.ReadValue();
            var moveDirection = Vector3.zero;

            // Check the left and right edges
            if (mousePosition.x < Screen.width * edgeTolerance)
                moveDirection += -GetCameraRight();
            else if (mousePosition.x > Screen.width * (1f - edgeTolerance))
                moveDirection += GetCameraRight();
            
            // Check the top and bottom edges
            if (mousePosition.y < Screen.height * edgeTolerance)
                moveDirection += -GetCameraForward();
            else if (mousePosition.y > Screen.height * (1f - edgeTolerance))
                moveDirection += GetCameraForward();
            
            // Update the target position
            _targetPosition += moveDirection;
        }
        
        /// <summary>
        /// Method <c>ToggleCameraFocus</c> toggles the camera focus.
        /// </summary>
        private void ToggleCameraFocus() {
            var cinemachineTargetGroup = _virtualCamera.GetComponent<CinemachineTargetGroup>();
            if (cinemachineTargetGroup.isActiveAndEnabled)
            {
                _focusActive = false;
                cinemachineTargetGroup.enabled = false;
                _targetPosition = Vector3.zero;
                _virtualCamera.transform.position = _targetPosition;
                _zoomHeight = maxHeight;
                _virtualCamera.m_Lens.OrthographicSize = _zoomHeight;
            }
            else
            {
                _focusActive = true;
                cinemachineTargetGroup.enabled = true;
                _virtualCamera.m_Lens.OrthographicSize = 6f;
            }
        }
    }
}
