using eWolf.eWolfDynamicVDU.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

namespace eWolf.eWolfDynamicVDU.Scripts.ShowMessaging
{
    public class UpdateAlerts : MonoBehaviour
    {
        public List<GameObject> ObjectsToUpdate = new List<GameObject>();
        private float _alert = 0;

        private void OnGUI()
        {
            if (GUI.Button(new Rect(325, 10, 90, 30), "Pause"))
            {
                foreach (GameObject go in ObjectsToUpdate)
                {
                    if (go != null && go.activeSelf)
                        go.SendMessage(MonitorDefines.SetPauseMessage, true);
                }
            }

            if (GUI.Button(new Rect(425, 10, 90, 30), "UnPause"))
            {
                foreach (GameObject go in ObjectsToUpdate)
                {
                    if (go != null && go.activeSelf)
                        go.SendMessage(MonitorDefines.SetPauseMessage, false);
                }
            }

            GUI.Label(new Rect(225, 10, 100, 30), _alert.ToString());

            float newAlert = GUI.HorizontalSlider(new Rect(25, 25, 200, 200), _alert, 0.0F, 1.0F);
            if (newAlert != _alert)
            {
                foreach (GameObject go in ObjectsToUpdate)
                {
                    if (go != null && go.activeSelf)
                        go.SendMessage(MonitorDefines.UpdateAlertMessage, newAlert);
                }
                _alert = newAlert;
            }
        }
    }
}
