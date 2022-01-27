using Core.Game;
using Core.Interfaces;
using Core.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
namespace Core.Controls
{
    public class InputManager : MonoBehaviour
    {
        private BuyService _buyService;
        private UiController _uiController;
        
        public float leftBorder;
        public float rightBorder;
        public float upperBorder;
        public float lowerBorder;
        private Rect _trueViewPort;
        private Rect _innerRect;
        private Camera _mainCamera;
        private Vector3 _startDrag;
        private float _initialZoomDistance;
        private bool _isDragging;

        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
            _mainCamera = Camera.main;
        }

        private void Start()
        {
            Initiate();
            
            // Get a rectangle, that represents the bounds of the camera
            _trueViewPort = _mainCamera.pixelRect;
            
            var topRightVpPosition = _mainCamera.ScreenToWorldPoint(new Vector3(_trueViewPort.width, _trueViewPort.height, 0));

            _innerRect = new Rect(Vector2.zero,
                new Vector2(Map.MapConstants.width - topRightVpPosition.x * 2, Map.MapConstants.height - topRightVpPosition.y * 2));
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject() && !_isDragging && !_buyService.UiElementIsDragged) return;

            if (Touch.activeFingers.Count == 1 && !_buyService.UiElementIsDragged)
            {
                MoveCamera(Touch.activeTouches[0]);
            }
            else if (_buyService.UiElementIsDragged)
            {
                DragGameObject(Touch.activeTouches[0], _buyService.DraggedElement);
            }

            // if (Touch.activeFingers.Count == 2)
            // {
            //     ZoomCamera(Touch.activeTouches[0], Touch.activeTouches[1]);
            // }
        }

        private void DragGameObject(Touch touch, GameObject go)
        {
            var newPosition = _mainCamera.ScreenToWorldPoint(touch.screenPosition);
            newPosition.z = go.transform.position.z;

            go.transform.position = Utils.SnapToGrid(newPosition);

            var buyableElement = go.GetComponent<IBuyable>();

            buyableElement?.UpdatePreview(newPosition);
        }

        private void MoveCamera(Touch touch)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _startDrag = _mainCamera.ScreenToWorldPoint(touch.screenPosition);
                _isDragging = true;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                _isDragging = false;
            }
            
            if (touch.phase == TouchPhase.Moved)
            {
                ResetElementsForCameraMove();

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
            return vector;
        }

        // Register any methods used for cleaning up stuff, when camera is moved
        private void ResetElementsForCameraMove()
        {
            _uiController.HideTurretConfirmPopover();
            _buyService.CancelBuyPreview();
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
        
        private void Initiate()
        {
            var sceneContext = GameObject.Find("Context");
            _buyService = sceneContext.GetComponent<BuyService>();
            _uiController = sceneContext.GetComponent<UiController>();
        }
    }
}
