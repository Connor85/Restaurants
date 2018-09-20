using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Food;

namespace Food.Models
{
  public class Restaurant{
    private string _name;
    private string _id;

    public Restaurant (string name, int id)
    {
      _name = name;
      _id = id;
    }
    public override bool Equals(System.Object otherCuisine)
    {
      if (!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool areIdsEqual = (this.GetId() == newCuisine.GetId());
        bool areNamesEqual = (this.GetName() == newCuisine.GetName());
        return (areIdsEqual && areNamesEqual);
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public string GetRestaurantName()
    {
      return _name;
    }
    public string GetRestaurantId()
    {
      return _id;
    }

    public void Save()
    {
      MySqlConnection = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO Resteraunts name value @name;";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Paramaters.Add(name);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
