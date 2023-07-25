using eWolf.eWolfDynamicVDU.Scripts.Data;
using eWolf.eWolfDynamicVDU.Scripts.Interfaces;
using UnityEngine;

namespace eWolf.eWolfDynamicVDU.Scripts.Effects
{
    public class PowerBar : EffectBase, IModifierEffects
    {
        private int _height;
        private int _valueMax;
        private float _value;
        private int _width;
        private int _bars;
        private int _xOffSet;
        private TargetableValues _targetableValues;

        public PowerBar(TexturePixelsHolder pixelsHolder, MonitorDetails monitorDetails, ColorDetails mainColor) : base(pixelsHolder, monitorDetails, mainColor)
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
                _width = 7;
            }
            else if (width > 90)
            {
                _width = 9;
            }
            else
            {
                _width = 12;
            }

            _bars = _right - _left;
            _bars /= (_width + 2);

            _height = _bottom - _top;
            _valueMax = (int)(_height * 0.99f);
            _targetableValues = new TargetableValues(_bars, 0, _valueMax - _width, 1);

            _xOffSet = _right - _left;
            _xOffSet /= (_width + 2);
            _xOffSet /= 2;

            UpdateFrame(0);
        }

        public void UpdateFrame(float alertness)
        {
            int x = _left + _xOffSet;

            _value = alertness;
            ClearScreeen();

            float f = (_height - 20) * _value;

            for (int i = 0; i < _bars; i++)
            {
                Color col = GetColor(_targetableValues[i]);
                DrawBoxBorder(x, _top, _width, (int)(_height * 0.99f));
                DrawBoxFilled(x + 2, _top + (_valueMax - _width) - (_targetableValues[i]), _width - 4, _width, col);
                x += _width + 2;
            }

            _targetableValues.Update(alertness);
        }

        private Color GetColor(int current)
        {
            int mid = (int)(_valueMax * 0.50f);
            int midHigh = (int)(_valueMax * 0.75f);

            Color col = _color.Main;

            if (current > mid)
            {
                col = _color.Warning;
            }

            if (current > midHigh)
            {
                col = _color.Error;
            }

            return col;
        }
    }
}
