
namespace Extensions.Core.Conversion
{
    public static class ConversionExtension
    {
        public static ConvertedValue<TDataType> ConvertTo<TDataType>(this string value)
        {
            dynamic defaultValue = default(TDataType);
            ConvertedValue<TDataType> convertedValue;
            if (value != null)
            {
                convertedValue = ConvertValue(defaultValue, value);
            }
            else
            {
                convertedValue = GetConvertedValue<TDataType>();
            }
            return convertedValue;
        }

        private static ConvertedValue<int> ConvertValue(int defaultValue, string value)
        {
            ConvertedValue<int> convertedObject;
            int convertedValue;
            if (int.TryParse(value, out convertedValue))
            {
                convertedObject = GetConvertedValue(convertedValue);
            }
            else
            {
                convertedObject = GetConvertedValue<int>();
            }
            return convertedObject;
        }

        private static ConvertedValue<float> ConvertValue(float defaultValue, string value)
        {
            ConvertedValue<float> convertedObject;
            float convertedValue;
            if (float.TryParse(value, out convertedValue))
            {
                convertedObject = GetConvertedValue(convertedValue);
            }
            else
            {
                convertedObject = GetConvertedValue<float>();
            }
            return convertedObject;
        }

        private static ConvertedValue<double> ConvertValue(double defaultValue, string value)
        {
            ConvertedValue<double> convertedObject;
            double convertedValue;
            if (double.TryParse(value, out convertedValue))
            {
                convertedObject = GetConvertedValue(convertedValue);
            }
            else
            {
                convertedObject = GetConvertedValue<double>();
            }
            return convertedObject;
        }

        private static ConvertedValue<decimal> ConvertValue(decimal defaultValue, string value)
        {
            ConvertedValue<decimal> convertedObject;
            decimal convertedValue;
            if (decimal.TryParse(value, out convertedValue))
            {
                convertedObject = GetConvertedValue(convertedValue);
            }
            else
            {
                convertedObject = GetConvertedValue<decimal>();
            }
            return convertedObject;
        }

        private static ConvertedValue<bool> ConvertValue(bool defaultValue, string value)
        {
            ConvertedValue<bool> converted;
            if (value.ToLower().Equals("true"))
            {
                converted = GetConvertedValue(true);
            }
            else if (value.ToLower().Equals("false"))
            {
                converted = GetConvertedValue(false);
            }
            else
            {
                converted = GetConvertedValue<bool>();
            }
            return converted;
        } 

        private static ConvertedValue<object> ConvertValue(object defaultValue, string value)
        {
            var convertedValue = value;
            return new ConvertedValue<object>(convertedValue);
        }

        private static ConvertedValue<TDataType> GetConvertedValue<TDataType>()
        {
            return new ConvertedValue<TDataType>();
        }

        private static ConvertedValue<TDataType> GetConvertedValue<TDataType>(TDataType value)
        {
            return new ConvertedValue<TDataType>(value);
        }
    }
}
