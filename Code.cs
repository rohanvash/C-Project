namespace EmployeeManagementSystem.Data
{
    public class Employee
    {
        public Employee()
        {
        }

        public Employee(string id, string fullName, string address, string contact, string email, string designation, string department, string dateOfJoin,
            string wageRate, string workedHour)
        {
            this.EmployeeID = id;
            this.FullName = fullName;
            this.Address = address;
            this.Contact = contact;
            this.Email = email;
            this.Designation = designation;
            this.Department = department;
            this.DateOfJoin = string.IsNullOrEmpty(dateOfJoin) ? DateTime.Now.ToShortDateString() : dateOfJoin;
            this.WageRate = wageRate;
            this.WorkedHour = workedHour;
        }

  [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string DateOfJoin { get; set; }
        public string WageRate { get; set; }
        public string WorkedHour { get; set; }
    }
}

class Program 
{ 
    static void Main(string[] args) 
    { 
        using (var db = new BloggingContext()) 
        { 
            // Create and save a new Blog 
            Console.Write("Enter a name for a new Blog: "); 
            var name = Console.ReadLine(); 
 
            var blog = new Blog { Name = name }; 
            db.Blogs.Add(blog); 
            db.SaveChanges(); 
 
            // Display all Blogs from the database 
            var query = from b in db.Blogs 
                        orderby b.Name 
                        select b; 
 
            Console.WriteLine("All blogs in the database:"); 
            foreach (var item in query) 
            { 
                Console.WriteLine(item.Name); 
            } 
 
            Console.WriteLine("Press any key to exit..."); 
            Console.ReadKey(); 
        } 
    } 
}
