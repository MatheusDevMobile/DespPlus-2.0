using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DespPlus.Helpers
{
    public class ConstructorParameters
    {
        protected ConstructorParameters()
        {
            Parameters = new Dictionary<string, object>();
        }

        protected IDictionary<string, object> Parameters { get; set; }
        public static ConstructorParameters Init(string key, object value)
        {
            var r = new ConstructorParameters();
            r.Parameters.Add(key, value);
            return r;
        }
        public ConstructorParameters AddItem(string key, object value)
        {
            Parameters.Add(key, value);
            return this;
        }
        public IReadOnlyDictionary<string, object> GenerateParameters()
        {
            return new ReadOnlyDictionary<string, object>(Parameters);
        }
    }
}
