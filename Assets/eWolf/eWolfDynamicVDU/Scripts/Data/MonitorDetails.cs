using System;
using UnityEngine;

namespace eWolf.eWolfDynamicVDU.Scripts.Data
{
    [Serializable]
    public class MonitorDetails
    {
        public MonitorDefines.VDUEffects Effect = MonitorDefines.VDUEffects.BarsAppear;
        public Rect Rect = new Rect(0.1f, 0.1f, 0.8f, 0.8f);
        public bool ApplyBorder = true;
    }
}
