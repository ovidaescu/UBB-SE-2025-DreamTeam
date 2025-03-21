using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Data.SqlClient;
using Duo.Models;
using Duo.Data;

namespace Duo.Repositories;

public class UserRepository
{
    private readonly DataLink dataLink;

    public UserRepository(DataLink dataLink)
    {
        this.dataLink = dataLink;
    }

    public int CreateUser(User user)
    {
        SqlParameter[] parameters = new SqlParameter[]
        {
        new SqlParameter("@Name", user.Name),
        new SqlParameter("@Email", user.Email),
        new SqlParameter("@Age", user.Age)
        };

        try
        {
            int? result = dataLink.ExecuteScalar<int>("CreateUser", parameters);
            return result ?? 0;
        }
        catch (SqlException ex)
        {
            throw new Exception($"SQL Error: {ex.Message}");
        }
    }

    public void DeleteUser(int id)
    {
        SqlParameter[] parameters = new SqlParameter[]
        {
        new SqlParameter("@Id", id)
        };
        dataLink.ExecuteNonQuery("DeleteUser", parameters);
    }


    public void UpdateUser(User user)
    {
        SqlParameter[] parameters = new SqlParameter[]
        {
        new SqlParameter("@Id", user.Id),
        new SqlParameter("@Name", user.Name),
        new SqlParameter("@Email", user.Email),
        new SqlParameter("@Age", user.Age)
        };
        dataLink.ExecuteNonQuery("UpdateUser", parameters);
    }


    public User GetUserById(int id)
    {
        SqlParameter[] parameters = new SqlParameter[]
        {
        new SqlParameter("@Id", id)
        };

        DataTable dataTable = dataLink.ExecuteReader("GetUserById", parameters);

        if (dataTable.Rows.Count > 0)
        {
            DataRow row = dataTable.Rows[0];
            return new User
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString() ?? string.Empty,
                Email = row["Email"].ToString() ?? string.Empty,
                Age = Convert.ToInt32(row["Age"])
            };
        }

        return null;
    }


    public List<User> GetAllUsers()
    {
        DataTable result = dataLink.ExecuteReader("GetAllUsers");
        List<User> users = new List<User>();

        foreach (DataRow row in result.Rows)
        {
            users.Add(new User
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString() ?? string.Empty,
                Email = row["Email"].ToString() ?? string.Empty,
                Age = Convert.ToInt32(row["Age"])
            });
        }

        return users;
    }
}

