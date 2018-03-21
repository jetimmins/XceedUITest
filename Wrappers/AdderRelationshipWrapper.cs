using ScopeSuite.Model;
using ScopeSuite.Wrapper.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopeSuite.Wrapper
{
    public class AdderRelationshipWrapper : ModelWrapper<AdderRelationship>
    {
        public AdderRelationshipWrapper(AdderRelationship model) : base(model)
        {
        }

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

    }
}
