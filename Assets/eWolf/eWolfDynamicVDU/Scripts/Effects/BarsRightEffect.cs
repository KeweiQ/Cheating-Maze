using eWolf.eWolfDynamicVDU.Scripts.Data;
using eWolf.eWolfDynamicVDU.Scripts.Interfaces;
using UnityEngine;

namespace eWolf.eWolfDynamicVDU.Scripts.Effects
{
    public class BarsRightEffect : EffectBase, IModifierEffects
    {
        private TargetableValues _targetableValues;
        private int _barThinkness = 1;

        public BarsRightEffect(TexturePixelsHolder pixelsHolder, MonitorDetails monitorDetails, ColorDetails mainColor) : base(pixelsHolder, monitorDetails, mainColor)
        {
        }

        public void DrawBoarder()
        {
            DrawBoarderBasic();
        }

        public void StartEffect(float alertness)
        {
            int diff = (int)((_right - _left));
            int stepsCounts = 1;

            if (diff > 100)
            {
                stepsCounts = 3;
                _barThinkness = 2;
            }

            int total = _bottom - _top;
            total /= _barThinkness;

            _targetableValues = new TargetableValues(total, 0, diff, stepsCounts);

            UpdateFrame(alertness);
        }

        public void UpdateFrame(float alertness)
        {
            Color lineColor = GetAlertnessColor(alertness);

            float diff = (_right - _left);

            float fullLength = (_right - _left);
            float halfLength = fullLength * 0.5f;
            float nearFullLength = fullLength * 0.75f;

            diff /= 100;

            int y = _top;
            for (int i = 0; i < _targetableValues.Total; i++)
            {
                int length = _targetableValues[i];

                lineColor = _color.Main;
                if (length > nearFullLength)
                    lineColor = _color.Error;
                else if (length > halfLength)
                    lineColor = _color.Warning;

                for (int p = 0; p < _barThinkness; p++)
                {
                    for (int j = _left; j < _left + length; j++)
                    {
                        _pixelsHolder.SetPixel(j, y, lineColor);
                    }

                    for (int j = _left + length; j < _right + 1; j++)
                    {
                        _pixelsHolder.RestorePixel(j, y);
                    }
                    y++;
                }
            }

            _targetableValues.Update(alertness);
        }
    }
}