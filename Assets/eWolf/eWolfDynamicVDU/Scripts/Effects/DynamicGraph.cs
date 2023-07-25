using eWolf.eWolfDynamicVDU.Scripts.Data;
using eWolf.eWolfDynamicVDU.Scripts.Interfaces;

namespace eWolf.eWolfDynamicVDU.Scripts.Effects
{
    public class DynamicGraph : EffectBase, IModifierEffects
    {
        private TargetableValues _targetableValues;

        public DynamicGraph(TexturePixelsHolder pixelsHolder, MonitorDetails monitorDetails, ColorDetails mainColor) :
            base(pixelsHolder, monitorDetails, mainColor)
        {
        }

        public void DrawBoarder()
        {
            DrawBoarderBasic();
        }

        public void StartEffect(float alertness)
        {
            int diff = (int)((_bottom - _top));
            int stepsCounts = 1;

            int total = (_right - _left) / 6;

            _targetableValues = new TargetableValues(total + 2, 0, diff, stepsCounts);
            UpdateFrame(0);
        }

        public void UpdateFrame(float alertness)
        {
            ClearScreeen();

            int x = 0;
            for (int i = 0; i < _targetableValues.Total - 1; i++)
            {
                int y = _targetableValues[i];
                int y2 = _targetableValues[i + 1];
                DrawLinePartsGradient(x + _left, _bottom - y, (int)x + _left + 6, _bottom - (int)y2, _color);
                x += 6;
            }

            _targetableValues.Update(alertness);
        }
    }
}
