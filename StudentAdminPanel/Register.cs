using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EncryptionandDecryption;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace StudentAdminPanel
{
    public partial class Register : Form
    {
        public static IMongoClient client = new MongoClient("mongodb+srv://junior:junior22@cluster0.ctxr861.mongodb.net/?retryWrites=true&w=majority");

        public static IMongoDatabase db = client.GetDatabase("election_data");

        public static IMongoCollection<User> collection = db.GetCollection<User>("user");

        public Register()
        {
            InitializeComponent();
        }

        public class User
        {
            [BsonId]
            public ObjectId Id { get; set; }
            [BsonElement("Username")]
            public string Username { get; set; }
            [BsonElement("Password")]
            public string Password { get; set; }

            public User(string username, string password)
            {
                Username = username;
                Password = password;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkInput.Checked == false)
            {
                txtPassword.PasswordChar = '\0';
                txtConfirmPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
                txtConfirmPassword.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text == "" && txtPassword.Text == "" && txtConfirmPassword.Text == "")
            {
                MessageBox.Show(
                    "Username and Password fields are empty",
                    "Registration Failed!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
               );
            }
            else if(txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show(
                    "Password Fields do not match!",
                    "Registration Failed!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                 );
            }
            else
            {
                User user = new User(txtUsername.Text, Cryptography.Encrypt(txtPassword.Text.ToString()));

                collection.InsertOne(user);

                MessageBox.Show(
                    "Registration Successful please click ok to be redirected to login page!",
                    "Registration Success!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None
                );

                new Login().Show();
                this.Hide();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Login().Show();
            this.Hide();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void checkInput_CheckedChanged(object sender, EventArgs e)
        {
            if (checkInput.Checked == true)
            {
                txtPassword.PasswordChar = '\0';
                txtConfirmPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
                txtConfirmPassword.PasswordChar = '*';
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
