using Credit.Core.Entities.Abstract;
using Credit.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credit.Entities.Dtos
{
    public class CalcCreditListDto : DtoGetBase
    {
        public IList<CalcCredit>? CalcCredits { get; set; }
        public double? TotalAmount { get; set; }
        public double? TotalInterest { get; set; }


    }
}
