using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace Uno.CanvasSample.Skia.Tizen
{
  class Program
  {
    static void Main(string[] args)
    {
      var host = new TizenHost(() => new Uno.CanvasSample.App(), args);
      host.Run();
    }
  }
}