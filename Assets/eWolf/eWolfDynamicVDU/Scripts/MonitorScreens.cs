using eWolf.eWolfDynamicVDU.Scripts.Data;
using eWolf.eWolfDynamicVDU.Scripts.Effects;
using eWolf.eWolfDynamicVDU.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace eWolf.eWolfDynamicVDU.Scripts
{
    public class MonitorScreens : MonoBehaviour
    {
        public bool IsPaused = false;
        public float Alert;
        public ColorDetails ColorSets = new ColorDetails();
        public int Interval = 20;
        public List<MonitorDetails> MonitorDetails = new List<MonitorDetails>();
        private int _delay;
        private List<IModifierEffects> _effects = new List<IModifierEffects>();
        private TexturePixelsHolder _pixelsHolder;

        public void SetPause(bool paused)
        {
            IsPaused = paused;
        }

        public void Start()
        {
            _delay = UnityEngine.Random.Range(5, 30);
            _pixelsHolder = new TexturePixelsHolder(GetComponent<Renderer>());
            _pixelsHolder.FillBackGroundColor(ColorSets.BackGround);

            _effects.AddRange(AddEffects());

            DrawAllBoarders();

            foreach (IModifierEffects effect in _effects)
            {
                if (!IsPaused)
                {
                    effect.StartEffect(Alert);
                }
            }

            foreach (IModifierEffects effect in _effects)
            {
                if (!IsPaused)
                {
                    effect.ApplyTextrue();
                }
            }
        }

        public void Update()
        {
            UpdateSnapshot();

            if (_delay-- != 0)
                return;

            _delay = Interval;
            foreach (IModifierEffects effect in _effects)
            {
                if (!IsPaused)
                {
                    effect.UpdateFrame(Alert);

                    effect.ApplyTextrue();
                }
            }
        }

        private void UpdateSnapshot()
        {
            if (Input.GetKeyDown("c"))
            {
                string screenshotFilename;
                DateTime td = System.DateTime.Now;
                screenshotFilename = "..//ScreenShots//SS - " + td.ToString("yyyy MM dd-HH-mm-ss-ffff") + ".png";
                ScreenCapture.CaptureScreenshot(screenshotFilename);
                Debug.Log("Taken Snap Shot." + td.ToString("yyyy MM dd-HH-mm-ss-ffff"));
            }
        }

        public void UpdateAlert(float alert)
        {
            Alert = alert;
        }

        private IEnumerable<IModifierEffects> AddEffects()
        {
            List<IModifierEffects> items = new List<IModifierEffects>();

            foreach (MonitorDetails md in MonitorDetails)
            {
                switch (md.Effect)
                {
                    case MonitorDefines.VDUEffects.ScrollingText_Small:
                        items.Add(new ScrollingTextEffect(_pixelsHolder, md, ColorSets));
                        break;

                    case MonitorDefines.VDUEffects.ScrollingText_Large:
                        items.Add(new ScrollingTextEffect_Large(_pixelsHolder, md, ColorSets));
                        break;

                    case MonitorDefines.VDUEffects.ScrollingGraph:
                        items.Add(new ScrollingGraphEffect(_pixelsHolder, md, ColorSets));
                        break;

                    case MonitorDefines.VDUEffects.BarsRight:
                        items.Add(new BarsRightEffect(_pixelsHolder, md, ColorSets));
                        break;

                    case MonitorDefines.VDUEffects.BarsAppear:
                        items.Add(new BarsAppearEffect(_pixelsHolder, md, ColorSets));
                        break;

                    case MonitorDefines.VDUEffects.HeartBeat:
                        items.Add(new HeartBeatEffect(_pixelsHolder, md, ColorSets));
                        break;

                    case MonitorDefines.VDUEffects.DynamicGraph:
                        items.Add(new DynamicGraph(_pixelsHolder, md, ColorSets));
                        break;

                    case MonitorDefines.VDUEffects.BlinkingLightsArray:
                        items.Add(new BlinkingLightsArray(_pixelsHolder, md, ColorSets));
                        break;

                    case MonitorDefines.VDUEffects.EmptyBox:
                        items.Add(new EmptyBox(_pixelsHolder, md, ColorSets));
                        break;

                    case MonitorDefines.VDUEffects.DNA:
                        items.Add(new DNA(_pixelsHolder, md, ColorSets));
                        break;

                    case MonitorDefines.VDUEffects.Audio:
                        items.Add(new Audio(_pixelsHolder, md, ColorSets));
                        break;

                    case MonitorDefines.VDUEffects.TextReport:
                        items.Add(new TextReport(_pixelsHolder, md, ColorSets));
                        break;

                    case MonitorDefines.VDUEffects.PowerBar:
                        items.Add(new PowerBar(_pixelsHolder, md, ColorSets));
                        break;
                }
            }
            return items;
        }

        private void DrawAllBoarders()
        {
            foreach (IModifierEffects effect in _effects)
            {
                effect.DrawBoarder();
            }
        }
    }
}
