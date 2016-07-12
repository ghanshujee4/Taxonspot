using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;




namespace TaxonspotMVC.Models
{
    public class MovieDB
    {
        public int ID { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Last Name is Required")]
        public string PAN { get; set; }

        [Required(ErrorMessage= "Date of Birth is Required ")]
        [Display (Name="Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public string DOB { get; set; }

        [Required(ErrorMessage=" Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage="First Name is Required")]
        [Display (Name="Father's Nmae")]
        public string FName { get; set; }

        [Required(ErrorMessage="Mobile Number is Required")]
        public string Mobile { get; set; }

        [Required(ErrorMessage="Your Adress is Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        
        public string Password { get; set; }

       


    }
    public class MovieDBContext : DbContext
    {
        public MovieDBContext()
            : base("MovieDBContext")
        {
        }
        public DbSet<MovieDB> Movies { get; set; }
        public DbSet<UserDB> Users { get; set; }
    }

    public class UserDB
    {
      [Key]
        public string PAN { get; set; }

   
        public string Password { get; set; }

}
public class VatDB
{

 public string VAT { get; set; }
        public string RegistrationDate { get; set; }
        public string Propriter { get; set; }

    }}