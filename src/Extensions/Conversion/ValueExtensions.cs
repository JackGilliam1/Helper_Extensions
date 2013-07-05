
namespace Extensions.Core.Conversion
{
    public static class ValueExtensions
    {
        public static bool IsEmpty(this int value)
        {
            return value == default(int);
        }
        public static bool IsEmpty(this float value)
        {
            return value == default(float);
        }
        public static bool IsEmpty(this double value)
        {
            return value == default(double);
        }
        public static bool IsEmpty(this decimal value)
        {
            return value == default(decimal);
        }
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        public static bool IsEmpty(this object value)
        {
            return value == null;
        }

        public static bool InRange(this int value, int min, int max, bool inclusive = true)
        {
            bool inRange;
            if (inclusive)
            {
                inRange = value <= max && value >= min;
            }
            else
            {
                inRange = value < max && value > min;
            }
            return inRange;
        }
        public static bool InRange<TDataType>(this int value, double min, double max, bool inclusive = true)
        {
            return InRange(value, (int)min, (int)max, inclusive);
        }
        public static bool InRange(this double value, double min, double max, bool inclusive = true)
        {
            bool inRange;
            if (inclusive)
            {
                inRange = value <= max && value >= min;
            }
            else
            {
                inRange = value < max && value > min;
            }
            return inRange;
        }
        public static bool InRange(this float value, float min, float max, bool inclusive = true)
        {
            bool inRange;
            if (inclusive)
            {
                inRange = value <= max && value >= min;
            }
            else
            {
                inRange = value < max && value > min;
            }
            return inRange;
        }
        public static bool InRange<TDataType>(this float value, double min, double max, bool inclusive = true)
        {
            return InRange(value, (float)(object)min, (float)(object)max, inclusive);
        }
        public static bool InRange(this decimal value, decimal min, decimal max, bool inclusive = true)
        {
            bool inRange;
            if (inclusive)
            {
                inRange = value <= max && value >= min;
            }
            else
            {
                inRange = value < max && value > min;
            }
            return inRange;
        }
        public static bool InRange<TDataType>(this decimal value, double min, double max, bool inclusive = true)
        {
            return InRange(value, (decimal)(object)min, (decimal)(object)max, inclusive);
        }
        public static bool InRange(dynamic value, double min, double max, bool inclusive = true)
        {
            return InRange(value, min, max, inclusive);
        }
    }
}
