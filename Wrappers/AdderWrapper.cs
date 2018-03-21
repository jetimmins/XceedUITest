using ScopeSuite.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;
using ScopeSuite.Wrapper.Base;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace ScopeSuite.Wrapper
{
    public class AdderWrapper : ModelWrapper<Adder>
    {
        public AdderWrapper(Adder model) : base(model)
        {
            currentFrame = "A1";
        }

       //After profiling it seems that the performance overhead is actually the table and row summaries of the datagrid.
       //I tried many different alternative solutions to wrapping the prices, the only one remaining is putting an ObservableCollection in the Model...
       //For now using a LostFocus event on the datagrid to refresh filters which updates all the summaries. This happens every time you click the grid.
            
        protected override void InitializeCollectionProperties(Adder model)
        {
            if(model.AdderPrices == null)
            {
                throw new ArgumentException("Adder Prices cannot be null");
            }
            AdderPrices = new ChangeTrackingCollection<PriceDetailsWrapper>(model.AdderPrices.Select(ap => new PriceDetailsWrapper(ap)));
            RegisterCollection(AdderPrices, model.AdderPrices);

            if(model.ConflictingRelationships == null)
            {
                throw new ArgumentException("Conflicting Relationships cannot be null");
            }
            ConflictingRelationships = new ChangeTrackingCollection<AdderRelationshipWrapper>(model.ConflictingRelationships.Select(ar => new AdderRelationshipWrapper(ar)));
            RegisterCollection(ConflictingRelationships, model.ConflictingRelationships);

            if (model.DependentRelationships == null)
            {
                throw new ArgumentException("Dependent Relationships cannot be null");
            }
            DependentRelationships = new ChangeTrackingCollection<AdderRelationshipWrapper>(model.DependentRelationships.Select(ar => new AdderRelationshipWrapper(ar)));
            RegisterCollection(DependentRelationships, model.DependentRelationships);
        }
        

        public ChangeTrackingCollection<PriceDetailsWrapper> AdderPrices { get; set; }

        public ChangeTrackingCollection<AdderRelationshipWrapper> ConflictingRelationships { get; set; }
        public ChangeTrackingCollection<AdderRelationshipWrapper> DependentRelationships { get; set; }

        public PriceDetailsWrapper CurrentPriceDetails { get; set; }

        string currentFrame;
        public string CurrentFrame
        {
            get
            {
                return currentFrame;
            }
            set
            {
                UpdatePrice(value);
            }
        }
        private void UpdatePrice(string value)
        {
            switch(value)
            {
                case "A1":
                    CurrentPriceDetails = AdderPrices[0]; 
                    break;
                case "BH":
                    CurrentPriceDetails = AdderPrices[1];
                    break;
                case "CH":
                    CurrentPriceDetails = AdderPrices[2];
                    break;
                case "D":
                    CurrentPriceDetails = AdderPrices[3];
                    break;
                default:
                    CurrentPriceDetails = AdderPrices[0];
                    break;
            }
        }

        #region property wraps

        //simple
        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int IdOriginalValue => GetOriginalValue<int>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));

        public string Description
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string DescriptionOriginalValue => GetOriginalValue<string>(nameof(Description));
        public bool DescriptionIsChanged => GetIsChanged(nameof(Description));

        public string UserNote
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string UserNoteOriginalValue => GetOriginalValue<string>(nameof(UserNote));
        public bool UserNoteIsChanged => GetIsChanged(nameof(UserNote));

        public string PriceBookCode
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string PriceBookCodeOriginalValue => GetOriginalValue<string>(nameof(PriceBookCode));
        public bool PriceBookCodeIsChanged => GetIsChanged(nameof(PriceBookCode));

        public string ClauseReference
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string ClauseReferenceOriginalValue => GetOriginalValue<string>(nameof(ClauseReference));
        public bool ClauseReferenceIsChanged => GetIsChanged(nameof(ClauseReference));

        public string SpecReference
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string SpecReferenceOriginalValue => GetOriginalValue<string>(nameof(SpecReference));
        public bool SpecReferenceIsChanged => GetIsChanged(nameof(SpecReference));
             public bool IsSpecialDutyRequirement
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }
        public bool IsSpecialDutyRequirementOriginalValue => GetOriginalValue<bool>(nameof(IsSpecialDutyRequirement));
        public bool IsSpecialDutyRequirementIsChanged => GetIsChanged(nameof(IsSpecialDutyRequirement));

        public bool IsBasicDutyRequirement
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }
        public bool IsBasicDutyRequirementOriginalValue => GetOriginalValue<bool>(nameof(IsBasicDutyRequirement));
        public bool IsBasicDutyRequirementIsChanged => GetIsChanged(nameof(IsBasicDutyRequirement));

        public bool IsSpecialDutyBullet
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }
        public bool IsSpecialDutyBulletOriginalValue => GetOriginalValue<bool>(nameof(IsSpecialDutyBullet));
        public bool IsSpecialDutyBulletIsChanged => GetIsChanged(nameof(IsSpecialDutyBullet));

        public bool IsBasicDutyBullet
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }
        public bool IsBasicDutyBulletOriginalValue => GetOriginalValue<bool>(nameof(IsBasicDutyBullet));
        public bool IsBasicDutyBulletIsChanged => GetIsChanged(nameof(IsBasicDutyBullet));

        //call the pricing setters
        public bool IsSpreadAcrossUnits
        {
            get { return GetValue<bool>(); }
            set
            {
                SetValue(value);
                ChangeDiscount(DiscountLevel);
                ChangeQuantity(Quantity);
            }
        }
        public bool IsSpreadAcrossUnitsOriginalValue => GetOriginalValue<bool>(nameof(IsSpreadAcrossUnits));
        public bool IsSpreadAcrossUnitsIsChanged => GetIsChanged(nameof(IsSpreadAcrossUnits));

        public Category Category
        {
            get { return GetValue<Category>(); }
            set { SetValue(value); }
        }
        public Category CategoryOriginalValue => GetOriginalValue<Category>(nameof(Category));
        public bool CategoryIsChanged => GetIsChanged(nameof(Category));


        //issues with quantity: multiple identical entries of adders share the same quantity - that should be fine as there should never be duplicates 
        //this will be solved with adder validation 
        public int Quantity
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
                ChangeQuantity(value);
                
            }
        }

        public int QuantityOriginalValue => GetOriginalValue<int>(nameof(Quantity));
        public bool QuantityIsChanged => GetIsChanged(nameof(Quantity));
        private void ChangeQuantity(int value)
        {
            for (int i = 0; i < AdderPrices.Count; i++)
            {
                //respects the current discount
                AdderPrices[i].CurrentListPriceByFrame = AdderPrices[i].OriginalListPriceByFrame * (1 - DiscountLevel);
                if(!IsSpreadAcrossUnits)
                AdderPrices[i].CurrentListPriceByFrame *= value;
                else
                AdderPrices[i].CurrentListPriceByFrame = value;
            }
        }

        //no changed or validity checking on this as it's duplicated from IPBU 
        public decimal DiscountLevel
        {
            get { return GetValue<decimal>(); }
            set
            {
                SetValue(value);
                ChangeDiscount(value);
            }
        }

        private void ChangeDiscount(decimal value)
        {
            for (int i = 0; i < AdderPrices.Count; i++)
            {
                //respects the current quantity
                if (!IsSpreadAcrossUnits)
                    AdderPrices[i].CurrentListPriceByFrame = AdderPrices[i].OriginalListPriceByFrame * Quantity;
                else
                    AdderPrices[i].CurrentListPriceByFrame = AdderPrices[i].OriginalListPriceByFrame;

                AdderPrices[i].CurrentListPriceByFrame *= (1 - value);
            }
        }

       #endregion

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Description))
            {
                yield return new ValidationResult("Description cannot be empty",
                  new[] { nameof(Description) });
            }

           
        }

        //if required...

        /*
     public decimal A1Price
     {
         get { return GetValue<decimal>(); }
         set { SetValue(value); }
     }
     public decimal A1PriceOriginalValue => GetOriginalValue<decimal>(nameof(A1Price));
     public bool A1PriceIsChanged => GetIsChanged(nameof(A1Price));

     public decimal BHPrice
     {
         get { return GetValue<decimal>(); }
         set { SetValue(value); }
     }
     public decimal BHPriceOriginalValue => GetOriginalValue<decimal>(nameof(BHPrice));
     public bool BHPriceIsChanged => GetIsChanged(nameof(BHPrice));

     public decimal CHPrice
     {
         get { return GetValue<decimal>(); }
         set { SetValue(value); }
     }
     public decimal CHPriceOriginalValue => GetOriginalValue<decimal>(nameof(CHPrice));
     public bool CHPriceIsChanged => GetIsChanged(nameof(CHPrice));

     public decimal DPrice
     {
         get { return GetValue<decimal>(); }
         set { SetValue(value); }
     }
     public decimal DPriceOriginalValue => GetOriginalValue<decimal>(nameof(DPrice));
     public bool DPriceIsChanged => GetIsChanged(nameof(DPrice));

     public decimal OriginalA1Price
     {
         get { return GetValue<decimal>(); }
         set { SetValue(value); A1Price = value; }
     }
     public decimal OriginalA1PriceOriginalValue => GetOriginalValue<decimal>(nameof(OriginalA1Price));
     public bool OriginalA1PriceIsChanged => GetIsChanged(nameof(OriginalA1Price));

     public decimal OriginalBHPrice
     {
         get { return GetValue<decimal>(); }
         set { SetValue(value); BHPrice = value; }
     }
     public decimal OriginalBHPriceOriginalValue => GetOriginalValue<decimal>(nameof(OriginalBHPrice));
     public bool OriginalBHPriceIsChanged => GetIsChanged(nameof(OriginalBHPrice));

     public decimal OriginalCHPrice
     {
         get { return GetValue<decimal>(); }
         set { SetValue(value); CHPrice = value; }
     }
     public decimal OriginalCHPriceOriginalValue => GetOriginalValue<decimal>(nameof(OriginalCHPrice));
     public bool OriginalCHPriceIsChanged => GetIsChanged(nameof(OriginalCHPrice));

     public decimal OriginalDPrice
     {
         get { return GetValue<decimal>(); }
         set { SetValue(value); DPrice = value; }
     }
     public decimal OriginalDPriceOriginalValue => GetOriginalValue<decimal>(nameof(OriginalDPrice));
     public bool OriginalDPriceIsChanged => GetIsChanged(nameof(OriginalDPrice));
     */



        /*
        public decimal[] ListPriceByFrame
        {
            get { return GetValue< decimal[] > (); }
            set { SetValue(value); }
        }

        public decimal[] ListPriceByFrameOriginalValue => GetOriginalValue<decimal[]>(nameof(ListPriceByFrame));
        public bool ListPriceByFrameIsChanged => GetIsChanged(nameof(ListPriceByFrame));

        public decimal[] OriginalListPriceByFrame
        {
            get { return GetValue<decimal[]>(); }
            set { SetValue(value); }
        }

        public decimal[] OriginalListPriceByFrameOriginalValue => GetOriginalValue<decimal[]>(nameof(OriginalListPriceByFrame));
        public bool OriginalListPriceByFrameIsChanged => GetIsChanged(nameof(OriginalListPriceByFrame));
         */

    }
}
