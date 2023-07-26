using eWolf.eWolfDynamicVDU.Scripts.Data;
using eWolf.eWolfDynamicVDU.Scripts.Interfaces;
using UnityEngine;

namespace eWolf.eWolfDynamicVDU.Scripts.Effects
{
    public class HeartBeatEffect : EffectBase, IModifierEffects
    {
        private float _gap;
        private float[] _heartBeatArray = new float[] { 0, 0, -10, 30, -40, 10, 0, 0, 0, 0, 0, -10, 30, -49, 10, 0, 0, 0 };
        private int _index;
        private int _steps;
        private int _target;

        public HeartBeatEffect(TexturePixelsHolder pixelsHolder, MonitorDetails monitorDetails, ColorDetails mainColor) :
            base(pixelsHolder, monitorDetails, mainColor)
        {
        }

        public void DrawBoarder()
        {
            DrawBoarderBasic();
        }

        public void StartEffect(float alertness)
        {
            _index = 0;
            _gap = (_bottom - _top);
            _target = _current = (int)((_heartBeatArray[_index] / 100) * _gap);

            int width = _right - _left;
            for (int i = 0; i < width; i++)
            {
                UpdateFrame(alertness);
            }
        }

        public void UpdateFrame(float alertness)
        {
            ScrollLeft();

            int x = _right;

            ClearNewRow(x);

            Color col = _color.Main;
            if (alertness > 0.5)
            {
                col = Color.Lerp(_color.Main, _color.Error, ((alertness - 0.5f) * 2));
            }

            DrawLinePart(x, _steps, _target, col);

            CheckForNextLine(alertness);
        }

        private void CheckForNextLine(float alertness)
        {
            float adjustment = (1 - alertness) * _gap;
            if (_current == _target)
            {
                _target = (int)(0.5f * _gap);
                _target += (int)(((_heartBeatArray[_index]) / 100) * adjustment);
                _steps = Mathf.Abs((int)((_current - _target) / 2));
                if (_steps == 0)
                    _steps = 1;

                if (_index++ > _heartBeatArray.Length - 2)
                {
                    _index = 0;
                }
            }
        }
    }
}
