using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class InteractablePanelModel : BaseObjectScene, IInteractable
    {
        #region Fields

        [SerializeField] private string _messageIfTarget = "InteractablePanel";
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _highlightingColor = Color.grey;

        private WorldTextMessageController _worldMessageController;
        //private Renderer _renderer;
        private Material _material;

        private bool _isTarget;
        
        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            Renderer renderer = GetComponent<Renderer>();
            if (renderer)
            {
                _material = renderer.material;
                _normalColor = _material.color;
            }
            _worldMessageController = GetComponentInChildren<WorldTextMessageController>();
            //CustumDebug.Log("InteractablePanelModel->Awake:");
        }

        #endregion


        #region Methods

        private void HighlightingOn()
        {
            SetColor(_highlightingColor);
        }

        private void HighlightingOff()
        {
            SetColor(_normalColor);
        }

        //private Color GetColor()
        //{
        //    Color color = Color.gray;
        //    Renderer renderer = GetComponent<Renderer>();
        //    if (renderer)
        //    {
        //        color = renderer.material.color;
        //    }

        //    return color; 
        //}

        private void SetColor(Color color)
        {
            if (_material)
            {
                _material.color = color;
            }
        }

        #endregion


        #region IInteractable

        public string GetMessageIfTarget()
        {
            return LangManager.Instance.Text(TextConstants.UI_GROUP_ID,  _messageIfTarget);
        }

        public void Interact()
        {
            //CustumDebug.Log("InteractablePanelModel->Interact:");
            if (_worldMessageController)
            {
                _worldMessageController.StartBackCounting();
            }
        }

        public bool IsTarget
        {
            set
            {
                if (_isTarget != value)
                {
                    _isTarget = value;
                    if (value)
                    {
                        HighlightingOn();
                    }
                    else
                    {
                        HighlightingOff();
                    }
                }
            }
        }

        public InteractType InteractType => InteractType.ExternalUse;

        #endregion
    }
}
