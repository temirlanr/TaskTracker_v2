using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker_v2.Dtos
{
    public class ProjectUpdateDto
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset CompleteDate { get; set; }
    }
}
