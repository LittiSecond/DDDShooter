namespace Geekbrains
{
    public abstract class BaseController
    {
        protected UiInterface UiInterface;
        protected BaseController()
        {
            UiInterface = new UiInterface();
        }
        
        public bool IsActive { get; private set; }

        public virtual void On()
        {
            On(null);
        }

        public virtual void On(params BaseObjectScene[] obj)
        {
            IsActive = true;
        }

        public virtual void Off()
        {
            IsActive = false;
        }

        public void Switch(params BaseObjectScene[] obj)
        {
            if (!IsActive)
            {
                On(obj);
            }
            else
            {
                Off();
            }
        }
    }
}
