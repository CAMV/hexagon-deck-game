using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gui
{
    public class CardGUIRegion : MonoBehaviour
    {

        public delegate void MouseEnterRegionDelegate();
        public event MouseEnterRegionDelegate MouseEnteredRegion;

        public delegate void MouseExitRegionDelegate();
        public event MouseExitRegionDelegate MouseExitedRegion;


        private bool _isMouseOnRegion = false;

        private bool _isTop = true;
        private Vector3 _topPoint, _bottomPoint;

        private void Awake()
        {
            var _rectTransform = GetComponent<RectTransform>();
            _topPoint = _rectTransform.localPosition + (Vector3.up * _rectTransform.sizeDelta.y / 2) + (Vector3.left * _rectTransform.sizeDelta.x / 2);
            _bottomPoint = _rectTransform.localPosition + (Vector3.down * _rectTransform.sizeDelta.y / 2) + (Vector3.right * _rectTransform.sizeDelta.x / 2);
        }

        void OnMouseEnter()
        {
            _isMouseOnRegion = true;

            if (MouseEnteredRegion != null)
                MouseEnteredRegion();
        }

        private void OnMouseOver()
        {
            Debug.Log(_topPoint);
            Debug.Log(_bottomPoint);
        }

        void OnMouseExit()
        {
            _isMouseOnRegion = false;

            if (MouseExitedRegion != null)
                MouseExitedRegion();
        }
    }

}
