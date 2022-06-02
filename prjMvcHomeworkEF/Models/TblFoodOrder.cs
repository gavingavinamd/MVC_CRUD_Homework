using System;
using System.Collections.Generic;

namespace prjMvcHomeworkEF.Models
{
    public partial class TblFoodOrder
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int Foodld { get; set; }
        public DateTime OrderDateTime { get; set; }
        public DateTime? PaidDateTime { get; set; }
    }
}
