using System;
using System.Collections.Generic;

namespace prjMvcHomeworkEF.Models
{
    public partial class TblHero
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Atk { get; set; }
        public int Hp { get; set; }
    }
}
