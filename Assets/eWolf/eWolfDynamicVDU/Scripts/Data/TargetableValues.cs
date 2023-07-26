namespace eWolf.eWolfDynamicVDU.Scripts.Data
{
    public class TargetableValues
    {
        private int _min = 0;
        private int _max = 10;
        private int _total = 10;
        private int _stepsCounts = 1;
        private int[] _bars;
        private int[] _barsTarget;

        public TargetableValues(int total, int min, int max, int stepsCounts)
        {
            _total = total;
            _min = min;
            _max = max;

            CreateArray();

            _stepsCounts = stepsCounts;
        }

        private void CreateArray()
        {
            _bars = new int[_total];
            _barsTarget = new int[_total];

            for (int i = 0; i < _total; i++)
            {
                _barsTarget[i] = _bars[i] = UnityEngine.Random.Range(_min, _max / 2);
            }

            Update(0);
        }

        public int Total
        {
            get
            {
                return _total;
            }
        }

        public int this[int index]
        {
            get
            {
                return _bars[index];
            }
        }

        public void Update(float alertness)
        {
            float gap = _max;
            float g2 = gap / 2;

            int max = (int)(g2 + (alertness * g2));
            int min = max / 2;
            if (min < _min)
                min = _min;

            for (int i = 0; i < _bars.Length; i++)
            {
                if (_bars[i] == _barsTarget[i])
                {
                    _barsTarget[i] = UnityEngine.Random.Range(min, max);
                }

                // The speed of changes
                for (int j = 0; j < _stepsCounts; j++)
                {
                    if (_bars[i] > _barsTarget[i])
                        _bars[i]--;

                    if (_bars[i] < _barsTarget[i])
                        _bars[i]++;
                }
            }
        }
    }
}