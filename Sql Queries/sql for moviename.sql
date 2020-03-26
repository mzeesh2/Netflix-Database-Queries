Select Movies.MovieID, MovieName, MovieYear, COUNT (*) AS NumReviews, ROUND(Avg(CONVERT (float,rating)), 6) as ratingAVG
FROM Reviews right JOIN Movies ON Reviews.MovieID = Movies.MovieID
WHERE MovieName LIKE '%matrix%'
Group By Movies.MovieID, MovieName, MovieYear