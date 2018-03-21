using ScopeSuite.Model;
using ScopeSuite.Wrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Utility;

namespace UITestGround.DataProvider
{
    public class TestDataProvider
    {
        private List<Adder> ModelPriceBook;
        private List<IPBU> ModelIPBUCollection;
        private Adder RandomAdderFromPriceBook { get => ModelPriceBook[RNG.Between(0, PriceBook.Count)]; }
        private IPBU RandomIPBUFromCollection { get => ModelIPBUCollection[RNG.Between(0, IPBUCollection.Count)]; }

        public TestDataProvider()
        {
            ModelPriceBook = Adder.RandomPriceBook();
            ModelIPBUCollection = IPBU.RandomIPBUCollection();
            PopulateIPBUModelJobAdders();
            PriceBook = GenerateWrappedPriceBook();
            IPBUCollection = GenerateWrappedIPBUCollection();
        }

        public readonly List<AdderWrapper> PriceBook;
        public readonly List<IPBUWrapper> IPBUCollection;
        public IPBUWrapper RandomIPBUWrapperFromCollection { get => IPBUCollection[RNG.Between(0, IPBUCollection.Count)]; }
        public AdderWrapper RandomAdderWrapperFromPriceBook { get => PriceBook[RNG.Between(0, PriceBook.Count)]; }

        //****HELPER METHODS
        private List<AdderWrapper> GenerateWrappedPriceBook()
        {
            return ModelPriceBook.Select(m => new AdderWrapper(m)).ToList();
        }

        //IPBUWrapper itself takes care of wrapping its own collection of AdderModels in JobAdders
        private List<IPBUWrapper> GenerateWrappedIPBUCollection()
        {
            return ModelIPBUCollection.Select(m => new IPBUWrapper(m)).ToList();
        }

        private void PopulateIPBUModelJobAdders()
        {
            ModelIPBUCollection.ForEach(m => m.JobAdders.AddRange(RangeOfRandomAddersFromModelPriceBook(10)));
        }

        private List<Adder> RangeOfRandomAddersFromModelPriceBook(int size)
        {
            List<Adder> range = new List<Adder>();
            int len = ModelPriceBook.Count - 1;
            for(int i = 0; i <= size; i++)
            {
                range.Add(ModelPriceBook[RNG.Between(0, len)]);
            }
            return range;
        }
        //****END HELPER METHODS

    }
}
