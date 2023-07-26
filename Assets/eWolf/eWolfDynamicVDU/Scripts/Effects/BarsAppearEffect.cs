using eWolf.eWolfDynamicVDU.Scripts.Data;
using eWolf.eWolfDynamicVDU.Scripts.Interfaces;
using UnityEngine;

namespace eWolf.eWolfDynamicVDU.Scripts.Effects
{
    public class BarsAppearEffect : EffectBase, IModifierEffects
    {
        private int _bars;
        private int _height = 3;
        private int _max = 8;
        private TargetableValues _targetableValues;
        private int _width = 10;
        private int _xOffSet = 2;

        public BarsAppearEffect(TexturePixelsHolder pixelsHolder, MonitorDetails monitorDetails, ColorDetails mainColor) : base(pixelsHolder, monitorDetails, mainColor)
        {
        }

        public void DrawBoarder()
        {
            DrawBoarderBasic();
        }

        public void StartEffect(float alertness)
        {
            float width = (_right - _left);
            if (width < 32)
            {
                _width = 4;
                _height = 2;
            }
            else if (width > 90)
            {
                _width = 10;
                _height = 3;
            }
            else
            {
                _width = 8;
                _height = 2;
            }

            _bars = _right - _left;
            _bars /= (_width + 2);

            _max = _bottom - _top;
            _max /= (_height + 1);

            _targetableValues = new TargetableValues(_bars, 0, _max, 1);

            _xOffSet = _right - _left;
            _xOffSet /= _width;
            _xOffSet /= 2;

            UpdateFrame(0);
        }

        public void UpdateFrame(float alertness)
        {
            int x = _left + _xOffSet;

            for (int i2 = 0; i2 < _targetableValues.Total; i2++)
            {
                int y = _bottom - 2;
                int value = _targetableValues[i2];

                for (int i = 0; i < _max; i++)
                {
                    Color col = GetColor(value, i);

                    for (int j = 0; j < _width; j++)
                    {
                        for (int p = 0; p < _height; p++)
                        {
                            _pixelsHolder.SetPixel(x + j, y + p, col);
                        }
                    }
                    y -= _height + 1;
                }
                x += _width + 2;
            }

            _targetableValues.Update(alertness);
        }

        private Color GetColor(int value, int current)
        {
            int mid = (int)(_max * 0.75f);
            int midHigh = (int)(_max * 0.85f);

            Color col = _color.Main;

            if (current > mid)
            {
                col = _color.Warning;
            }

            if (current > midHigh)
            {
                col = _color.Error;
            }

            if (current > value)
                col /= 2;

            return col;
        }
    }
}
