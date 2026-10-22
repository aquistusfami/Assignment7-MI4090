/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

public class ACRemote : TeachingDevice
{
  public IReadOnlySet<string> CompatibleRooms { get; }

  public ACRemote(string id, string name, string storedAt, IEnumerable<string> compatibleRooms) : base(id,name,storedAt)
  {
    if (compatibleRooms is null) throw new ArgumentNullException(nameof(compatibleRooms));
    this.CompatibleRooms = compatibleRooms.ToHashSet();
  }

  public bool IsCompatibleWith(string roomId)
  {
    return this.CompatibleRooms.Contains(roomId);
  }

  public override bool IsReadyForUse()
  {
    if(this.Status == DeviceStatus.Available) return true;
    else return false;
  }

  public override double GetDefaultPriorityScore()
  {
    return this.CompatibleRooms.Count > 0 ? 100.0 : 0.0;
  }
}
