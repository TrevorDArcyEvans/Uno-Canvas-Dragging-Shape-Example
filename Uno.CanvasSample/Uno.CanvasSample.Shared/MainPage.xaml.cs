namespace Uno.CanvasSample
{
  using Windows.UI.Input;
  using Windows.UI.Xaml.Controls;
  using Windows.UI.Xaml.Input;
  using Windows.UI.Xaml.Shapes;
  using Windows.UI.Xaml;
  using Windows.UI.Xaml.Media;

  public sealed partial class MainPage
  {
    public MainPage()
    {
      InitializeComponent();
    }

    private void Canvas_OnPointerMoved(object sender, PointerRoutedEventArgs e)
    {
      var currPt = e.GetCurrentPoint(null);
      MousePos.Text = $"({currPt.Position.X:0}, {currPt.Position.Y:0})@{((CompositeTransform) canvas.RenderTransform).ScaleX}";
    }

    #region Shape

    private bool _drag;
    private PointerPoint _startPoint;

    // we already have 4 rectangles, so topmost in Z order will be 5
    // NOTE: this will eventually overflow but is good enough for a demo
    private int _currZindex = 5;

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

      UpdateLine(draggedRectangle);

      // save where we end up
      _startPoint = newPoint;
    }

    private void UpdateLine(Rectangle draggedRectangle)
    {
      if (draggedRectangle == Rectangle1)
      {
        Line12.X1 = Canvas.GetLeft(Rectangle1) + Rectangle1.Width / 2;
        Line12.Y1 = Canvas.GetTop(Rectangle1) + Rectangle1.Height / 2;
      }

      if (draggedRectangle == Rectangle2)
      {
        Line12.X2 = Canvas.GetLeft(Rectangle2) + Rectangle2.Width / 2;
        Line12.Y2 = Canvas.GetTop(Rectangle2) + Rectangle2.Height / 2;
      }
    }

    private void Shape_OnMouseUp(object sender, PointerRoutedEventArgs e)
    {
      // stop dragging
      _drag = false;
    }

    #endregion

    #region Zoom

    private const double ZoomInc = 0.1;

    private void Zoom_In(object sender, RoutedEventArgs e)
    {
      var ct = (CompositeTransform) canvas.RenderTransform;
      ct.ScaleX += ZoomInc;
      ct.ScaleY += ZoomInc;
    }

    private void Zoom_Fit(object sender, RoutedEventArgs e)
    {
      var ct = (CompositeTransform) canvas.RenderTransform;
      ct.ScaleX = 1.0;
      ct.ScaleY = 1.0;
    }

    private void Zoom_Out(object sender, RoutedEventArgs e)
    {
      var ct = (CompositeTransform) canvas.RenderTransform;
      ct.ScaleX -= ZoomInc;
      ct.ScaleY -= ZoomInc;
    }

    #endregion

    #region Translate

    private const double TranslateInc = 10d;

    private void Translate_Up(object sender, RoutedEventArgs e)
    {
      var ct = (CompositeTransform) canvas.RenderTransform;
      ct.TranslateY -= TranslateInc;
    }

    private void Translate_Down(object sender, RoutedEventArgs e)
    {
      var ct = (CompositeTransform) canvas.RenderTransform;
      ct.TranslateY += TranslateInc;
    }

    private void Translate_Left(object sender, RoutedEventArgs e)
    {
      var ct = (CompositeTransform) canvas.RenderTransform;
      ct.TranslateX -= TranslateInc;
    }

    private void Translate_Right(object sender, RoutedEventArgs e)
    {
      var ct = (CompositeTransform) canvas.RenderTransform;
      ct.TranslateX += TranslateInc;
    }

    private void Translate_Reset(object sender, RoutedEventArgs e)
    {
      var ct = (CompositeTransform) canvas.RenderTransform;
      ct.TranslateX = 0;
      ct.TranslateY = 0;
    }

    #endregion
  }
}
