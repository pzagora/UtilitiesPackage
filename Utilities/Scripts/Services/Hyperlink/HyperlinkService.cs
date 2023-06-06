using System.Collections.Generic;
using TMPro;
using Utilities.Constants;
using Utilities.Enums;
using Utilities.Extensions;
using Utilities.Workers.Hyperlinks;

namespace Utilities.Services.Hyperlink
{
    public class HyperlinkService : IHyperlinkService
    {
        public HyperlinkService()
        {
            if (UtilityController.Instance.IsNull())
            {
                return;
            }

            UtilityController.Instance.RegisterManager(this, AppUtilsGroup.Managers, ConstantMessages.MANAGER_HYPERLINK);
        }

        public void EnableHyperlinks(List<TMP_Text> texts)
        {
            if (!texts.IsValid()) 
                return;
            
            texts.ForEach(EnableHyperlinks);
        }

        public void EnableHyperlinks(TMP_Text text)
        {
            if (!text.TryGetComponent(out HyperlinkWorker _))
            {
                text.gameObject.AddComponent<HyperlinkWorker>();
            }
        }
        
        public void DisableHyperlinks(List<TMP_Text> texts)
        {
            if (!texts.IsValid()) 
                return;
            
            texts.ForEach(DisableHyperlinks);
        }
        
        public void DisableHyperlinks(TMP_Text text)
        {
            if (text.TryGetComponent(out HyperlinkWorker worker))
            {
                worker.Remove();
            }
        }
    }
}


