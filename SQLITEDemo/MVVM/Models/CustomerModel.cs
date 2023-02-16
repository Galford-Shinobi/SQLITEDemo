using SQLite;

namespace SQLITEDemo.MVVM.Models
{
    [Table("CustomerModels")]
    public class CustomerModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Column("name"), Indexed, MaxLength(75), NotNull]
        
        public string Name { get; set; }
      
        [MaxLength(15), Unique]
        public string Phone { get; set; }

        public int Age { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }
        [Column("Email"), Unique, MaxLength(100), NotNull]
        public string Email { get; set; }
        [Ignore]
        public bool IsYoung =>
             Age > 50 ? true : false;

        [Ignore]
        public string MyFullName => $"{Name}{" - "}{Email}";
    }
}
