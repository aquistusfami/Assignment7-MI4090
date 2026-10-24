/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

public class AllocationService<T> where T : TeachingDevice
{
  private readonly IRepository<T> _repository;
  private ISelectionStrategy<T> _strategy;

  public AllocationService(IRepository<T> repository, ISelectionStrategy<T> strategy)
  {
    _repository = repository;
    _strategy = strategy;
  }

  public void ChangeStrategy(ISelectionStrategy<T> newStrategy)
  {
    _strategy = newStrategy;
  }

  public T Allocate(Predicate<T> additionalCondition)
  {
    var candidates = _repository.FindAll(d => d.IsReadyForUse() && (additionalCondition == null || additionalCondition(d)));
    if (candidates.Count == 0) throw new NoSuitableItemException();
    T chosen = _strategy.Select(candidates);
    chosen.Borrow();
    return chosen;
  }
}
