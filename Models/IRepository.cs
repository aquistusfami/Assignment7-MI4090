/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

/// <summary>
/// Giao diện kho dữ liệu ràng buộc IIdentifiable
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : IIdentifiable
{
  /// <summary>
  /// Method thêm thiết bị 
  /// </summary>
  /// <param name="item"></param>
  public void Add(T item);

  /// <summary>
  /// Xóa thiết bị 
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public bool Remove(string id);

  /// <summary>
  /// Tìm kiếm thiết bị theo mã 
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public T? FindById(string id);

  /// <summary>
  /// Lấy ra danh sách thiết bị
  /// </summary>
  /// <returns></returns>
  public IReadOnlyList<T> GetAll();

  /// <summary>
  /// Tìm tất tả các thiết bị đáp ứng điều kiện
  /// </summary>
  /// <param name="condition"></param>
  /// <returns></returns>
  public IReadOnlyList<T> FindAll(Predicate<T> condition);
} 
