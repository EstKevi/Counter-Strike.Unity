using UnityEngine;

public class KeyBoardInput : IInput
{
    public bool Jump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public float DirectionX()
    {
        return Input.GetAxis("Horizontal");
    }

    public float DirectionZ()
    {
        return Input.GetAxis("Vertical");
    }
}