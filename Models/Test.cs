using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyMATEUpload.Models
{
    public class Test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Year { get; set; }
        public string Text { get; set; }
        public int QuestionNo { get; set; }
        public string ShortDescription { get; set; }
        public int Duration { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        public int CourseId { get; set; }

        public ICollection<Quiz> Quizes { get; set; }

    }

    namespace ViewModels
    {
        public class TestViewModel
        {
            public string Year { get; set; }
            public string Text { get; set; }
            public int QuestionNo { get; set; }
            public string ShortDescription { get; set; }
            public int Duration { get; set; }
            public int CourseId { get; set; }
        }
    }

    namespace DTOs
    {
        public class TestDTO : Test
        { }
    }
}
