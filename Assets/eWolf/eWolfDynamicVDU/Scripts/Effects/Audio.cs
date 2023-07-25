using eWolf.eWolfDynamicVDU.Scripts.Data;
using eWolf.eWolfDynamicVDU.Scripts.Interfaces;
using UnityEngine;

namespace eWolf.eWolfDynamicVDU.Scripts.Effects
{
    public class Audio : EffectBase, IModifierEffects
    {
        private bool _alternate;
        private float _maxHeight;
        private int _mid;
        private int _nextTarget;
        private int _phaseTime;
        private int _target;

        public Audio(TexturePixelsHolder pixelsHolder, MonitorDetails monitorDetails, ColorDetails mainColor) : base(pixelsHolder, monitorDetails, mainColor)
        {
        }

        public void DrawBoarder()
        {
            DrawBoarderBasic();
        }

        public void StartEffect(float alertness)
        {
            _mid = (_bottom + _top) / 2;
            _maxHeight = (_bottom - _top) * 0.49f;
            _current = 0;
            SetTarget(0);

            int width = _right - _left;
            for (int i = 0; i < width; i++)
            {
                UpdateFrame(alertness);
            }
        }

        public void UpdateFrame(float alertness)
        {
            ScrollLeft();
            ClearNewRow(_right);

            _alternate = !_alternate;

            if (_alternate)
                return;

            float offSet = _target * GetRandomPercentage();

            Color col = GetAlertnessColor(alertness);

            DrawLineParts(_right, _mid, _right, _mid + offSet, col);
            DrawLineParts(_right, _mid, _right, _mid - offSet, col);

            AdjustTargets(alertness);
        }

        private void AdjustTargets(float alertness)
        {
            if (_phaseTime-- == 0)
                SetTarget(alertness);

            if (_target > _nextTarget)
            {
                _target -= 2;
            }
            if (_target < _nextTarget)
            {
                _target += 2;
            }
        }

        private void SetTarget(float alertness)
        {
            float gap = _maxHeight;
            float g2 = gap / 2;

            int max = (int)(g2 + (alertness * g2));

            float percentage = GetRandomPercentage();

            _nextTarget = (int)(max * percentage);
            _phaseTime = Random.Range(3, 15);
        }
    }
}
