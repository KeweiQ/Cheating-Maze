namespace eWolf.eWolfDynamicVDU.Scripts.Interfaces
{
    // TODO: Add error message pop up that stops the screens updating
    // TODO: clear error
    // TODO: Alert
    public interface IReciveEffectMessages
    {
        void UpdateAlert(float alert);

        void SetActive(bool paused);
    }
}
