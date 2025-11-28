using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SamplePrism.Core
{
    //public class AnimatedContentControlRegionAdapter : RegionAdapterBase<ContentControl> 可以
    public class AnimatedContentControlRegionAdapter : RegionAdapterBase<ContentControl>
    {
        public AnimatedContentControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {

        }

        protected override void Adapt(IRegion region, ContentControl regionTarget)
        {
            throw new NotImplementedException();
        }

        protected override IRegion CreateRegion()
        {
            throw new NotImplementedException();
        }
    }
}
