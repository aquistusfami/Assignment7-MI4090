/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

public class FirstAvailableStrategy<T> : ISelectionStrategy<T>
{
  public T Select(IReadOnlyList<T> candidates)
  {
    if (candidates == null || candidates.Count == 0) throw new NoSuitableItemException();
    return candidates[0];
  }
}
