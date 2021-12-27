using System;
using System.Collections.Generic;
using System.Text;

namespace P79.Domain.Settings
{
    public class Module
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
        public List<Feature> Features { get; set; }
    }
}
