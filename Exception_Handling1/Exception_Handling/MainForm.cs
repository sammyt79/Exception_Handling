// Exception Handling
// Samuel Tollefson
// POS/409
// April 20, 2015
// Jon Jensen

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exception_Handling
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;

            try
            {
                // Check for null data.
                if (txtID.Text == "" | txtFName.Text == "" | txtLName.Text == "" | txtPhone.Text == "")
                {
                    throw new ArgumentNullException();
                }

                // Check current list for duplicate ID.
                foreach (DataGridViewRow row in dgvPeople.Rows)
                {
                    if (txtID.Text == dgvPeople.Rows[rowIndex].Cells[0].Value.ToString())
                    {
                        throw new IDAlreadyExistsException();
                    }
                    rowIndex++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            // Used for following ID check
            char[] b = new char[6]; 
            List<string> idCollectionString = Person.searchList().Split(',').ToList();

            // Check text file for duplicate ID.
            foreach (string s in idCollectionString)
            {
                try
                {
                    using (StringReader sr = new StringReader(s))
                    {
                        sr.Read(b, 0, 6);
                        string str = new string(b);

                        if (txtID.Text == str)
                        {
                            throw new IDAlreadyExistsException();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            // Set the variables.
            new Person(txtID.Text, txtFName.Text, txtLName.Text, txtPhone.Text);

            // Display the new person.
            dgvPeople.Rows.Add(Person.PersonID, Person.FName, Person.LName, Person.PhoneNumber);

            Person.addPersonToList();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private void btnAddToFile_Click(object sender, EventArgs e)
        {
            Person.addListToFile();

            MessageBox.Show("List has been added to file.");
        }
    }
}
