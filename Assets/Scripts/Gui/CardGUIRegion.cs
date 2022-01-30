using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace gui
{
    public class CardGUIRegion : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler
    {

        public delegate void MouseEnterRegionDelegate();
        public event MouseEnterRegionDelegate MouseEnteredRegion;

        public delegate void MouseExitRegionDelegate();
        public event MouseExitRegionDelegate MouseExitedRegion;


        private bool _isMouseOnRegion = false;

        private bool _isTop = true;
        private Vector3 _topPoint, _bottomPoint;

        void Awake()
        {
            var rect = GetComponent<RectTransform>().rect;
            _topPoint = rect.position + (Vector2.up * (rect.size.y / 2)) + (Vector2.left * (rect.size.x / 2));
            _bottomPoint = rect.position + (Vector2.down * (rect.size.y / 2)) + (Vector2.right * (rect.size.x / 2));

            ////_topPoint = Camera.main.ScreenToWorldPoint(rectTransform.position + (Vector3.up * rectTransform.sizeDelta.y / 2) + (Vector3.left * rectTransform.sizeDelta.x / 2));
            //_bottomPoint = Camera.main.ScreenToWorldPoint(rectTransform.rect.+ (Vector3.down * rectTransform.sizeDelta.y / 2) + (Vector3.right * rectTransform.sizeDelta.x / 2));
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log(_isTop);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            //Debug.Log(_topPoint);
            //Debug.Log(_bottomPoint);
            //Debug.Log(Input.mousePosition);

            var topDistance = Vector3.Distance(Input.mousePosition, _topPoint);
            var bottomDistance = Vector3.Distance(Input.mousePosition, _topPoint);

            _isTop = topDistance < bottomDistance;
        }
    }
}
