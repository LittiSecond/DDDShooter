using Geekbrains;

namespace DddShooter
{
    public sealed class PauseMenuFake : PauseMenu
    {
        // pattern Proxy
        // it's need in testing scene, where don't need true PauseMenu


        protected override void Start()
        {
            // don't need to do anything
        }

        public override void Show()
        {
            // don't need to do anything
        }

        public override void Hide()
        {
            // don't need to do anything
        }

    }
}
