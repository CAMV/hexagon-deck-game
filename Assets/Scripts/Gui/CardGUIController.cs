using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace gui
{
    public class CardGUIController : MonoBehaviour
    {

        [SerializeField]
        private CardGUIRegion _topRegion, _bottomRegion;
        [SerializeField]
        private Image _mask;
        


        private Card _card;

        public void TESTicles(string s)
        {
            Debug.Log(s);
        }


        // Start is called before the first frame update
        void Awake()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

