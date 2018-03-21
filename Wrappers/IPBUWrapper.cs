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
    public class IPBUWrapper : ModelWrapper<IPBU>
    {
        public IPBUWrapper(IPBU model) : base(model)
        {
        }

        protected override void InitializeCollectionProperties(IPBU model)
        {
            if (model.JobAdders == null)
            {
                throw new ArgumentException("Adders cannot be empty");
            }
            JobAdders = new ChangeTrackingCollection<AdderWrapper>(
              model.JobAdders.Select(a => new AdderWrapper(a)));
            //pricing is 0'd unless current frame is set on instantiation
            foreach(var a in JobAdders)
            {
                a.CurrentFrame = this.SelectedFrame;
            }
            RegisterCollection(JobAdders, model.JobAdders);
        }

        #region property wraps

        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int IdOriginalValue => GetOriginalValue<int>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));


        public string ProjectName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string ProjectNameOriginalValue => GetOriginalValue<string>(nameof(ProjectName));
        public bool ProjectNameIsChanged => GetIsChanged(nameof(ProjectName));

        public string QuoteNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string QuoteNumberOriginalValue => GetOriginalValue<string>(nameof(QuoteNumber));
        public bool QuoteNumberIsChanged => GetIsChanged(nameof(QuoteNumber));

        public DateTime DateCreated
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        public DateTime DateCreatedOriginalValue => GetOriginalValue <DateTime>(nameof(DateCreated));
        public bool DateCreatedIsChanged => GetIsChanged(nameof(DateCreated));

        public DateTime LastEdited
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        public DateTime LastEditedOriginalValue => GetOriginalValue<DateTime>(nameof(LastEdited));
        public bool LastEditedIsChanged => GetIsChanged(nameof(LastEdited));

        public int RevisionNumber
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int RevisionNumberOriginalValue => GetOriginalValue<int>(nameof(RevisionNumber));
        public bool RevisionNumberIsChanged => GetIsChanged(nameof(RevisionNumber));

        public float Multiplier
        {
            get { return GetValue<float>(); }
            set { SetValue(value); }
        }

        public float MultiplierOriginalValue => GetOriginalValue<float>(nameof(Multiplier));
        public bool MultiplierIsChanged => GetIsChanged(nameof(Multiplier));

        public decimal Discount
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }

        public decimal DiscountOriginalValue => GetOriginalValue<decimal>(nameof(Discount));
        public bool DiscountIsChanged => GetIsChanged(nameof(Discount));

        public string SelectedFrame
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string SelectedFrameOriginalValue => GetOriginalValue<string>(nameof(SelectedFrame));
        public bool SelectedFrameIsChanged => GetIsChanged(nameof(SelectedFrame));

        public ChangeTrackingCollection<AdderWrapper> JobAdders { get; set; }

        public string[] Frames
        {
            get { return GetValue<string[]>(); }
            set { SetValue(value); }
        }

        public string RevisionLogHistory
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        #endregion

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(ProjectName))
            {
                yield return new ValidationResult("Project Name is a Required Field",
                  new[] { nameof(ProjectName) });
            }

            if (string.IsNullOrWhiteSpace(QuoteNumber))
            {
                yield return new ValidationResult("Quote Number is a Required Field",
                  new[] { nameof(QuoteNumber) });
            }
        }

    }
}
