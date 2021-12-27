using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Domain.Settings
{
    public class Feature
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public int Order { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public string ServicesPath { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
