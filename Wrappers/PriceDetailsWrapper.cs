using ScopeSuite.Model;
using ScopeSuite.Wrapper.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopeSuite.Wrapper
{
    public class PriceDetailsWrapper : ModelWrapper<PriceDetails>
    {
        public PriceDetailsWrapper(PriceDetails model) : base(model)
        { }
        
        public decimal OriginalListPriceByFrame
        {
            get { return GetValue<decimal>(); }
            set
            {
                SetValue(value);
                CurrentListPriceByFrame = OriginalListPriceByFrame;
            }
        }

        public decimal OriginalListPriceByFrameOriginalValue => GetOriginalValue<decimal>(nameof(OriginalListPriceByFrame));
        public bool OriginalListPriceByFrameIsChanged => GetIsChanged(nameof(OriginalListPriceByFrame));

        public decimal CurrentListPriceByFrame
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }

        public decimal CurrentListPriceByFrameOriginalValue => GetOriginalValue<decimal>(nameof(CurrentListPriceByFrame));
        public bool CurrentListPriceByFrameIsChanged => GetIsChanged(nameof(CurrentListPriceByFrame));

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (OriginalListPriceByFrame <= 0)
            {
                yield return new ValidationResult("Price cannot be 0 or negative",
                  new[] { nameof(OriginalListPriceByFrame) });
            }


        }


    }
}
