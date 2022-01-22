using System;
using UnityEngine;

namespace Core.UI.Components
{
    public class TrashButton : MonoBehaviour
    {
        private UiController _uiController;
        
        private void Start()
        {
            Initiate();
        }

        private void Initiate()
        {
            var sceneContext = GameObject.Find("Context");
            _uiController =  sceneContext.GetComponent<UiController>();
        }

        private void OnMouseOver()
        {
            // if (_uiController.IsOverTrash(_uiController.LastFingerTouchScreenPosition))
            // {
            //     // Continue
            //     // gameObject.GetComponent<>
            // }
        }
    }
}