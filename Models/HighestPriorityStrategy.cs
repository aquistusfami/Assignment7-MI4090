/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

public class HighestPriorityStrategy<T> : ISelectionStrategy<T>
{
  private readonly Func<T,double> _scoreSelector;

  public HighestPriorityStrategy(Func<T,double> scoreSelector)
  {
    _scoreSelector = scoreSelector ?? throw new ArgumentNullException(nameof(scoreSelector));
  }

  public T Select(IReadOnlyList<T> candidates)
  {
    if (candidates == null || candidates.Count == 0) throw new NoSuitableItemException();

    T best = candidates[0];
    for (int i = 1; i < candidates.Count; i++)
      if (_scoreSelector(candidates[i]) > _scoreSelector(best)) best = candidates[i];
    return best;
  }
}
