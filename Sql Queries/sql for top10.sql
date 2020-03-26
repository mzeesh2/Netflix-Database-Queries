Select TOP 10 Movies.MovieID, Movies.MovieName, Movies.MovieYear, COUNT (*) AS NumReviews, ROUND(Avg(CONVERT (float,rating)), 6) as ratingAVG
FROM Reviews right JOIN Movies ON Reviews.MovieID = Movies.MovieID
Group By Movies.MovieID, MovieName, MovieYear
ORDER By ratingAVG DESC