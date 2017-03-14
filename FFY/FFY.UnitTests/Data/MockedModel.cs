using FFY.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.UnitTests.Data
{
    public class MockedModel : IDeletableEntity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Name { get; set; }
    }
}
