using MvvmCross.ViewModels;
using MvxStarter.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvxStarter.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<HomeViewModel>();
        }
    }
}
