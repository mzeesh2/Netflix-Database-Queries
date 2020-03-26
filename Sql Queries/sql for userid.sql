Select Users.UserName, Users.UserID, Users.Occupation, COUNT (*) AS NumReviews, ROUND(Avg(CONVERT (float,rating)), 6) as ratingAVG
FROM Reviews right JOIN Users ON Reviews.UserID = Users.UserID
WHERE Users.UserID = 1461435
Group By UserName, Users.UserID, Occupation