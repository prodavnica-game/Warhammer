using MySql.Data.MySqlClient;
using System;
using TMPro;
using UnityEngine;

public class Write : MonoBehaviour
{

    public TMP_InputField username;
    private string connectionString;
    private MySqlConnection MS_Connection;
    private MySqlCommand MS_Command;
    string query;
    string check_user;
    public void sendInfo()
    {
        try
        {
            connection();


            if (username.text == "")
            {
                username.text = "Guest1";
            }
            PlayerPrefs.SetString("UserOne", username.text.ToLower());

            check_user = "SELECT COUNT(*) FROM users WHERE username = '" + username.text.ToLower() + "';";

            MS_Command = new MySqlCommand(check_user, MS_Connection);

            MS_Command.ExecuteScalar();

            var row_number = MS_Command.ExecuteScalar();


            if (Convert.ToInt32(row_number) < 1)
            {

                query = "INSERT INTO users ( username ) VALUES ('" + username.text.ToLower() + "');";


                MS_Command = new MySqlCommand(query, MS_Connection);

                MS_Command.ExecuteNonQuery();


                MS_Connection.Close();
            }
            else
            {
                return;
            }



        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }
    }

    private void connection()
    {
        connectionString = "Server=localhost; database = unity_database; user = root; password = ''; charset = utf8";
        MS_Connection = new MySqlConnection(connectionString);

        MS_Connection.Open();
    }
}
