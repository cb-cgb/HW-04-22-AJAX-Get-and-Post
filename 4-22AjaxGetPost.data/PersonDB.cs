using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace _4_22AjaxGetPost.data
{
   public  class PersonDB
    {
        private string _conn;

        public PersonDB(string connection)
        {
            _conn = connection;
        }

        public IEnumerable<Person> GetPeople()
        {
            using (var conn = new SqlConnection(_conn))
            using (var cmd = conn.CreateCommand())
            {
                var ppl = new List<Person>();
                cmd.CommandText = "select * from People";
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ppl.Add(new Person
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Age = (int)reader["Age"]
                    });
                }
                return ppl;
            }
        }

        public int AddPerson(Person p)
        {
            using (var conn = new SqlConnection(_conn))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "Insert into People select @first, @last, @age select Scope_identity()";
                cmd.Parameters.AddWithValue("@first", p.FirstName);
                cmd.Parameters.AddWithValue("@last", p.LastName);
                cmd.Parameters.AddWithValue("@age", p.Age);
                conn.Open();

                return (int)(decimal)cmd.ExecuteScalar();
            }

        }

        public void DeletePerson(int id )
        {
            using (var conn = new SqlConnection(_conn))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "Delete from People where id  = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                cmd.ExecuteNonQuery();
            }

        }

        public void UpdatePerson(Person p)
        {
            using (var conn = new SqlConnection(_conn))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "Update  People set firstName = @first, lastName = @last, age = @age where id  = @Id";
                cmd.Parameters.AddWithValue("@Id", p.Id);
                cmd.Parameters.AddWithValue("@first", p.FirstName);
                cmd.Parameters.AddWithValue("@last", p.LastName);
                cmd.Parameters.AddWithValue("@age", p.Age);
                conn.Open();

                cmd.ExecuteNonQuery();
            }

        }
    }
}
