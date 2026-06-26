namespace TaskbarBreakout.Core
{
    public interface IWindowDraggable
    {
        bool IsDragging { get; }
        void BeginDrag();
        void EndDrag();
    }
}
