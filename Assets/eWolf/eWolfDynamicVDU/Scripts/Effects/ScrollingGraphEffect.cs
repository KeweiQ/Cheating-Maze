using eWolf.eWolfDynamicVDU.Scripts.Data;
using eWolf.eWolfDynamicVDU.Scripts.Interfaces;
using UnityEngine;

namespace eWolf.eWolfDynamicVDU.Scripts.Effects
{
    public class ScrollingGraphEffect : EffectBase, IModifierEffects
    {
        private int _steps = 1;
        private int _target;

        public ScrollingGraphEffect(TexturePixelsHolder pixelsHolder, MonitorDetails monitorDetails, ColorDetails mainColor) : base(pixelsHolder, monitorDetails, mainColor)
        {
        }

        public void DrawBoarder()
        {
            DrawBoarderBasic();
        }

        public void StartEffect(float alertness)
        {
            _current = Random.Range(1, _bottom - _top);
            SetRandomWave();

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

            DrawLinePart(x, _steps, _target, _color.Main);

            if (_current == _target)
            {
                SetRandomWave();
            }
        }

        private void SetRandomWave()
        {
            float diff = _bottom - _top;
            _target = Random.Range(1, (int)diff);
            _steps = Random.Range(1, (int)(diff * 0.25));
        }
    }
}
