namespace eWolf.eWolfDynamicVDU.Scripts.Interfaces
{
    public interface IModifierEffects
    {
        void ApplyTextrue();

        void DrawBoarder();

        void StartEffect(float alertness);

        void UpdateFrame(float alertness);
    }
}