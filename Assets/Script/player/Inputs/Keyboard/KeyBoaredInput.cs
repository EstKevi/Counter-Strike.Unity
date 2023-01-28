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

        public bool SpaseButton()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        public bool R_Button()
        {
            return Input.GetKeyDown(KeyCode.R);
        }
    }
}