using eWolf.eWolfDynamicVDU.Scripts.Data;
using eWolf.eWolfDynamicVDU.Scripts.Interfaces;
using UnityEngine;

namespace eWolf.eWolfDynamicVDU.Scripts.Effects
{
    public class ScrollingTextEffect_Large : EffectBase, IModifierEffects
    {
        private int _delay = 0;

        public ScrollingTextEffect_Large(TexturePixelsHolder pixelsHolder, MonitorDetails monitorDetails, ColorDetails mainColor) : base(pixelsHolder, monitorDetails, mainColor)
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

            if (_delay++ < 4)
            {
                return;
            }

            _delay = 0;

            int diff = _right - _left;
            int r = Random.Range(0, diff / 4);
            bool[] line = new bool[r];

            for (int i = 0; i < line.Length; i++)
            {
                line[i] = true;
            }

            Color lineColor = GetAlertnessColor(alertness);

            int s = Random.Range(0, 7);
            while (s < line.Length)
            {
                line[s] = false;
                s += Random.Range(2, 8);
            }

            DrawFakeTextLine(line, lineColor, _bottom);
        }
    }
}