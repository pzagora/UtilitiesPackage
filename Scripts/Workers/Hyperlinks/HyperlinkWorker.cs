using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities.Attributes;
using Utilities.Constants;
using Utilities.Enums;

namespace Utilities.Workers.Hyperlinks
{
    // TODO: Implement HyperlinkType selection.
    
    [RequireComponent(typeof(TMP_Text))]
    public class HyperlinkWorker : Worker, IPointerClickHandler
    {
        #region FIELDS

        [Required] private TMP_Text _text;
        [Required] private Canvas _canvas;

        private Camera _camera;

        private HyperlinkType _hyperlinkType = HyperlinkType.Unknown;

        #endregion

        #region IMPLEMENTATION OF: MonoBehaviour

        private void Awake()
        {
            _text ??= GetComponent<TMP_Text>();
            _canvas ??= gameObject.GetComponentInParent<Canvas>();

            _camera = _canvas.renderMode == RenderMode.ScreenSpaceOverlay
                ? null
                : _canvas.worldCamera;
        }

        #endregion

        #region PUBLIC METHODS

        public void Remove()
        {
            Destroy(this);
        }

        /// <summary>
        /// This method is called when hyperlink in <see cref="TMP_Text"/> with <see cref="HyperlinkWorker"/> attached is clicked.
        /// Hyperlink should then be processed according to it's type via <see cref="ProcessHyperlink"/> method.
        /// </summary>
        /// <param name="eventData">Data passed on pointer click.</param>
        public void OnPointerClick(PointerEventData eventData)
        {
            _camera ??= Camera.main;

            var linkIndex = TMP_TextUtilities.FindIntersectingLink(_text, Input.mousePosition, _camera);
            if (linkIndex < 0 || linkIndex >= _text.textInfo.linkInfo.Length)
                return;

            var linkInfo = _text.textInfo.linkInfo[linkIndex];
            var linkID = linkInfo.GetLinkID();

            ProcessHyperlink(linkID);
        }

        #endregion

        #region PRIVATE METHODS

        private void ProcessHyperlink(string linkID)
        {
            switch (_hyperlinkType)
            {
                case HyperlinkType.OpenURL:
                    Application.OpenURL(linkID);
                    break;
                case HyperlinkType.Unknown:
                default:
                    throw new ArgumentOutOfRangeException(nameof(_hyperlinkType),
                        _hyperlinkType, ConstantMessages.HYPERLINK_UNKNOWN);
            }
        }

        #endregion
    }
}
