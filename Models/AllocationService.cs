/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

/// <summary>
/// Lớp generic dịch vụ cấp phát ràng buộc TeachingDevice
/// </summary>
/// <typeparam name="T"></typeparam>
public class AllocationService<T> where T : TeachingDevice
{
  private readonly IRepository<T> _repository;
  private ISelectionStrategy<T> _strategy;

  /// <summary>
  /// Constructor
  /// </summary>
  /// <param name="repository"></param>
  /// <param name="strategy"></param>
  public AllocationService(IRepository<T> repository, ISelectionStrategy<T> strategy)
  {
    _repository = repository;
    _strategy = strategy;
  }

  /// <summary>
  /// Đổi chiến lược
  /// </summary>
  /// <param name="newStrategy"></param>
  public void ChangeStrategy(ISelectionStrategy<T> newStrategy)
  {
    _strategy = newStrategy;
  }

  /// <summary>
  /// Cấp phát thiết bị
  /// </summary>
  /// <param name="additionalCondition"></param>
  /// <returns></returns>
  public T Allocate(Predicate<T> additionalCondition)
  {
    var candidates = _repository.FindAll(d => d.IsReadyForUse() && (additionalCondition == null || additionalCondition(d)));
    if (candidates.Count == 0) throw new NoSuitableItemException();
    T chosen = _strategy.Select(candidates);
    chosen.Borrow();
    return chosen;
  }
}
