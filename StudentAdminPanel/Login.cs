﻿using System;
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
    public partial class Login : Form
    {

        public static IMongoClient client = new MongoClient("mongodb+srv://junior:junior22@cluster0.ctxr861.mongodb.net/?retryWrites=true&w=majority");

        public static IMongoDatabase db = client.GetDatabase("election_data");

        public static IMongoCollection<User> collection = db.GetCollection<User>("user");

        public Login()
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Register().Show();
            this.Hide();
        }

        private void signinBtn_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text == "" && txtPassword.Text == "")
            {
                MessageBox.Show(
                   "Username and Password fields are empty",
                   "Registration Failed!",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error
              );
            }
            else
            {
                var filter = Builders<User>.Filter.Eq("Username", txtUsername.Text);
                var validateUser = collection.Find(filter).FirstOrDefault();
                if(validateUser == null)
                {
                    MessageBox.Show(
                            "No user with provided credentials",
                               "Registration Failed!",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error
                        );
                }
                else if(validateUser != null)
                {
                    new InputForm().Show();
                    this.Hide();
                }
            };
        }

        private void checkInput_CheckedChanged(object sender, EventArgs e)
        {
            if(checkInput.Checked == true)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
