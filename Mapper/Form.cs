using System.Collections.Generic;

namespace Mapper
{
    public class Form
    {
        public string FormName { get; set; }

        public IEnumerable<Field> Fields { get; set; }
    }
}