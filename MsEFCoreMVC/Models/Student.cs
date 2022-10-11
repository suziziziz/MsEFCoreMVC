using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsEFCoreMVC.Models {
  public class Student : Person {
    private DateTime _enrollmentDate;
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Enrollment Date")]
    public DateTime EnrollmentDate {
      get => _enrollmentDate;
      set => _enrollmentDate = value.ToUniversalTime();
    }


    public ICollection<Enrollment> Enrollments { get; set; }
  }
}
