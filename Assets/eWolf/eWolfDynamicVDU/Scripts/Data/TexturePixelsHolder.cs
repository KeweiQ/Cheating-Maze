using UnityEngine;

namespace eWolf.eWolfDynamicVDU.Scripts.Data
{
    public class TexturePixelsHolder
    {
        private Color[] _backGroundColors;
        private Color[] _colors;
        private Renderer _rend;
        private Texture2D _texture;

        public TexturePixelsHolder(Renderer rend)
        {
            _rend = rend;

            _texture = MonoBehaviour.Instantiate(_rend.material.mainTexture) as Texture2D;
            _colors = _texture.GetPixels();
            _backGroundColors = _texture.GetPixels();
        }

        public int TextureWidth
        {
            get
            {
                return _texture.width;
            }
        }

        public int TextureHeight
        {
            get
            {
                return _texture.height;
            }
        }

        public void ApplyTexture()
        {
            _texture.SetPixels(_colors);
            _texture.Apply(false);

            _rend.material.mainTexture = _texture;
        }

        public void FillBackGroundColor(Color backColor)
        {
            for (int i = 0; i < _colors.Length; i++)
            {
                _colors[i] = backColor;
                _backGroundColors[i] = backColor;
            }
        }

        public Color GetPixel(int x, int y)
        {
            return _colors[TextureWidth * y + ((TextureWidth - 1) - x)];
        }

        public void RestorePixel(int x, int y)
        {
            if (x < 0 || y < 0)
                return;

            if (x >= TextureWidth || y >= TextureHeight)
            {
                Debug.Log("Can't set pixel at x:" + x + " y:" + y);
                return;
            }

            _colors[TextureWidth * y + ((TextureWidth - 1) - x)] = _backGroundColors[TextureWidth * y + ((TextureWidth - 1) - x)];
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (x < 0 || y < 0)
                return;

            if (x >= TextureWidth || y >= TextureHeight)
                return;

            _colors[TextureWidth * y + ((TextureWidth - 1) - x)] = color;
        }
    }
}
