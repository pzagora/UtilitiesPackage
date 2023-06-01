using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities.Constants;
using Utilities.Enums;

namespace Utilities.Workers.Hyperlinks
{
    [RequireComponent(typeof(TMP_Text))]
    public class HyperlinkWorker : Worker, IPointerClickHandler
    {
        [Required] private TMP_Text _text;
        [Required] private Camera _camera;
        [Required] private Canvas _canvas;
        
        private HyperlinkType _hyperlinkType = HyperlinkType.Unknown;

        private void Awake()
        {
            _text ??= GetComponent<TMP_Text>();
            _camera ??= Camera.main;
            _canvas ??= gameObject.GetComponentInParent<Canvas>();
            
            _camera = _canvas.renderMode == RenderMode.ScreenSpaceOverlay 
                ? null 
                : _canvas.worldCamera;
        }

        public void Remove()
        {
            Destroy(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var linkIndex = TMP_TextUtilities.FindIntersectingLink(_text, Input.mousePosition, _camera);
            if (linkIndex < 0 || linkIndex >= _text.textInfo.linkInfo.Length) 
                return;

            var linkInfo = _text.textInfo.linkInfo[linkIndex];
            var linkID = linkInfo.GetLinkID();

            ProcessHyperlink(linkID);
        }

        private void ProcessHyperlink(string linkID)
        {
            switch (_hyperlinkType)
            {
                case HyperlinkType.OpenURL:
                    Application.OpenURL(linkID);
                    break;
                case HyperlinkType.Unknown:
                default:
                    throw new ArgumentOutOfRangeException(nameof(_hyperlinkType), _hyperlinkType, ConstantMessages.HYPERLINK_UNKNOWN);
            }
        }
    }
}
