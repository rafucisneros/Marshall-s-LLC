using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salaries.Domain.AggregatesModel.PositionAggregate
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Division { get; set; }
    }
}
