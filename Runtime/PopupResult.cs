
namespace UI.Popups
{
    public struct PopupResult
    {
        public readonly bool WasSuccessful;

        public PopupResult(bool wasSuccessful)
        {
            WasSuccessful = wasSuccessful;
        }
    }
}