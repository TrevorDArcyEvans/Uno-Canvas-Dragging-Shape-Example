namespace Uno.CanvasSample
{
  using Windows.UI.Input;
  using Windows.UI.Xaml.Controls;
  using Windows.UI.Xaml.Input;
  using Windows.UI.Xaml.Shapes;

  public sealed partial class MainPage
  {
    private bool _drag;
    private PointerPoint _startPoint;

    public MainPage()
    {
      InitializeComponent();
    }

    private void Canvas_OnPointerMoved(object sender, PointerRoutedEventArgs e)
    {
      var currPt = e.GetCurrentPoint(null);
      txt.Text = $"({currPt.Position.X:0}, {currPt.Position.Y:0})";
    }

    private void Shape_OnMouseDown(object sender, PointerRoutedEventArgs e)
    {
      // start dragging
      _drag = true;

      // save start point of dragging
      _startPoint = e.GetCurrentPoint(canvas);
    }

    private void Shape_OnMouseMove(object sender, PointerRoutedEventArgs e)
    {
      if (!_drag)
      {
        return;
      }

      // if dragging, then adjust rectangle position based on mouse movement
      var draggedRectangle = sender as Rectangle;
      var left = Canvas.GetLeft(draggedRectangle);
      var top = Canvas.GetTop(draggedRectangle);
      var newPoint = e.GetCurrentPoint(canvas);
      Canvas.SetLeft(draggedRectangle, left + (newPoint.RawPosition.X - _startPoint.RawPosition.X));
      Canvas.SetTop(draggedRectangle, top + (newPoint.RawPosition.Y - _startPoint.RawPosition.Y));

      _startPoint = newPoint;
    }

    private void Shape_OnMouseUp(object sender, PointerRoutedEventArgs e)
    {
      // stop dragging
      _drag = false;
    }
  }
}
