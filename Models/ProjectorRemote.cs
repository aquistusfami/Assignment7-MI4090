/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

public class ProjectorRemote : TeachingDevice
{
  public int BatteryLevel { get; }

  public ProjectorRemote(string id, string name, string storedAt, int batteryLevel) : base(id, name, storedAt)
  {
    if(batteryLevel < 0 || batteryLevel > 100) throw new ArgumentOutOfRangeException(nameof(batteryLevel),"<!> Mức pin phải nằm trong đoạn từ 0 đến 100.");
    this.BatteryLevel = batteryLevel;
  }

  public override bool IsReadyForUse()
  {
    if(this.Status == DeviceStatus.Available && this.BatteryLevel >= 20) return true;
    else return false;
  }

  public override double GetDefaultPriorityScore()
  {
    return (double)this.BatteryLevel;
  }
}
