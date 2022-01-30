using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace gui
{
    public class CardGUIController : MonoBehaviour, IPointerMoveHandler, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler
    {

        [SerializeField]
        private Animator _maskAnimator;

        [Space(25)]
        [SerializeField]
        private float _scaleUpAmount = 1.25f;
        [SerializeField]
        private float _scaleUpDuration = 0.25f;
        [SerializeField]
        private Ease _scaleUpEase = Ease.OutQuad;
        [SerializeField]
        private float _scaleUpCooldown = 0.25f;
        
        private Card _card;

        private bool _isTop = true;
        private bool _isCursorOver = false;
        private float _cursorOverTime = 0;

        private Vector3 _topPoint, _bottomPoint;


        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log(_isTop);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            _maskAnimator.SetBool("isOver", true);
            _isCursorOver = true;

            var topDistance = Vector3.Distance(Input.mousePosition, transform.position + Vector3.left + Vector3.up);
            var bottomDistance = Vector3.Distance(Input.mousePosition, transform.position + Vector3.right + Vector3.down);

            _isTop = topDistance < bottomDistance;
            _maskAnimator.SetBool("isFst", _isTop);
            _cursorOverTime += Time.deltaTime;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _maskAnimator.SetBool("isOver", false);
            _isCursorOver = false;
            transform.DOKill();
            transform.DOScale(Vector3.one, _scaleUpDuration).SetEase(_scaleUpEase);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOKill();
            transform.DOScale(Vector3.one * _scaleUpAmount, _scaleUpDuration).SetEase(_scaleUpEase);
            _cursorOverTime = 0;
        }
    }
}

