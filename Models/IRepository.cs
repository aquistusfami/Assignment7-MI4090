/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

public interface IRepository<T> where T : IIdentifiable
{
  public void Add(T item);
  public bool Remove(string id);
  public T? FindById(string id);
  public IReadOnlyList<T> GetAll();
  public IReadOnlyList<T> FindAll(Predicate<T> condition);
} 
