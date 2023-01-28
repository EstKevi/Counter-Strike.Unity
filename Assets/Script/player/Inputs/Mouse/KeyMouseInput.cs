using UnityEngine;

namespace Script.player.Inputs
{
    public class KeyMouseInput : IInputMouse
    {
        public float DirectionMouseX()
        {
            return Input.GetAxis("Mouse X");
        }

        public float DirectionMouseY()
        {
            return Input.GetAxis("Mouse Y");
        }

        public bool MouseLeft()
        {
            return Input.GetMouseButton(0);
        }

        public bool MouseRight()
        {
            return Input.GetMouseButton(1);
        }
    }
}