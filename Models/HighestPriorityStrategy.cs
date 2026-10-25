/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

/// <summary>
/// Lớp generic chiến lược điểm ưu tiên cao nhất thực thi ISelectionStrategy
/// </summary>
/// <typeparam name="T"></typeparam>
public class HighestPriorityStrategy<T> : ISelectionStrategy<T>
{
  private readonly Func<T,double> _scoreSelector;

  /// <summary>
  /// Constructor
  /// </summary>
  /// <param name="scoreSelector"></param>
  public HighestPriorityStrategy(Func<T,double> scoreSelector)
  {
    _scoreSelector = scoreSelector ?? throw new ArgumentNullException(nameof(scoreSelector));
  }

  /// <summary>
  /// Chọn thiết bị đạt đúng yêu cầu của chiến lược
  /// </summary>
  /// <param name="candidates"></param>
  /// <returns></returns>
  public T Select(IReadOnlyList<T> candidates)
  {
    if (candidates == null || candidates.Count == 0) throw new NoSuitableItemException();

    T best = candidates[0];
    for (int i = 1; i < candidates.Count; i++)
      if (_scoreSelector(candidates[i]) > _scoreSelector(best)) best = candidates[i];
    return best;
  }
}
