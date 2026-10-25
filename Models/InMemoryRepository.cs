/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

/// <summary>
/// Lớp generic repo thực thi IRepository 
/// </summary>
/// <typeparam name="T">IIdentifiable</typeparam>
public class InMemoryRepository<T> : IRepository<T> where T : IIdentifiable
{
  private List<T> items = new List<T>();

  /// <summary>
  /// Thêm thiết bị 
  /// </summary>
  /// <param name="item"></param>
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

  /// <summary>
  /// Xóa thiết bị
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
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

  /// <summary>
  /// Tìm kiếm thiết bị theo mã
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
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

  /// <summary>
  /// Lấy ds thiết bị
  /// </summary>
  /// <returns></returns>
  public IReadOnlyList<T> GetAll() => items.AsReadOnly();

  /// <summary>
  /// Tìm tất cả các thiết bị thỏa mãn đk 
  /// </summary>
  /// <param name="condition"></param>
  /// <returns></returns>
  public IReadOnlyList<T> FindAll(Predicate<T> condition) => items.FindAll(condition);
}
