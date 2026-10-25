/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

/// <summary>
/// Giao diện chọn chiến lược
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ISelectionStrategy<T>
{
  /// <summary>
  /// Method chọn các ứng viên
  /// </summary>
  /// <param name="candidates"></param>
  /// <returns></returns>
  public T Select(IReadOnlyList<T> candidates);
}
