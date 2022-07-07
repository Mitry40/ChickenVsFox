using System;

namespace Uliger
{
    public class CBoundFloat
    {
        private float _Value;
        private float _Min;
        private float _Max;

        public event Action DOnUp = delegate { };
        public event Action DOnDown = delegate { };
        public event Action DOnChange = delegate { };


        public void New(float value)
        {
            _Min = 0.0f;
            _Max = value;
            Value = value;
        }
        public static void StaticNew(CBoundFloat bv, float value)
        {
            if (bv is null)
            {
                bv = new CBoundFloat(value);
            } else {
                bv._Min = 0.0f;
                bv._Max = value;
                bv.Value = value;
            }
        }



        public CBoundFloat(float value, float min, float max)
        {
            _Min = min;
            _Max = max;
            Value = value;
        }
        public CBoundFloat(float value)
        {
            _Min = 0.0f;
            _Max = value;
            Value = value;
        }
        public CBoundFloat(CBoundFloat boundvalue)
        {
            if (boundvalue != null)
            {
                _Min = boundvalue.Min;
                _Max = boundvalue.Max;
                Value = boundvalue.Value;
            }
        }

        public float Value
        {
            get => _Value;
            set
            {
                _Value = value;
                if (_Value <= _Min)
                {
                    _Value = _Min;
                    DOnChange();
                    DOnDown();
                } else if (_Value >= _Max)
                {
                    _Value = _Max;
                    DOnChange();
                    DOnUp();
                } else {
                    DOnChange();
                }
            }
        }

        public float Min
        {
            get => _Min;
            set
            {
                _Min = value;
                Value = _Value;
            }
        }

        public float Max
        {
            get => _Max;
            set
            {
                _Max = value;
                Value = _Value;
            }
        }

        public float Rate()
        {
            float k = _Max - _Min;
            if (k > 0) return (_Value - _Min) / k;
            return 0.0f;
        }

        public void SetRate(float rate)
        {
            Value = _Min + (_Max - _Min) * rate;
        }

        public void Up() { Value = _Max; }
        public void Down() { Value = _Min; }
        public bool IsUp() { return (_Value >= _Max); }
        public bool IsDown() { return (_Value <= _Min); }

        public CBoundFloat GetCopy() => new CBoundFloat(_Value, _Min, _Max);

        public CBoundFloat GetRandom()
        {
            float k = UnityEngine.Random.Range(_Min, _Max);
            return new CBoundFloat(k);
        }

        public float GetRandomValue()
        {
            return UnityEngine.Random.Range(_Min, _Max);
        }

        public int IntRate(int count)
        {
            float k = ((float)count) * Rate();
            int i = (int)k;
            if (i >= count) i = count - 1;
            if (i < 0) i = 0;
            return i;
        }

    }
}