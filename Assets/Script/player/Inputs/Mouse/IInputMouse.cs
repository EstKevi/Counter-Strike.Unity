namespace Script.player.Inputs
{
    public interface IInputMouse
    {
        public float DirectionMouseX();
        
        public float DirectionMouseY();

        public bool MouseLeftButton();

        public bool MouseRightButton();
    }
}