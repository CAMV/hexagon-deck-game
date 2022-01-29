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

        void OnMouseEnter()
        {
            _isMouseOnRegion = true;

            if (MouseEnteredRegion != null)
                MouseEnteredRegion();
        }

        void OnMouseClick()
        {
            _isMouseOnRegion = true;

            if (MouseEnteredRegion != null)
                MouseEnteredRegion();
        }

        void OnMouseExit()
        {
            _isMouseOnRegion = false;

            if (MouseExitedRegion != null)
                MouseExitedRegion();
        }
    }

}
