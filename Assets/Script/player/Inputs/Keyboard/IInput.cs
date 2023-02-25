namespace Script.player.Inputs.Keyboard
{
    public interface IInput
    {
        public float MoveHorizontalX();
    
        public float MoveVerticalZ();
    
        public bool KeySpace();

        public bool KeyR();

        public bool KeyEscape();
    }
}