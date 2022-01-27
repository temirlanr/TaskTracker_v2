using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker_v2.Extensions
{
    public class ProjectRetrievalOptions
    {
        public Filter Filter { get; set; }
        public SortOrder SortOrder { get; set; }
    }

    public class Filter
    {
        public FilterOperator Operator { get; set; }
        public string Value { get; set; }
    }

    public enum FilterOperator
    {
        EQ, // Equal
        GTE, // Greater than or equal
        LTE, // Less than or equal
    }

    public enum SortOrder
    {
        ASC, // Ascending
        DESC // Descending
    }

}
