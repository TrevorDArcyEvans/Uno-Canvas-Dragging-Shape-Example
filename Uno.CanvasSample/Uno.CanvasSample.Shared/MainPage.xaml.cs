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

    // we already have 4 rectangles, so topmost in Z order will be 5
    // NOTE: this will eventually overflow but is good enough for a demo
    private int _currZindex = 5;

    public MainPage()
    {
      InitializeComponent();
    }

    private void Canvas_OnPointerMoved(object sender, PointerRoutedEventArgs e)
    {
      var currPt = e.GetCurrentPoint(null);
      MousePos.Text = $"({currPt.Position.X:0}, {currPt.Position.Y:0})";
    }

    private void Shape_OnMouseDown(object sender, PointerRoutedEventArgs e)
    {
      // start dragging
      _drag = true;

      // save start point of dragging
      _startPoint = e.GetCurrentPoint(canvas);

      // move selected rectangle to the top of the Z order
      var draggedRectangle = sender as Rectangle;
      Canvas.SetZIndex(draggedRectangle, _currZindex++);
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

      // save where we end up
      _startPoint = newPoint;
    }

    private void Shape_OnMouseUp(object sender, PointerRoutedEventArgs e)
    {
      // stop dragging
      _drag = false;
    }
  }
}
