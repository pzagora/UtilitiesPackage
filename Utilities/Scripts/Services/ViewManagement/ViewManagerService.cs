using System;
using System.Collections.Generic;
using Utilities.Constants;
using Utilities.Enums;
using Utilities.Extensions;
using Utilities.Services.ViewManagement.Views;

namespace Utilities.Services.ViewManagement
{
    public class ViewManagerService : IViewManagerService
    {
        private List<ViewBehaviour> _allViews;

        public ViewManagerService()
        {
            if (UtilityController.Instance.IsNull())
            {
                return;
            }

            UtilityController.Instance.RegisterManager(this, AppUtilsGroup.Managers, ConstantMessages.MANAGER_VIEWS);
        }
        
        public T Show<T>() where T : ViewBehaviour
        {
            throw new NotImplementedException();
        }

        public void Close(ViewBehaviour view)
        {
            throw new NotImplementedException();
        }

        public T Get<T>() where T : ViewBehaviour
        {
            throw new NotImplementedException();
        }
    }
}


