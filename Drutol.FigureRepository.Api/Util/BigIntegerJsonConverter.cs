using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Drutol.FigureRepository.Api.Util;

public class BigIntegerJsonConverter : JsonConverter<BigInteger>
{
    public override BigInteger Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetInt32();
    }

    public override void Write(Utf8JsonWriter writer, BigInteger value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue((int)value);
    }
}