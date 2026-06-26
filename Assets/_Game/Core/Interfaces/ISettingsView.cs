namespace TaskbarBreakout.Core
{
    public interface ISettingsView
    {
        bool IsVisible { get; }
        void Show();
        void Hide();
    }
}
