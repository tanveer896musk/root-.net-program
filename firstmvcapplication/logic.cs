using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.InteropServices;

namespace firstmvcapplication.Models
{


    public class logic
    {
      //  private string connectionString = ConfigurationManager.ConnectionStrings["Tanveer_Test10"].ConnectionString;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TanveerDB"].ToString());
        
        
        // Method to insert a student
        public void InsertStudent(Student student)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_student_insert", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", student.Name);
                cmd.Parameters.AddWithValue("@Age", student.Age);
                cmd.Parameters.AddWithValue("@Addmissiondate", student.Addmissiondate);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
             Console.WriteLine(ex.Message);  
            }
                
            
        }
        // Method to get all students
        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_result", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Age = Convert.ToInt32(reader["Age"]),
                        Addmissiondate = reader.GetDateTime(reader.GetOrdinal("Addmissiondate"))
                    });  
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return students;
        }


        // Method to update a student
        public void UpdateStudent(Student student)
        {
            try
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_student_update", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", student.Id);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Age", student.Age);
                cmd.Parameters.AddWithValue("@Addmissiondate", student.Addmissiondate);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Method to delete a student
        public void DeleteStudent(int id)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_student_delete", conn);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public List<Student> details(int id)
        {
            List<Student> students = new List<Student>();
            conn.Open();
            SqlCommand c = new SqlCommand("select * from student where ID = @ID",conn);
            c.Parameters.AddWithValue("@ID", id);
            SqlDataReader reader = c.ExecuteReader();
            while (reader.Read())
            {
                Student student = new Student
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Age = reader.GetInt32(reader.GetOrdinal("Age")),
                    Addmissiondate = reader.GetDateTime(reader.GetOrdinal("Addmissiondate"))

                };
                students.Add(student);
            }

            reader.Close();
            c.Dispose();
            conn.Close();

            return students;

        }
    }
}