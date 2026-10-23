/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

public interface ISelectionStrategy<T>
{
  public T Select(IReadOnlyList<T> candidates);
}
