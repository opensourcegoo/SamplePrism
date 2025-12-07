using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Core
{
    public class RegionTransitioningContent : TransitioningContent
    {
        public RegionTransitioningContent()
        {
            this.Loaded += (s, e) => OnContentChanged(Content, null);
        }

        //protected override void OnContentChanged(object oldContent, object newContent)
        //{
        //    base.OnContentChanged(oldContent, newContent);

        //    if (newContent != null && oldContent != null)
        //    {
        //        // 强制重新触发动画
        //        this.SetCurrentValue(RunTransitionProperty, false);
        //        this.Dispatcher.BeginInvoke(new Action(() =>
        //        {
        //            this.SetCurrentValue(RunTransitionProperty, true);
        //        }), System.Windows.Threading.DispatcherPriority.Loaded);
        //    }
        //}
    }
}
