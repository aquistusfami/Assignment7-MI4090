/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

/// <summary>
/// Lớp điều khiển điều hòa kế thừa TeachingDevice
/// </summary>
public class ACRemote : TeachingDevice
{
  public IReadOnlySet<string> CompatibleRooms { get; }

  /// <summary>
  /// Constructor khởi tạo
  /// </summary>
  /// <param name="id"></param>
  /// <param name="name"></param>
  /// <param name="storedAt"></param>
  /// <param name="compatibleRooms"></param>
  /// <returns></returns>
  public ACRemote(string id, string name, string storedAt, IEnumerable<string> compatibleRooms) : base(id,name,storedAt)
  {
    if (compatibleRooms is null) throw new ArgumentNullException(nameof(compatibleRooms));
    this.CompatibleRooms = compatibleRooms.ToHashSet();
  }

  /// <summary>
  /// Tương thích với phòng có điều hòa
  /// </summary>
  /// <param name="roomId"></param>
  /// <returns></returns>
  public bool IsCompatibleWith(string roomId)
  {
    return this.CompatibleRooms.Contains(roomId);
  }

  /// <summary>
  /// Kiểm tra xem điều khiển có sẵn sàng được dùng
  /// </summary>
  /// <returns></returns>
  public override bool IsReadyForUse()
  {
    if(this.Status == DeviceStatus.Available) return true;
    else return false;
  }

  /// <summary>
  /// Điểm ưu tiên của điều hòa
  /// </summary>
  /// <returns></returns>
  public override double GetDefaultPriorityScore()
  {
    return this.CompatibleRooms.Count > 0 ? 100.0 : 0.0;
  }
}
