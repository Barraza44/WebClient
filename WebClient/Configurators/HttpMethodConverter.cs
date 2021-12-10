using System;
using System.ComponentModel;
using System.Globalization;

namespace WebClient.Configurators
{
    [TypeConverter(typeof(HttpMethodConverter))]
    public class HttpMethodConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            object result = null;
            var stringValue = value as string;
            if (string.IsNullOrWhiteSpace(stringValue)) return null;

            result = stringValue.ToLowerInvariant() switch
            {
                "get" => HttpMethod.Get,
                "post" => HttpMethod.Post,
                "put" => HttpMethod.Put,
                "delete" => HttpMethod.Delete,
                "patch" => HttpMethod.Patch,
                _ => null
            };

            return result ?? base.ConvertFrom(context, culture, value);
        }
    }
}