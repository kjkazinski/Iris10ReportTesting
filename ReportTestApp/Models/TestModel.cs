using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testing.Models
{
    public class TestModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string table { get; set; }
        public IEnumerable<TestModel> items { get; set; }

        public TestModel Clone()
        {
            TestModel clone = new TestModel
            {
                id = this.id,
                text = this.text,
                table = this.table
            };
            return clone;
        }
    }
}