/*Program.cs*/

//
// <<Muhammad Zeeshan>>
// U. of Illinois, Chicago
// CS 341, Fall 2018
// Project #06: Netflix database application
//

using System;
using System.Data;
using System.Data.SqlClient;

namespace program
{
  class Program
  {
    //
    // Connection info for ChicagoCrimes database in Azure SQL:
    //
    static string connectionInfo = String.Format(@"
Server=tcp:jhummel2.database.windows.net,1433;Initial Catalog=Netflix;
Persist Security Info=False;User ID=student;Password=cs341!uic;
MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;
Connection Timeout=30;
");


    static void OutputNumMovies()
    {
      SqlConnection db = null;

      try
      {
        db = new SqlConnection(connectionInfo);
        db.Open();

        string sql = string.Format(@"
SELECT Count(*) As NumMovies
FROM Movies;
");

        System.Console.WriteLine(sql);  // debugging:

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = db;
        cmd.CommandText = sql;

        object result = cmd.ExecuteScalar();

        db.Close();
        
        int numMovies = System.Convert.ToInt32(result);
				
				System.Console.WriteLine("Number of movies: {0}", numMovies);
      }
      catch (Exception ex)
      {
        System.Console.WriteLine();
        System.Console.WriteLine("**Error: {0}", ex.Message);
        System.Console.WriteLine();
      }
      finally
      {
				// make sure we close connection no matter what happens:
        if (db != null && db.State != ConnectionState.Closed)
          db.Close();
      }
    }

    static void getMovieInfoFromID(int id)
    {
      SqlConnection db = null;

      try
      {
        db = new SqlConnection(connectionInfo);
        db.Open();

        string sql = string.Format(@"
Select Movies.MovieID, MovieName, MovieYear, COUNT (*) AS NumReviews, ROUND(Avg(CONVERT (float,rating)), 6) as ratingAVG
FROM Reviews right JOIN Movies ON Reviews.MovieID = Movies.MovieID
WHERE Movies.MovieID = "+id+" Group By Movies.MovieID, MovieName, MovieYear");
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = db;
        cmd.CommandText = sql;

        object result = cmd.ExecuteScalar();
        DataSet ds = new DataSet();
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        adapter.Fill(ds);
        var rows = ds.Tables["TABLE"].Rows;
        foreach (DataRow row in rows){
            int movieId = System.Convert.ToInt32(row["MovieID"]);
            string movieName = System.Convert.ToString(row["MovieName"]);
            int movieYear = System.Convert.ToInt32(row["MovieYear"]);  
            int numReviews = System.Convert.ToInt32(row["NumReviews"]); 
            double avgRating = System.Convert.ToDouble(row["ratingAVG"]);
            System.Console.WriteLine("{0}\n '{1}'\n Year: {2}\n Num Reviews: {3}\n Avg rating: {4:0.00000}\n", movieId, movieName, movieYear, numReviews, avgRating);
        }
          
        db.Close();
      
      }
      catch (Exception ex)
      {
        System.Console.WriteLine();
        System.Console.WriteLine("**Error: {0}", ex.Message);
        System.Console.WriteLine();
      }
      finally
      {
				// make sure we close connection no matter what happens:
        if (db != null && db.State != ConnectionState.Closed)
          db.Close();
      }
    }

      static void getMovieInfoFromName(string input)
    {
      SqlConnection db = null;

      try
      {
        db = new SqlConnection(connectionInfo);
        db.Open();
            
        string sql = string.Format(@"
Select Movies.MovieID, MovieName, MovieYear, COUNT (*) AS NumReviews, ROUND(Avg(CONVERT (float,rating)), 6) as ratingAVG
FROM Reviews right JOIN Movies ON Reviews.MovieID = Movies.MovieID
WHERE MovieName LIKE '%"+input+"%' Group By Movies.MovieID, MovieName, MovieYear;");
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = db;
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();    
        cmd.CommandText = sql;
        adapter.Fill(ds);
        var rows = ds.Tables[0].Rows;
        db.Close();
        foreach (DataRow row in rows){
            int movieId = System.Convert.ToInt32(row["MovieID"]);
            string movieName = System.Convert.ToString(row["MovieName"]);
            int movieYear = System.Convert.ToInt32(row["MovieYear"]);  
            int numReviews = System.Convert.ToInt32(row["NumReviews"]); 
            double avgRating = System.Convert.ToDouble(row["ratingAVG"]);
            System.Console.WriteLine("{0}\n '{1}'\n Year: {2}\n Num Reviews: {3}\n Avg rating: {4:0.00000}\n", movieId, movieName, movieYear, numReviews, avgRating);
        }
         
      }
      catch (Exception ex)
      {
        System.Console.WriteLine();
        System.Console.WriteLine("**Error: {0}", ex.Message);
        System.Console.WriteLine();
      }
      finally
      {
				// make sure we close connection no matter what happens:
        if (db != null && db.State != ConnectionState.Closed)
          db.Close();
      }
    }
      
    static void getUserInfoFromID(int id)
    {
      SqlConnection db = null;

      try
      {
        db = new SqlConnection(connectionInfo);
        db.Open();

        string sql = string.Format(@"
Select Users.UserName, Users.UserID, Users.Occupation, COUNT (*) AS NumReviews, ROUND(Avg(CONVERT (float,rating)), 6) as ratingAVG
FROM Reviews right JOIN Users ON Reviews.UserID = Users.UserID
WHERE Users.UserID = "+id+" Group By UserName, Users.UserID, Occupation");

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = db;
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();    
        cmd.CommandText = sql;
        adapter.Fill(ds);
        var rows = ds.Tables[0].Rows;
        db.Close();
        foreach (DataRow row in rows){
            int userId = System.Convert.ToInt32(row["UserID"]);
            string userName = System.Convert.ToString(row["UserName"]);
            string occupation = System.Convert.ToString(row["Occupation"]);  
            int numReviews = System.Convert.ToInt32(row["NumReviews"]); 
            double avgRating = System.Convert.ToDouble(row["ratingAVG"]);
            System.Console.WriteLine("{0}\n User id:'{1}'\n Occupation: {2}\n Avg rating: {3:0.00000}\n Num reviews: {4}\n", userName, userId, occupation, avgRating, numReviews);
        }
        
      }
      catch (Exception ex)
      {
        System.Console.WriteLine();
        System.Console.WriteLine("**Error: {0}", ex.Message);
        System.Console.WriteLine();
      }
      finally
      {
				// make sure we close connection no matter what happens:
        if (db != null && db.State != ConnectionState.Closed)
          db.Close();
      }
    }
      
    static void getUserInfoFromName(string input)
    {
      SqlConnection db = null;

      try
      {
        db = new SqlConnection(connectionInfo);
        db.Open();

        string sql = string.Format(@"
Select Users.UserName, Users.UserID, Users.Occupation, COUNT (*) AS NumReviews, ROUND(Avg(CONVERT (float,rating)), 6) as ratingAVG
FROM Reviews right JOIN Users ON Reviews.UserID = Users.UserID
WHERE UserName LIKE '%"+input+"%' Group By UserName, Users.UserID, Occupation");

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = db;
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();    
        cmd.CommandText = sql;
        adapter.Fill(ds);
        var rows = ds.Tables[0].Rows;
        db.Close();
        foreach (DataRow row in rows){
            int userId = System.Convert.ToInt32(row["UserID"]);
            string userName = System.Convert.ToString(row["UserName"]);
            string occupation = System.Convert.ToString(row["Occupation"]);  
            int numReviews = System.Convert.ToInt32(row["NumReviews"]); 
            double avgRating = System.Convert.ToDouble(row["ratingAVG"]);
            System.Console.WriteLine("{0}\n User id:'{1}'\n Occupation: {2}\n Avg rating: {3:0.00000}\n Num reviews: {4}\n", userName, userId, occupation, avgRating, numReviews);
        }
        
      }
      catch (Exception ex)
      {
        System.Console.WriteLine();
        System.Console.WriteLine("**Error: {0}", ex.Message);
        System.Console.WriteLine();
      }
      finally
      {
				// make sure we close connection no matter what happens:
        if (db != null && db.State != ConnectionState.Closed)
          db.Close();
      }
    }
      
    static void getTop10()
    {
      SqlConnection db = null;

      try
      {
        db = new SqlConnection(connectionInfo);
        db.Open();

        string sql = string.Format(@"
Select TOP 10 Movies.MovieID, Movies.MovieName, Movies.MovieYear, COUNT (*) AS NumReviews, ROUND(Avg(CONVERT (float,rating)), 6) as ratingAVG
FROM Reviews right JOIN Movies ON Reviews.MovieID = Movies.MovieID
Group By Movies.MovieID, MovieName, MovieYear
ORDER By ratingAVG DESC
");

        Console.WriteLine("Rank\tMovieID\tNumReviews\tAvgRating\tMovieName");
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = db;
        cmd.CommandText = sql;
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();    
        cmd.CommandText = sql;
        adapter.Fill(ds);
        var rows = ds.Tables[0].Rows;
        //System.Console.WriteLine(rows.Count);
        db.Close();
        int count = 1;
        foreach (DataRow row in rows){
            int movieId = System.Convert.ToInt32(row["MovieID"]);
            string movieName = System.Convert.ToString(row["MovieName"]);
            int movieYear = System.Convert.ToInt32(row["MovieYear"]);  
            int numReviews = System.Convert.ToInt32(row["NumReviews"]); 
            double avgRating = System.Convert.ToDouble(row["ratingAVG"]);
            System.Console.WriteLine("{0}\t{1}\t{2}\t\t{3:0.00000}\t\t'{4}'",count,movieId,numReviews,avgRating,movieName);
            count++;
        }
        
        db.Close();
          
      }
      catch (Exception ex)
      {
        System.Console.WriteLine();
        System.Console.WriteLine("**Error: {0}", ex.Message);
        System.Console.WriteLine();
      }
      finally
      {
				// make sure we close connection no matter what happens:
        if (db != null && db.State != ConnectionState.Closed)
          db.Close();
      }
    }
      
    static string GetUserCommand()
    {
      System.Console.WriteLine();
      System.Console.WriteLine("What would you like?");
      System.Console.WriteLine("m. movie info");
      System.Console.WriteLine("t. top-10 info");
      System.Console.WriteLine("u. user info");
      System.Console.WriteLine("x. exit");
      System.Console.Write(">> ");

      string cmd = System.Console.ReadLine();

      return cmd.ToLower();
    }

    //
    // Main:
    //
    static void Main(string[] args)
    {
      System.Console.WriteLine("** Netflix Database App **");

      string cmd = GetUserCommand();

      while (cmd != "x")
      {
          if(cmd == "t"){
              getTop10();
          }
          
          else if (cmd == "m"){
              System.Console.WriteLine("Enter movie id or part of movie name>> ");
              string input = System.Console.ReadLine();
              int id;
              
              if(System.Int32.TryParse(input, out id)){
                   getMovieInfoFromID(id);
              }
              else{
                  input = input.Replace("'", "''");
                  getMovieInfoFromName(input);
              }
          }
          
          else if (cmd == "u"){
              System.Console.WriteLine("Enter user id or name>> ");
              string input = System.Console.ReadLine();
              int id;
              
              if(System.Int32.TryParse(input, out id)){
                   getUserInfoFromID(id);
              }
              else{
                  input = input.Replace("'", "''");
                  getUserInfoFromName(input);
              }
          }
          
          else{
              System.Console.WriteLine("Invalid input. Please try again>> ");
          }

        cmd = GetUserCommand();
      }

      System.Console.WriteLine();
      System.Console.WriteLine("** Done **");
      System.Console.WriteLine();
    }

  }//class
}//namespace

