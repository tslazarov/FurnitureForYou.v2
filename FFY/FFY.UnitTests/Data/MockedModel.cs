using FFY.Models.Contracts;

namespace FFY.UnitTests.Data
{
    public class MockedModel : IDeletableEntity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Name { get; set; }
    }
}
