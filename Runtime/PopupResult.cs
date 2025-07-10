
namespace SayItLabs.PopupSystem
{
    public struct PopupResult
    {
        private readonly int buttonPressed;

        public bool Button1Pressed { get { return buttonPressed == PopupStatics.BUTTON_1; } }
        public bool Button2Pressed { get { return buttonPressed == PopupStatics.BUTTON_2; } }

        public PopupResult(int buttonPressed)
        {
            this.buttonPressed = buttonPressed;
        }
    }
}