using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Monbsoft.Feeader.Avalonia.Models
{
    public class Category
    {
        public Category(string name)
            : this(Guid.NewGuid().ToString(), name)
        {            
        }        
        [JsonConstructor]
        public Category(string id, string name)
        {
            Id = id;
            Name = name;
        }
        public string Id { get; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
