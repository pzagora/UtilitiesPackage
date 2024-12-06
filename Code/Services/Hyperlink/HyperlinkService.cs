using System.Collections.Generic;
using TMPro;
using Utilities.Constants;
using Utilities.Enums;
using Utilities.Extensions;
using Utilities.Workers.Hyperlinks;

namespace Utilities.Services.Hyperlink
{
    // TODO: Refactor HyperlinkService
    
    public class HyperlinkService : IHyperlinkService
    {
        /// <summary>
        /// Constructor for <see cref="HyperlinkService"/>
        /// </summary>
        public HyperlinkService()
        {
            /*if (UtilityController.Instance.IsNull())
                return;

            UtilityController.Instance.RegisterManager(this, UtilitiesGroup.Managers, ConstantMessages.MANAGER_HYPERLINK);*/
        }

        public void EnableHyperlinks(List<TMP_Text> texts)
        {
            if (texts.IsNullOrEmpty()) 
                return;
            
            texts.ForEach(EnableHyperlinks);
        }

        public void EnableHyperlinks(TMP_Text text)
        {
            text.AddOrGetComponent<HyperlinkWorker>(out _);
        }
        
        public void DisableHyperlinks(List<TMP_Text> texts)
        {
            if (texts.IsNullOrEmpty()) 
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


