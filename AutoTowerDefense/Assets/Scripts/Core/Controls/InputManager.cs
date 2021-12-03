using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
namespace Core.Controls
{
    public class InputManager : MonoBehaviour
    {
        public float leftBorder;
        public float rightBorder;
        public float upperBorder;
        public float lowerBorder;
        private Rect _trueViewPort;
        private Rect _innerRect;
        private Camera _mainCamera;
        private Vector3 _startDrag;
        private float _initialZoomDistance;

        private void Awake()
        {
            EnhancedTouchSupport.Enable();
            _mainCamera = Camera.main;
        }

        private void Start()
        {
            // Get a rectangle, that represents the bounds of the camera
            _trueViewPort = _mainCamera.pixelRect;
            
            var topRightVpPosition = _mainCamera.ScreenToWorldPoint(new Vector3(_trueViewPort.width, _trueViewPort.height, 0));

            _innerRect = new Rect(Vector2.zero,
                new Vector2(Map.Map.width - topRightVpPosition.x * 2, Map.Map.height - topRightVpPosition.y * 2));
        }

        private void Update()
        {
            if (Touch.activeFingers.Count == 1)
            {
                MoveCamera(Touch.activeTouches[0]);
            }

            // if (Touch.activeFingers.Count == 2)
            // {
            //     ZoomCamera(Touch.activeTouches[0], Touch.activeTouches[1]);
            // }
        }

        private void MoveCamera(Touch touch)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _startDrag = _mainCamera.ScreenToWorldPoint(touch.screenPosition);
            }
            
            if (touch.phase == TouchPhase.Moved)
            {
                var currentPos = _mainCamera.ScreenToWorldPoint(touch.screenPosition);

                var delta = _startDrag - currentPos;
                var newPosition = _mainCamera.transform.position + delta;

                _mainCamera.transform.position = ClampToMap(newPosition);
            }
        }

        private Vector3 ClampToMap(Vector3 vector)
        {
            vector.x = Mathf.Clamp(vector.x, -_innerRect.width / 2, _innerRect.width / 2);
            vector.y = Mathf.Clamp(vector.y, -_innerRect.height / 2, _innerRect.height / 2);
            Debug.Log(vector);
            return vector;
        }

        // postpone for now
        private void ZoomCamera(Touch touch1, Touch touch2)
        {
            if (touch1.phase == TouchPhase.Began)
            {
                _initialZoomDistance = Vector2.Distance(_mainCamera.ScreenToWorldPoint(touch1.screenPosition),
                    _mainCamera.ScreenToWorldPoint(touch2.screenPosition));

            }

            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                var currentDistance = Vector2.Distance(_mainCamera.ScreenToWorldPoint(touch1.screenPosition),
                    _mainCamera.ScreenToWorldPoint(touch2.screenPosition));

                var touchDistance = _initialZoomDistance - currentDistance;
                _initialZoomDistance = currentDistance;

                _mainCamera.fieldOfView += touchDistance * 0.1f;
            }
        }
    }
}
