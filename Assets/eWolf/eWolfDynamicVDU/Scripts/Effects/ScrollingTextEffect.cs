using eWolf.eWolfDynamicVDU.Scripts.Data;
using eWolf.eWolfDynamicVDU.Scripts.Interfaces;
using UnityEngine;

namespace eWolf.eWolfDynamicVDU.Scripts.Effects
{
    public class ScrollingTextEffect : EffectBase, IModifierEffects
    {
        public ScrollingTextEffect(TexturePixelsHolder pixelsHolder, MonitorDetails monitorDetails, ColorDetails mainColor)
            : base(pixelsHolder, monitorDetails, mainColor)
        {
        }

        public void DrawBoarder()
        {
            DrawBoarderBasic();
        }

        public void StartEffect(float alertness)
        {
            int width = _bottom - _top;
            for (int i = 0; i < width; i++)
            {
                UpdateFrame(alertness);
            }
        }

        public void UpdateFrame(float alertness)
        {
            ScrollUp();

            for (int i = _left; i < _right; i++)
            {
                _pixelsHolder.RestorePixel(i, _bottom);
            }

            int diff = _right - _left;
            int r = Random.Range(0, diff);

            bool[] line = new bool[r];

            for (int i = 0; i < line.Length; i++)
            {
                line[i] = true;
            }

            int s = Random.Range(0, 7);
            while (s < line.Length)
            {
                line[s] = false;
                s += Random.Range(2, 8);
            }

            Color lineColor = GetAlertnessColor(alertness);

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i])
                {
                    _pixelsHolder.SetPixel(i + _left, _bottom, lineColor);
                }
            }
        }
    }
}