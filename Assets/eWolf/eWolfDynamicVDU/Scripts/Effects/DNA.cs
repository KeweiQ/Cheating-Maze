using eWolf.eWolfDynamicVDU.Scripts.Data;
using eWolf.eWolfDynamicVDU.Scripts.Interfaces;
using UnityEngine;

namespace eWolf.eWolfDynamicVDU.Scripts.Effects
{
    public class DNA : EffectBase, IModifierEffects
    {
        private Color[,] _DNAColor;
        private int _links = 14;
        private float _radius = 4;
        private float _spin = 0;

        public DNA(TexturePixelsHolder pixelsHolder, MonitorDetails monitorDetails, ColorDetails mainColor) : base(pixelsHolder, monitorDetails, mainColor)
        {
        }

        public void DrawBoarder()
        {
            DrawBoarderBasic();
        }

        public void StartEffect(float alertness)
        {
            float height = (_bottom - _top);
            if (height < 32)
            {
                _links = 8;
                _radius = 1.5f;
            }
            else if (height < 90)
            {
                _links = 11;
                _radius = 2.5f;
            }
            else if (height < 140)
            {
                _links = 14;
                _radius = 4;
            }
            else
            {
                _links = 20;
                _radius = 6;
            }

            _DNAColor = new Color[2, _links];
            for (int i = 0; i < _links; i++)
            {
                _DNAColor[0, i] = _color.Main;
                _DNAColor[1, i] = _color.Main;
            }
            UpdateFrame(0);
        }

        public void UpdateFrame(float alertness)
        {
            ClearScreeen();

            int x = (_right + _left) / 2;
            int xHalf = x / 2;

            int y = _top;

            int x2 = (x * 2) + _left;
            x += _left;

            float twist = _spin;

            for (int i = 0; i < _links; i++)
            {
                Color colLeft = _DNAColor[0, i];
                Color colRight = _DNAColor[1, i];

                y += (int)(_radius * 2);

                float xOffSet = Mathf.Cos(twist) * xHalf;
                float xOffStartSet = Mathf.Cos(twist) * (xHalf * 0.1f);

                if (xOffSet > 0)
                {
                    DrawLineParts(x - xOffStartSet, y, x - (xOffSet - 6), y, colLeft / 2);
                    DrawLineParts(x + xOffStartSet, y, x + (xOffSet - 6), y, colRight / 2);
                }
                else
                {
                    DrawLineParts(x - xOffStartSet, y, x - (xOffSet + 6), y, colLeft / 2);
                    DrawLineParts(x + xOffStartSet, y, x + (xOffSet + 6), y, colRight / 2);
                }

                DrawCircle((int)(x + xOffSet), y, _radius, colRight);
                DrawCircle((int)(x - xOffSet), y, _radius, colLeft);

                twist += 0.4f;
            }
            _spin += 0.1f;

            Updatecolors(alertness);
        }

        private void Updatecolors(float alertness)
        {
            for (int i = 0; i < _links / 10; i++)
            {
                int r = (Random.Range(0, 2));
                int r2 = (Random.Range(0, _links));

                _DNAColor[r, r2] = GetAlertnessColor(alertness);
            }
        }
    }
}
