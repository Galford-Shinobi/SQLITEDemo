using SQLITEDemo.Abstractions;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SQLITEDemo.MVVM.Models
{
    [Table("Customers")]
    public class Customer : TableData
    {

        [Column("name"), Indexed, NotNull]
        public string Name { get; set; }
        [Unique]
        public string Phone { get; set; }
        public int Age { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        [Column("Email"), Unique, MaxLength(100), NotNull]
        public string Email { get; set; }
        [Ignore]
        public bool IsYoung =>
             Age > 50 ? true : false;
        //[ForeignKey(typeof(Passport))]
        //public int PassportId { get; set; }
        //[OneToOne(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead | CascadeOperation.CascadeDelete)]
        //public Passport Passport { get; set; }
        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        [ManyToMany(typeof(Passport), CascadeOperations = CascadeOperation.All)]
        public List<Passport> Passport { get; set; }
    }
}
