namespace Finance.Infrastructure.Data.Core
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.Reflection;

    public class DeserializeContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(
            MemberInfo member,
            MemberSerialization memberSerialization)
        {
            var property = base
                .CreateProperty(member, memberSerialization);

            if (property.PropertyType.Name == "Maybe`1")
            {
                property.ShouldSerialize =
                    instance =>
                    {
                        var value = instance.GetType().GetProperty(property.PropertyName)
                            .GetValue(instance, null).ToString();

                        return !value.Equals("No value");
                    };
            }

            return property;
        }
    }
}