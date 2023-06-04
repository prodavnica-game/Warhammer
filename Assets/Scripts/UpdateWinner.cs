using MySql.Data.MySqlClient;
using System;
using UnityEngine;

public class UpdateWinner : MonoBehaviour
{
    private string connectionString;
    private MySqlConnection MS_Connection;
    private MySqlCommand MS_Command;
    private string pobednik_upit;
    private string gubitnik_upit;
    string updateQuery;

    public void UpdateUserScore(string pobednik, string gubitnik)
    {
        try
        {
            connection();
            pobednik_upit = $"UPDATE users SET win = win + 1 WHERE username = '{pobednik}'";
            gubitnik_upit = $"UPDATE users SET loss = loss + 1 WHERE username = '{gubitnik}'";
            MS_Command = new MySqlCommand(pobednik_upit, MS_Connection);
            MS_Command.ExecuteNonQuery();
            MS_Command = new MySqlCommand(gubitnik_upit, MS_Connection);
            MS_Command.ExecuteNonQuery();
            MS_Connection.Close();

        }
        catch (Exception ex)
        {

            Debug.Log(ex.Message);
        }
    }
    public void connection()
    {
        connectionString = "Server=localhost; database = unity_database; user = root; password = ''; charset = utf8";
        MS_Connection = new MySqlConnection(connectionString);

        MS_Connection.Open();
    }
}
