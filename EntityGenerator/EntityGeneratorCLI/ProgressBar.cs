using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace EntityGeneratorCLI
{
  internal class ProgressBar : IDisposable, IProgress<double>
  {
    private const int BlockCount = 10;
    private readonly TimeSpan _animationInterval = TimeSpan.FromSeconds(1.0 / 8);
    private const string Animation = @"|/-\";

    private readonly Timer _timer;

    private double _currentProgress = 0;
    private string _currentText = string.Empty;
    private bool _disposed = false;
    private int _animationIndex = 0;

    public ProgressBar()
      : this(false)
    { }

    public ProgressBar(bool redirectedOutputOverwrite)
    {
      RedirectedOutputOverwrite = redirectedOutputOverwrite;
      _timer = new Timer(TimerHandler);

      if (!Console.IsOutputRedirected || redirectedOutputOverwrite)
      {
        ResetTimer();
      }
    }

    public bool RedirectedOutputOverwrite { get; }

    public void Report(double value)
    {
      // Make sure value is in [0..1] range
      value = Math.Clamp(value, 0.0, 1.0);
      Interlocked.Exchange(ref _currentProgress, value);
    }

    private void TimerHandler(object state)
    {
      lock (_timer)
      {
        if (_disposed)
          return;

        var progressBlockCount = (int)(_currentProgress * BlockCount);
        var percent = (int)(_currentProgress * 100);

        var text = string.Format("[{0}{1}] {2,3}% {3}",
          new string('#', progressBlockCount),
          new string('-', BlockCount - progressBlockCount),
          percent,
          Animation[_animationIndex++ % Animation.Length]);

        UpdateText(text);
        ResetTimer();
      }
    }

    private void UpdateText(string text)
    {
      // Get length of common portion
      var commonPrefixLength = 0;
      var commonLength = Math.Min(_currentText.Length, text.Length);

      while (commonPrefixLength < commonLength && text[commonPrefixLength] == _currentText[commonPrefixLength])
      {
        commonPrefixLength++;
      }

      // Backtrack to the first differing character
      var outputBuilder = new StringBuilder();
      outputBuilder.Append('\b', _currentText.Length - commonPrefixLength);

      // Output new suffix
      outputBuilder.Append(text.Substring(commonPrefixLength));

      // If the new text is shorter than the old one: delete overlapping characters
      var overlapCount = _currentText.Length - text.Length;

      if (overlapCount > 0)
      {
        outputBuilder.Append(' ', overlapCount);
        outputBuilder.Append('\b', overlapCount);
      }

      Console.Write(outputBuilder);
      _currentText = text;
    }

    private void ResetTimer()
    {
      _timer.Change(_animationInterval, TimeSpan.FromMilliseconds(-1));
    }

    public void Dispose()
    {
      lock (_timer)
      {
        _disposed = true;
        UpdateText(string.Empty);
      }
    }

  }
}