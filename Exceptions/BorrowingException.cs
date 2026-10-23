/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

/// <summary>
/// Lớp ngoại lệ mượn đồ
/// </summary>
public class BorrowingException : Exception
{
  public BorrowingException(string message) : base(message)
  {

  }

  public BorrowingException(string message, Exception innerException)
  : base(message, innerException)
  {
    
  }
}
