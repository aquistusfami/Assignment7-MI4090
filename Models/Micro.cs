/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

public class Micro : TeachingDevice
{
  public int BatteryLevel { get; }
  public int SignalQuality { get; }

  public Micro(string id, string name, string storedAt, int batLevel, int sigQuality) : base(id, name, storedAt)
  {
    if(batLevel < 0 || batLevel > 100) throw new ArgumentOutOfRangeException(nameof(batLevel),"<!> Mức pin phải nằm trong đoạn từ 0 đến 100.");
    if(sigQuality < 0 || sigQuality > 100) throw new ArgumentOutOfRangeException(nameof(sigQuality),"<!> Tín hiệu phải nằm trong đoạn từ 0 đến 100.");
    this.BatteryLevel = batLevel;
    this.SignalQuality = sigQuality;
  }

  public override bool IsReadyForUse()
  {
    if(this.Status == DeviceStatus.Available && this.BatteryLevel >= 30
    && this.SignalQuality >= 40) return true;
    else return false;
  }

  public override double GetDefaultPriorityScore()
  {
    return (this.BatteryLevel + this.SignalQuality)/2.0;
  }
}
