SELECT p.Id, p.Title, p.Content, 
                              p.ImageLocation AS HeaderImage,
                              p.CreateDateTime, p.PublishDateTime, p.IsApproved,
                              p.CategoryId, p.UserProfileId,
                              c.[Name] AS CategoryName,
                              u.FirstName, u.LastName, u.DisplayName, 
                              u.Email, u.CreateDateTime, u.ImageLocation AS AvatarImage,
                              u.UserTypeId, 
                              ut.[Name] AS UserTypeName,
                              co.Id as CommentId
                         FROM Post p
                              LEFT JOIN Category c ON p.CategoryId = c.id
                              LEFT JOIN UserProfile u ON p.UserProfileId = u.id
                              LEFT JOIN UserType ut ON u.UserTypeId = ut.id
                              LEFT JOIN Comment co on p.Id = co.PostId
                        WHERE IsApproved = 1 AND PublishDateTime < SYSDATETIME()

--SELECT c.Id, c.PostId, c.UserProfileId, c.Subject, c.Content, c.CreateDateTime, Post.Title, u.DisplayName
--                        FROM Comment c
--                        join Post on c.PostId = Post.Id
--                        join UserProfile u on c.UserProfileId = u.Id
--                    WHERE c.Id = 2