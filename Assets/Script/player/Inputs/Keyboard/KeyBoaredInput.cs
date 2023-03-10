using UnityEngine;

namespace Script.player.Inputs.Keyboard
{
    public class KeyBoardInput : IInput
    {
        public float MoveHorizontalX()
        {
            return Input.GetAxis("Horizontal");
        }

        public float MoveVerticalZ()
        {
            return Input.GetAxis("Vertical");
        }

        public bool KeySpace()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        public bool KeyR()
        {
            return Input.GetKeyDown(KeyCode.R);
        }

        public bool KeyEscape()
        {
            return Input.GetKeyDown(KeyCode.Escape);
        }
    }
}