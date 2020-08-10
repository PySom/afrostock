using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyMATEUpload.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string SubText { get; set; }

        public ICollection<Test> Tests { get; set; }
    }

    namespace ViewModels
    {
        public class CourseViewModel : Course
        { }
    }

    namespace DTOs
    {
        public class CourseDTO : Course
        { }
    }
}

