using System.ComponentModel.DataAnnotations;

namespace SwaggerTestAPI2.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Unique Person Identifier
        /// </summary>
        [Required]
        public int ID { get; set; }

        /// <summary>
        /// This is the person's First Name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// This is the person's Last Name
        /// </summary>
        public string LastName { get; set; }


    }
}
