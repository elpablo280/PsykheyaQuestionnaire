﻿using Microsoft.Data.Sqlite;

namespace Psykheya
{
    public partial class DataEntryForm : Form
    {
        public DataEntryForm()
        {
            InitializeComponent();
        }

        // добавляем (проверяем наличие) юзера в базу и запускаем тест
        private void beginTestButton_Click(object sender, EventArgs e)
        {
            User user = new(
                lastNameTextBox.Text, 
                firstNameTextBox.Text,
                middleNameTextBox.Text,
                groupTextBox.Text
            );

            string FIO = $"{user.LastName} {user.FirstName} {user.MiddleName}";

            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();

                // создаём таблицу (если она не создана) с юзерами
                SqliteCommand commandCreate = new()
                {
                    Connection = connection,
                    CommandText = "CREATE TABLE IF NOT EXISTS Users(ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL, Platoon TEXT NOT NULL)",
                };
                commandCreate.ExecuteNonQuery();

                // проверяем, есть ли такой юзер в базе
                SqliteCommand commandCheck = new()
                {
                    Connection = connection,
                    CommandText = $"SELECT * FROM Users WHERE Name='{FIO}' AND Platoon='{user.Group}'",
                };
                using (SqliteDataReader reader = commandCheck.ExecuteReader())
                {
                    if (reader.HasRows)       // если нашёл такого юзера
                    {
                        MessageBox.Show("Такой пользователь уже есть в базе данных!");
                        return;
                    }
                }

                // добавляем юзера в базу
                SqliteCommand commandInsert = new()
                {
                    Connection = connection,
                    CommandText = $"INSERT INTO Users (Name, Platoon) VALUES ('{FIO}', '{user.Group}')"
                };
                int number = commandInsert.ExecuteNonQuery();
                MessageBox.Show($"В таблицу Users добавлено объектов: {number}");
            }

            this.Close();
        }
    }
}
