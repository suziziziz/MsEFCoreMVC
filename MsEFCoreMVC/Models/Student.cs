using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MsEFCoreMVC.Models {
  public class Student {
    public int ID { get; set; }
    public string LastName { get; set; }
    public string FirstMidName { get; set; }

    private DateTime _enrollmentDate;
    public DateTime EnrollmentDate {
      get => _enrollmentDate;
      set => _enrollmentDate = value.ToUniversalTime();
    }

    public ICollection<Enrollment> Enrollments { get; set; }
  }
}
