namespace eWolf.eWolfDynamicVDU.Scripts.Data
{
    public static class MonitorDefines
    {
        public const string UpdateAlertMessage = "UpdateAlert";
        public const string SetPauseMessage = "SetPause";

        public enum VDUEffects
        {
            ScrollingText_Small,
            ScrollingText_Large,
            ScrollingGraph,
            BarsRight,
            BarsAppear,
            HeartBeat,
            DynamicGraph,
            BlinkingLightsArray,
            EmptyBox,
            DNA,
            Audio,
            TextReport,
            PowerBar,
        }
    }
}
