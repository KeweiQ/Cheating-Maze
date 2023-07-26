using eWolf.eWolfDynamicVDU.Scripts.Data;
using eWolf.eWolfDynamicVDU.Scripts.Interfaces;
using UnityEngine;

namespace eWolf.eWolfDynamicVDU.Scripts.Effects
{
    public class TextReport : EffectBase, IModifierEffects
    {
        private int _delay = 3;
        private int _row = 0;

        public TextReport(TexturePixelsHolder pixelsHolder, MonitorDetails monitorDetails, ColorDetails mainColor) : base(pixelsHolder, monitorDetails, mainColor)
        {
        }

        public void DrawBoarder()
        {
            DrawBoarderBasic();
        }

        public void StartEffect(float alertness)
        {
            _row = _top + 4;
            UpdateFrame(0);
        }

        public void UpdateFrame(float alertness)
        {
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

            DrawFakeTextLine(line, lineColor, _row);
            _row += 4;
            if (_row > _bottom)
            {
                _row = _top + 4;
                ClearScreeen();
            }
        }
    }
}