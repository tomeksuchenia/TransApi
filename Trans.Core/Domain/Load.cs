using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Core.Domain
{
    public class Load
    {
        public string NameLoads { get; protected set; }
        public int Weight { get; protected set; }
        public int Width { get; protected set; }
        public int Length { get; protected set; }
        public int Height { get; protected set; }
        public string Description { get; protected set; }

        private Load(string nameLoads, int weight, int width, int length, int height, string description)
        {
            NameLoads = nameLoads;
            Weight = weight;
            Width = width;
            Length = length;
            Height = height;
            Description = description;
        }


        public static Load Create(string nameLoads, int weight, int width, int length, int height, string description) 
            => new Load(nameLoads, weight, width, length, height, description);
    }
}
