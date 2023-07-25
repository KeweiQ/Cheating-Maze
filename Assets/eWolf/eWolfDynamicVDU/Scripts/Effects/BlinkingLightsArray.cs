using eWolf.eWolfDynamicVDU.Scripts.Data;
using eWolf.eWolfDynamicVDU.Scripts.Interfaces;
using UnityEngine;

namespace eWolf.eWolfDynamicVDU.Scripts.Effects
{
    public class BlinkingLightsArray : EffectBase, IModifierEffects
    {
        private float _buttonHeight = 7;
        private float _buttonWidth = 10;

        public BlinkingLightsArray(TexturePixelsHolder pixelsHolder, MonitorDetails monitorDetails, ColorDetails mainColor) : base(pixelsHolder, monitorDetails, mainColor)
        {
        }

        public void DrawBoarder()
        {
            DrawBoarderBasic();
        }

        public void StartEffect(float alertness)
        {
            float width = _right - _left;
            _buttonWidth = width / 10;

            float heigh = _bottom - _top;
            _buttonHeight = heigh / 10;

            UpdateFrame(0);
        }

        public void UpdateFrame(float alertness)
        {
            float offX = _buttonWidth * 0.5f;
            float offY = _buttonHeight * 0.5f;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    float x = (i * (_buttonWidth)) + _left + offX;
                    float y = (j * (_buttonHeight)) + _top + offY;

                    float f = Random.Range(1, 100);
                    f /= 100;

                    Color col = _color.BackGround;
                    float r = Random.Range(1, 100);
                    r *= alertness;

                    if (r < 50)
                        col = Color.Lerp(_color.Main, _color.BackGround, f);

                    if (r >= 50 && r < 75)
                        col = Color.Lerp(_color.Warning, _color.Main, f);

                    if (r >= 75)
                        col = Color.Lerp(_color.Error, _color.Main, f);

                    DrawBox((int)x, (int)y, (int)_buttonWidth, (int)_buttonHeight, col);
                }
            }
        }

        private void DrawBox(int x, int y, int width, int height, Color col)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    _pixelsHolder.SetPixel(i + x, j + y, col);
                }
            }
        }
    }
}
