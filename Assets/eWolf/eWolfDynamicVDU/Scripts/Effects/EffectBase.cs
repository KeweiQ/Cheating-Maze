using eWolf.eWolfDynamicVDU.Scripts.Data;
using UnityEngine;

namespace eWolf.eWolfDynamicVDU.Scripts.Effects
{
    public class EffectBase
    {
        protected bool _applyBoarder;
        protected int _bottom;
        protected ColorDetails _color;
        protected int _current;
        protected int _left;
        protected TexturePixelsHolder _pixelsHolder;
        protected int _right;
        protected int _top;

        public EffectBase(TexturePixelsHolder pixelsHolder, MonitorDetails monitorDetails, ColorDetails mainColor)
        {
            _pixelsHolder = pixelsHolder;

            float width = _pixelsHolder.TextureWidth;
            float height = _pixelsHolder.TextureHeight;

            width /= 100;
            height /= 100;

            float fleft = monitorDetails.Rect.min.x * 100;
            float fright = monitorDetails.Rect.max.x * 100;
            float ftop = monitorDetails.Rect.min.y * 100;
            float fbottom = monitorDetails.Rect.max.y * 100;

            _applyBoarder = monitorDetails.ApplyBorder;

            _left = (int)(width * fleft);
            _top = (int)(height * ftop);
            _right = (int)(width * fright);
            _bottom = (int)(height * fbottom);
            _color = mainColor;
        }

        public void ApplyTextrue()
        {
            _pixelsHolder.ApplyTexture();
        }

        public float GetRandomPercentage()
        {
            return ((float)(Random.Range(0, 1000)) / 1000);
        }

        protected void ClearNewRow(int x)
        {
            for (int j = _top; j < _bottom; j++)
            {
                _pixelsHolder.RestorePixel(x, j);
            }
        }

        protected void ClearScreeen()
        {
            for (int i = _left; i < _right + 2; i++)
            {
                for (int j = _top; j < _bottom; j++)
                {
                    _pixelsHolder.RestorePixel(i, j);
                }
            }
        }

        protected void DrawBoarderBasic()
        {
            if (!_applyBoarder)
                return;

            for (int i = _left; i < _right + 1; i++)
            {
                _pixelsHolder.SetPixel(i, _top, _color.Border);
                _pixelsHolder.SetPixel(i, _bottom, _color.Border);
            }

            for (int i = _top; i < _bottom; i++)
            {
                _pixelsHolder.SetPixel(_left, i, _color.Border);
                _pixelsHolder.SetPixel(_right, i, _color.Border);
            }

            _left += 2;
            _right -= 2;

            _top += 2;
            _bottom -= 2;
        }

        protected void DrawBoxBorder(int x, int y, int width, int height)
        {
            for (int i = x; i < x + width + 1; i++)
            {
                _pixelsHolder.SetPixel(i, y, _color.Main);
                _pixelsHolder.SetPixel(i, y + height, _color.Main);
            }

            for (int i = y; i < y + height; i++)
            {
                _pixelsHolder.SetPixel(x, i, _color.Main);
                _pixelsHolder.SetPixel(x + width, i, _color.Main);
            }
        }

        protected void DrawBoxFilled(int x, int y, int width, int height, Color col)
        {
            for (int i = x; i < x + width + 1; i++)
            {
                for (int j = y; j < y + height + 1; j++)
                {
                    _pixelsHolder.SetPixel(i, j, col);
                }
            }
        }

        protected void DrawCircle(int x, int y, float radius, Color col)
        {
            for (float i = 0; i < Mathf.PI * 2; i += 0.01f)
            {
                float x2 = Mathf.Sin(i) * radius;
                float y2 = Mathf.Cos(i) * radius;

                x2 += x;
                y2 += y;

                _pixelsHolder.SetPixel((int)(x2), (int)(y2), col);
            }
        }

        protected void DrawFakeTextLine(bool[] line, Color lineColor, int lineY)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i])
                {
                    for (int x = 0; x < 3; x++)
                    {
                        for (int y = 0; y < 3; y++)
                        {
                            if (Random.Range(0, 9) >= 4)
                                _pixelsHolder.SetPixel(((i) * 4) + _left + x, lineY - y, lineColor);
                        }
                    }
                }
            }
        }

        protected void DrawLinePart(int x, int steps, int target, Color col)
        {
            for (int i = 0; i < steps; i++)
            {
                _pixelsHolder.SetPixel(x, _current + _top, col);
                if (_current > target)
                {
                    _current--;
                }
                else if (_current < target)
                {
                    _current++;
                }
            }
        }

        protected void DrawLineParts(float x, float y, float newX, float newY, Color col)
        {
            Vector2 xy = new Vector2(x, y);
            Vector2 targetXY = new Vector2(newX, newY);

            Vector2 diff = targetXY - xy;
            Vector2 normal = diff.normalized;

            float g = (diff.magnitude - normal.magnitude) + 2;
            for (int i = 0; i < (int)g; i++)
            {
                _pixelsHolder.SetPixel((int)xy.x, (int)xy.y, col);
                xy += normal;
            }
        }

        protected void DrawLinePartsGradient(float x, float y, float newX, float newY, ColorDetails colorDetails)
        {
            Vector2 xy = new Vector2(x, y);
            Vector2 targetXY = new Vector2(newX, newY);

            Vector2 diff = targetXY - xy;
            Vector2 normal = diff.normalized;

            float colDiff = _bottom - _top;

            float g = (diff.magnitude - normal.magnitude) + 2;
            for (int i = 0; i < (int)g; i++)
            {
                float ycol = (int)(xy.y - _top);
                ycol /= colDiff;

                Color col = Color.Lerp(colorDetails.Main, colorDetails.Error, 1 - ycol);

                _pixelsHolder.SetPixel((int)xy.x, (int)xy.y, col);
                xy += normal;
                if (xy.x > _right + 1)
                    break;
            }
        }

        protected Color GetAlertnessColor(float alertness)
        {
            float percentage = GetRandomPercentage();

            return GetErrorColor(alertness, percentage);
        }

        protected Color GetErrorColor(float alertness, float percentage)
        {
            Color lineColor = _color.Main;
            if (percentage < alertness)
                lineColor = _color.Error;
            else if (percentage < alertness + (alertness / 2))
                lineColor = _color.Warning;
            return lineColor;
        }

        protected void ScrollLeft()
        {
            for (int i = _left; i < _right; i++)
            {
                for (int j = _top; j < _bottom; j++)
                {
                    _pixelsHolder.SetPixel(i, j, _pixelsHolder.GetPixel(i + 1, j));
                }
            }
        }

        protected void ScrollUp()
        {
            for (int j = _top; j < _bottom; j++)
            {
                for (int i = _left; i < _right; i++)
                {
                    _pixelsHolder.SetPixel(i, j, _pixelsHolder.GetPixel(i, j + 1));
                }
            }
        }
    }
}
