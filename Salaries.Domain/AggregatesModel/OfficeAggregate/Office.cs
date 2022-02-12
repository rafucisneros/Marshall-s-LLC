using System.ComponentModel.DataAnnotations;

namespace Salaries.Domain.AggregatesModel.OfficeAggregate
{
    public class Office
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
