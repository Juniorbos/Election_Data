using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace StudentAdminPanel
{
    public partial class InputForm : Form
    {
        public static IMongoClient client = new MongoClient("mongodb+srv://junior:junior22@cluster0.ctxr861.mongodb.net/?retryWrites=true&w=majority");

        public static IMongoDatabase db = client.GetDatabase("election_data");

        public static IMongoCollection<Voters> collection = db.GetCollection<Voters>("voter");
        public InputForm()
        {
            InitializeComponent();
        }

        public class Voters
        {
            [BsonId]
            public ObjectId Id { get; set; }
            [BsonElement("Fullname")]
            public string FullName { get; set; }
            [BsonElement("PhoneNumber")]
            public string PhoneNumber { get; set; }
            [BsonElement("Age")]
            public string Age { get; set; }
            [BsonElement("Gender")]
            public string Gender { get; set; }
            [BsonElement("StateOfOrigin")]
            public string StateOfOrigin { get; set; }
            [BsonElement("Address")]
            public string Address { get; set; }

            public Voters(string fullname, string phone_number, string age, string gender, string state_of_origin, string address)
            {
                FullName = fullname;
                PhoneNumber = phone_number;   
                Age = age;
                Gender = gender;
                StateOfOrigin = state_of_origin;
                Address = address;
            }
        }

        private void InputForm_Load(object sender, EventArgs e)
        {

        }

        private void addData_Click(object sender, EventArgs e)
        {
            if(txtFullname.Text == "" && txtPhoneNumber.Text == "" && txtStateOfOrigin.Text == "" && txtAge.Text == "" && txtGender.Text == "" && txtAddress.Text == "")
            {
                MessageBox.Show(
                    "Please fill a input fields",
                    "Data Registration Failed!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
               );
            }
            else
            {
                Voters student = new Voters(
                       txtFullname.Text,
                       txtPhoneNumber.Text,
                       txtStateOfOrigin.Text,
                       txtAge.Text,
                       txtGender.Text,
                       txtAddress.Text
                    );

                collection.InsertOne(student);

                MessageBox.Show(
                    "Registration Successful please click ok to be redirected to login page!",
                    "Registration Success!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None
                );

                ReadData();

                txtFullname.Text = "";
                txtPhoneNumber.Text = "";
                txtStateOfOrigin.Text = "";
                txtAge.Text = "";
                txtGender.Text = "";
                txtAddress.Text = "";
            }
        }

        public void ReadData()
        {
            List<Voters> list = collection.AsQueryable().ToList();
            dataGridView1.DataSource = list;
            txtFullname.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Hide();
        }

        private void txtFullname_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
