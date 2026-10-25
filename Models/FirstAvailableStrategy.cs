/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

/// <summary>
/// Lớp generic chiến lược thiết bị đầu tiên sẵn sàng thực thi ISelectionStrategy
/// </summary>
/// <typeparam name="T"></typeparam>
public class FirstAvailableStrategy<T> : ISelectionStrategy<T>
{
  /// <summary>
  /// Chọn thiết bị đầu tiên thõa mãn chiến lược
  /// </summary>
  /// <param name="candidates"></param>
  /// <returns></returns>
  public T Select(IReadOnlyList<T> candidates)
  {
    if (candidates == null || candidates.Count == 0) throw new NoSuitableItemException();
    return candidates[0];
  }
}
