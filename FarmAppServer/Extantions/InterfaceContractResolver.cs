using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppServer.Extantions
{
    public class InterfaceContractResolver<TInterface> : DefaultContractResolver where TInterface : class
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(typeof(TInterface), memberSerialization);
            return properties;
        }
    }
}
