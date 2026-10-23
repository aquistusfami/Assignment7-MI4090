/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

public class InMemoryRepository<T> : IRepository<T> where T : IIdentifiable
{
  private List<T> items = new List<T>();

  public void Add(T item)
  {
    if(item == null) throw new ArgumentNullException(nameof(item), $"<!> Thiết bị không được để trống");
    foreach (var i in items)
    {
      if (i.Id == item.Id)
      {
        throw new DuplicateIdException(item.Id);
      }
    }
    items.Add(item);
  }

  public bool Remove(string id)
  {
    foreach(var item in items)
    {
      if(item.Id == id)
      {
        items.Remove(item);
        return true;
      }
    }
    return false;
  }

  public T? FindById(string id)
  {
    foreach(var item in items)
    {
      if(item.Id == id)
      {
        return item;
      }
    }
    return default(T);
  }

  public IReadOnlyList<T> GetAll() => items.AsReadOnly();

  public IReadOnlyList<T> FindAll(Predicate<T> condition) => items.FindAll(condition);
}
