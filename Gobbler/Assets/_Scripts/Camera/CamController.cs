using UnityEngine;
using Sirenix.OdinInspector;

namespace RTS_Camera
{
    //[RequireComponent(typeof(Camera))]
    public class CamController : MonoBehaviour
    {

        #region Variables

        public bool useFixedUpdate = false; //use FixedUpdate() or Update()

        #region Foldouts

        #if UNITY_EDITOR
        public int lastTab = 0;

        public bool movementSettingsFoldout;
        public bool zoomingSettingsFoldout;
        public bool mapLimitSettingsFoldout;
        public bool inputSettingsFoldout;
        #endif

        #endregion

        #region Data

        private Transform m_Trans; //camera transform
        private Vector3 fitted_WorldPos;
        private Vector3 rel_WorldPos;

        public LayerMask layerMask;

        public float dTime;

        #endregion

        #region Movement

        public float keyboardSpeed = 20f; //keyboard speed
        public float screenEdgeScrollSpeed = 10f; //screen edge movement speed
        public float followingSpeed = 5f; //speed when following a target
        public float panningSpeed = 40f;

        #endregion

        #region Zooming

        //public

        public float keyboardZoomingSensitivity = 2f;
        public float scrollWheelZoomingSensitivity = 25f;

        private float zoomPos = 0; //value in range (0, 1) used as t in Matf.Lerp

        #endregion

        #region MapLimits

        public bool limitMovement = false;

        public float minDistance = 10, maxDistance = 100;

        public float limitX = 100f, limitY = 100f;

        #endregion

        #region Following

        public bool following = false;

        public Transform target; //target to follow

        /*
        /// <summary>
        /// are we following target
        /// </summary>
        public bool FollowingTarget
        {
            get
            {
                return targetFollow != null;
            }
        }
        */

        #endregion

        #region Input

        public bool debug = true;

        #region Moving

            #region screenEdge

            public bool useScreenEdgeInput = true;

            public float screenEdgeBorder = 2f;

            private Vector3 xDirection = new Vector3(0f, 0f, 0f);
            private Vector3 yDirection = new Vector3(0f, 0f, 0f);

            #endregion

            #region keyboard

            public bool useKeyboardInput = true;
            public string horizontalAxis = "Horizontal", verticalAxis = "Vertical";
            public KeyCode horizontalLeft = KeyCode.A, horizontalRight = KeyCode.D;
            public KeyCode verticalUp = KeyCode.W, verticalDown = KeyCode.S;

            #endregion
            
            #region Panning

            public bool useScrollPanning = true;

            public bool usePanning = true;
            public KeyCode panningKey = KeyCode.Mouse2;

            #endregion

        #endregion

        #region Zooming

        public bool useKeyboardZooming = true;
        public KeyCode zoomInKey = KeyCode.R;
        public KeyCode zoomOutKey = KeyCode.F;

        public bool useScrollwheelZooming = true;
        public bool invertScrollwheel = false;
        public string zoomingAxis = "Mouse ScrollWheel";

        #endregion

        public bool moveCameraTowardsMouse = false;

        private Vector2 KeyboardInput
        {
            get { return useKeyboardInput ? new Vector2(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis)) : Vector2.zero; }
        }

        private Vector2 MouseInput
        {
            get { return Input.mousePosition; }
        }

        private Vector2 MouseAxis
        {
            get
            {
                return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            }
        }

        private float ScrollWheel
        {
            get { return Input.GetAxis(zoomingAxis); }
        }

        public Vector3 collisionChecker;

        private int ZoomDirection
        {
            get
            {
                bool zoomIn = Input.GetKey(zoomInKey);
                bool zoomOut = Input.GetKey(zoomOutKey);
                if (zoomIn && zoomOut)
                    return 0;
                else if (!zoomIn && zoomOut)
                    return 1;
                else if (zoomIn && !zoomOut)
                    return -1;
                else
                    return 0;
            }
        }

        /// <summary> 
        /// gets position of mouse in worldspace.
        /// </summary>
        private Vector3 MouseWorldPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(MouseInput);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, layerMask.value))
            {
                return (hit.point);
            }

            return Vector3.zero;
        }

        /*
            private int RotationDirection
            {
                get
                {
                    bool rotateRight = Input.GetKey(rotateRightKey);
                    bool rotateLeft = Input.GetKey(rotateLeftKey);
                    if (rotateLeft && rotateRight)
                    {
                        return 0;
                    }
                    else if (rotateLeft && !rotateRight)
                    {
                        return -1;
                    }
                    else if (!rotateLeft && rotateRight)
                    {
                        return 1;
                    }
                    else
                    {
                            return 0;
                    }
                }
            }
            */

        #endregion

        #endregion

        #region Unity_Methods

        private void Start()
        {
            m_Trans = transform;
        }

        private void Update()
        {
            if (!useFixedUpdate)
            {
                dTime = Time.deltaTime;
                CameraUpdate();
            }
        }

        private void FixedUpdate()
        {
            if (useFixedUpdate)
            {
                dTime = Time.fixedDeltaTime;
                CameraUpdate();
            }
        }

        #endregion

        #region CamController_Methods

        /// <summary>
        /// update camera movement and rotation
        /// </summary>
        private void CameraUpdate()
        {
            if (following)
            {
                //FollowTarget();
            }
            else
            {
                Move();
            }

            Zoom();
            LimitPosition();
        }

        /// <summary>
        /// move camera with key, axis or with screen edge
        /// </summary>
        private void Move()
        {
            if (useKeyboardInput)
            {
                Vector3 desiredMove = new Vector3(KeyboardInput.x, KeyboardInput.y, 0);

                desiredMove *= keyboardSpeed;
                desiredMove *= dTime;

                desiredMove = m_Trans.InverseTransformDirection(desiredMove);

                m_Trans.Translate(desiredMove, Space.Self);
            }

            if (useScreenEdgeInput)
            {
                // determine directions the camera should move in when scrolling
                yDirection.x = transform.up.x;
                yDirection.y = 0;
                yDirection.z = transform.up.z;
                yDirection = yDirection.normalized;

                xDirection.x = transform.right.x;
                xDirection.y = 0;
                xDirection.z = transform.right.z;
                xDirection = xDirection.normalized;

                Vector3 targetMove = new Vector3();

                if (MouseInput.x >= Screen.width - screenEdgeBorder)
                {
                    targetMove.x = targetMove.x + xDirection.x;
                    targetMove.z = targetMove.z + xDirection.z;
                }
                if (MouseInput.x <= screenEdgeBorder)
                {
                    targetMove.x = targetMove.x - xDirection.x;
                    targetMove.z = targetMove.z - xDirection.z;
                }

                if (MouseInput.y >= Screen.height - screenEdgeBorder)
                {
                    targetMove.x = targetMove.x + yDirection.x;
                    targetMove.z = targetMove.z + yDirection.z;
                }
                if (MouseInput.y <= screenEdgeBorder)
                {
                    targetMove.x = targetMove.x - yDirection.x;
                    targetMove.z = targetMove.z - yDirection.z;
                }

                targetMove *= screenEdgeScrollSpeed;
                targetMove *= dTime;

                targetMove = m_Trans.InverseTransformDirection(targetMove);

                m_Trans.Translate(targetMove, Space.Self);
            }

            if (usePanning && Input.GetKey(panningKey) && MouseAxis != Vector2.zero) //Uses 
            {
                Vector3 desiredMove = new Vector3(-MouseAxis.x, -MouseAxis.y, 0);

                desiredMove *= panningSpeed;
                desiredMove *= dTime;

                desiredMove = m_Trans.InverseTransformDirection(desiredMove);

                m_Trans.Translate(desiredMove, Space.Self);
            }
        }

        /// <summary>
        /// Zoom using keys or axis
        /// </summary>
        private void Zoom()
        {
            if (useKeyboardZooming)
            {
                zoomPos += ZoomDirection * dTime * keyboardZoomingSensitivity;
            }

            if (useScrollwheelZooming)
            {
                zoomPos += ScrollWheel * dTime * -scrollWheelZoomingSensitivity;
            }

            zoomPos = Mathf.Clamp01(zoomPos);

            float targetZoom = Mathf.Lerp(minDistance, maxDistance, zoomPos);

            float difference = 0;

            m_Trans.position = new Vector3(m_Trans.position.x, m_Trans.position.y, targetZoom + difference);
        }

        /// <summary>
        /// Limit camera position
        /// </summary>
        private void LimitPosition()
        {
            if (!limitMovement)
            {
                return;
            }

            m_Trans.position = new Vector3(Mathf.Clamp(m_Trans.position.x, -limitX, limitX),
                m_Trans.position.y,
                    Mathf.Clamp(m_Trans.position.z, -limitY, limitY));
        }

        #region Targeting

        /// <summary>
        /// set the target
        /// </summary>
        /// <param name="_target"></param>
        public void SetTarget(Transform _target)
        {
            target = _target;
        }

        /// <summary>
        /// reset the target (target is set to null)
        /// </summary>
        public void ResetTarget()
        {
            target = null;
        }

        #endregion

        #endregion

    }
}