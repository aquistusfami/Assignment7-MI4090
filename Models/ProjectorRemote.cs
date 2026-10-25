/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

/// <summary>
/// Lớp điều khiển máy chiếu kế thừa TeachingDevice
/// </summary>
public class ProjectorRemote : TeachingDevice
{
  public int BatteryLevel { get; }

  /// <summary>
  /// Khởi tạo đk máy chiếu với các thôgn tin cần thiết
  /// </summary>
  /// <param name="id"></param>
  /// <param name="name"></param>
  /// <param name="storedAt"></param>
  /// <param name="batteryLevel"></param>
  /// <returns></returns>
  public ProjectorRemote(string id, string name, string storedAt, int batteryLevel) : base(id, name, storedAt)
  {
    if(batteryLevel < 0 || batteryLevel > 100) throw new ArgumentOutOfRangeException(nameof(batteryLevel),"<!> Mức pin phải nằm trong đoạn từ 0 đến 100.");
    this.BatteryLevel = batteryLevel;
  }

  /// <summary>
  /// Kiểm tra thiết bị đã sẵn sàng để sử dụng
  /// </summary>
  /// <returns>true nếu sẵn sàng, false nếu không</returns>
  public override bool IsReadyForUse()
  {
    if(this.Status == DeviceStatus.Available && this.BatteryLevel >= 20) return true;
    else return false;
  }

  /// <summary>
  /// Tính điểm ưu tiên
  /// </summary>
  /// <returns>Điểm ưu tiên của đk máy chiếu</returns>
  public override double GetDefaultPriorityScore()
  {
    return (double)this.BatteryLevel;
  }
}
