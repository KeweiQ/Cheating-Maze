using eWolf.eWolfDynamicVDU.Scripts.Data;
using eWolf.eWolfDynamicVDU.Scripts.Interfaces;
using UnityEngine;

namespace eWolf.eWolfDynamicVDU.Scripts.Effects
{
    public class RadarEffect : EffectBase, IModifierEffects
    {
        private float _radius = 10;
        private float _angle = 0;

        public RadarEffect(TexturePixelsHolder pixelsHolder, MonitorDetails monitorDetails, ColorDetails mainColor) :
           base(pixelsHolder, monitorDetails, mainColor)
        {
        }

        public void DrawBoarder()
        {
            DrawBoarderBasic();
        }

        public void StartEffect(float alertness)
        {
            _radius = (_right - _left) / 2.5f;

            ClearScreeen();

            int x = (_right - _left) / 2;
            int y = (_bottom - _top) / 2;

            DrawCircle(x + _left, y + _top, _radius, _color.Main);
        }

        public void UpdateFrame(float alertness)
        {
            int x = (_right - _left) / 2;
            int y = (_bottom - _top) / 2;

            float an = _angle;

            for (int i = 0; i < 6; i++)
            {
                float colorValue = i * 20;
                Color col = Color.Lerp(_color.Main, _color.BackGround, (colorValue) / 100);
                an = DrawBand(x, y, an, col);
                an -= 0.1f;
            }

            _angle += 0.05f;
        }

        private float DrawBand(int x, int y, float an, Color col)
        {
            for (int i = 0; i < 6; i++)
            {
                float x2b = Mathf.Sin(an) * _radius;
                float y2b = Mathf.Cos(an) * _radius;
                x2b += x;
                y2b += y;

                DrawLineParts(x + _left, y + _top,
                        (int)x2b + _left, (int)y2b + _top, col);
                an -= 0.002f;
            }

            return an;
        }
    }
}
