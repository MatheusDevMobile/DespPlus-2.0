using System;
using System.Diagnostics;
using System.Reflection;
using PowerDev.Enterprise.Eureka.Extensoes;

namespace PowerDev.Enterprise.Eureka.Conversores
{
    public class EnumJsonConversor //: StringEnumConverter
    {
        //public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        //{
        //    if (value == null)
        //    {
        //        writer.WriteValue("");
        //        return;
        //    }

        //    Enum e = (Enum)value;

        //    string enumName = e.ToString("G");
        //    if (enumName == "-1")
        //    {
        //        writer.WriteValue("");
        //        return;
        //    }

        //    if (char.IsNumber(enumName[0]) || enumName[0] == '-')
        //    {
        //        // enum value has no name so write number
        //        writer.WriteValue(value);
        //    }
        //    else
        //    {
        //        //Type enumType = e.GetType();

        //        string finalName = e.valorComoString(); //EnumUtils.ToEnumName(enumType, enumName, CamelCaseText);

        //        writer.WriteValue(finalName);
        //    }
        //}

        //public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        //{
        //    bool isNullable = objectType.isNullable();// ReflectionUtils.IsNullableType(objectType);
        //    Type t = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;

        //    if (reader.TokenType == JsonToken.Null)
        //    {
        //        if (!isNullable)
        //            throw new JsonSerializationException("Cannot convert null value to ");//.Create(reader, "Cannot convert null value to {0}.");

        //        return null;
        //    }

        //    try
        //    {
        //        if (reader.TokenType == JsonToken.String)
        //        {
        //            string enumText = reader.Value.ToString();
        //            return Enum.Parse(t, enumText, true); //EnumUtils.ParseEnumName(enumText, isNullable, t);
        //        }

        //        if (reader.TokenType == JsonToken.Integer)
        //        {
        //            if (!AllowIntegerValues)
        //                throw new JsonSerializationException("Integer value {0} is not allowed."); //.Create(reader, "Integer value {0} is not allowed.".FormatWith(CultureInfo.InvariantCulture, reader.Value));

        //            return Enum.ToObject(t, reader.Value);// ConvertUtils.ConvertOrCast(reader.Value, CultureInfo.InvariantCulture, t);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //        throw new JsonSerializationException("Error converting value {0} to type '{1}'.");//.Create(reader, "Error converting value {0} to type '{1}'.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.FormatValueForPrint(reader.Value), objectType), ex);
        //    }

        //    // we don't actually expect to get here.
        //    throw new JsonSerializationException("Unexpected token {0} when parsing enum."); //.Create(reader, "Unexpected token {0} when parsing enum.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
        //}

        //public override bool CanConvert(Type objectType)
        //{
        //    Type t = objectType.isNullable()
        //                        ? Nullable.GetUnderlyingType(objectType)
        //                        : objectType;

        //    return t.GetTypeInfo().IsEnum;
        //}
    }
}