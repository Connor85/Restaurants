using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Food;

namespace Food.Models
{
  public class Cuisine
  {
    private string _name;
    private string _id;

    public Cuisine(string name, int id =0)
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

      public string GetCuisineName()
      {
        return _name;
      }

      public int GetCuisineId()
      {
        return _id;
      }

      public void Save()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();

       var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"INSERT INTO Restaurants cuisine value @cuisine;";

       MySqlParameter cuisine = new MySqlParameter();
       cuisine.ParameterCuisine = "@cuisine";
       cuisine.Value = this._name;
       cmd.Parameters.Add(cuisine);

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
