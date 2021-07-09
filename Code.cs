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
 public class EmployeeManagementContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
    }
}
    public partial class AddEmployee : Form
    {
        private bool _dragging;
        private Point _startPoint = new Point(0, 0);

        //Delegate has been added
        public delegate void IdentityHandler(object sender, IdentityEventArgs e);

        
        //Event of the delegate type has been added. i.e. Object of delegate created
        public event IdentityHandler IdentityUpdated;

        public AddEmployee()
        {
            InitializeComponent();
            comboBoxDepartment.Items.Add("Administrative");
            comboBoxDepartment.Items.Add("Finance");
            comboBoxDepartment.Items.Add("Customer service");
            comboBoxDepartment.Items.Add("Marketing");
            comboBoxDepartment.Items.Add("IT");
            comboBoxDepartment.SelectedIndex = 0;
        }

        //This method will set the values on controls received from the selected row.
        public void LoadData(string id, string name, string address, string contact, string email, string desigination,
           string department, string dateOfJoin, string wageRate, string workedHour)
        {
            txtIdNo.Text = id;
            txtFullName.Text = name;
            txtAddress.Text = address;
            txtContact.Text = contact;
            txtEmail.Text = email;
            txtDesignation.Text = desigination;
            comboBoxDepartment.Text = department;
            dateTimePicker.Text = dateOfJoin;
            txtWage.Text = wageRate;
            txtWorkedHour.Text = workedHour;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _startPoint = new Point(e.X, e.Y);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragging) return;
            var p = PointToScreen(e.Location);
            Location = new Point(p.X - this._startPoint.X, p.Y - this._startPoint.Y);
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var id = txtIdNo.Text;
            var name = txtFullName.Text;
            var address = txtAddress.Text;
            var contactNo = txtContact.Text;
            var email = txtEmail.Text;
            var desigination = txtDesignation.Text;
            var department = comboBoxDepartment.Text;
            var dateOfJoin = dateTimePicker.Text;
            var wageRate = txtWage.Text;
            var hourWorked = txtWorkedHour.Text;

            using (var context = new EmployeeManagementContext())
            {
                var emp = new Employee(id, name, address, contactNo, email, desigination, department, dateOfJoin, wageRate, hourWorked);
                context.Employees.Add(emp);
                await context.SaveChangesAsync();
            }

            //instance event args and value has been passed 
            var args = new IdentityEventArgs(id, name, address, contactNo, email, desigination, department, dateOfJoin, wageRate, hourWorked);

            //Event has be raised with update arguments of delegate
            IdentityUpdated?.Invoke(this, args);

            this.Hide();
        }

        //This method valid the textoBox full name, if you put a number it return an error
        private bool Validation(TextBox t, string name)
        {
            int n;
            bool error = false;

            if (int.TryParse(t.Text, out n))
            {
                error = true;
                MessageBox.Show("Invalid character", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return error;

        }
        private void txtFullName_TextChanged(object sender, EventArgs e)
        {
            Validation(txtFullName, "Full name");
        }
    }
}
