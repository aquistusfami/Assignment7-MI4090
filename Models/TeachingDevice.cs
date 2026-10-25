/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

public enum DeviceStatus 
{
  Available,
  Borrowed,
  UnderMaintenance
}

/// <summary>
/// Lớp trừu tượng TeachingDevice đại diện cho các thiết bị trong phòng học
/// </summary>
public abstract class TeachingDevice : IIdentifiable
{
  public string Id { get; }
  public string Name { get; }
  public string StorageLocation { get; }
  public DeviceStatus Status { get; protected set; }
  
  /// <summary>
  /// Constructor khởi tạo thông tin cơ bản của thiết bị giảng dạy
  /// </summary>
  /// <param name="id"></param>
  /// <param name="name"></param>
  /// <param name="storedAt"></param>
  public TeachingDevice(string id, string name, string storedAt)
  {
    if(string.IsNullOrWhiteSpace(id)) throw new ArgumentException("<!> Mã thiết bị không được trống", nameof(id));
    if(string.IsNullOrWhiteSpace(name)) throw new ArgumentException("<!> Tên thiết bị không được trống", nameof(name));
    if(string.IsNullOrWhiteSpace(storedAt)) throw new ArgumentException("<!> Nơi để thiết bị không được để trống", nameof(storedAt));
    this.Id = id;
    this.Name = name;
    this.StorageLocation = storedAt;
    this.Status = DeviceStatus.Available;
  }

  /// <summary>
  /// Abstract method kiểm tra thiết bị có sẵn sàng sử dụng được không
  /// </summary>
  /// <returns></returns>
  public abstract bool IsReadyForUse();

  /// <summary>
  /// Abstract method xác định điểm ưu tiên của thiết bị
  /// </summary>
  /// <returns></returns>
  public abstract double GetDefaultPriorityScore();

  /// <summary>
  /// Mượn thiết bị
  /// </summary>
  public void Borrow()
  {
    if(!this.IsReadyForUse()) throw new DeviceNotReadyException(this.Id, this.Status);
    this.Status = DeviceStatus.Borrowed;
    Console.WriteLine($"[dev] Mượn thành công {this.Name} - mã {this.Id} !");
  }

  /// <summary>
  /// Trả thiết bị
  /// </summary>
  public void Return()
  {
    if(this.Status != DeviceStatus.Borrowed) throw new InvalidOperationException($"<!> Thiết bị không ở trạng thái đang được mượn.");
    this.Status = DeviceStatus.Available;
  }
}
