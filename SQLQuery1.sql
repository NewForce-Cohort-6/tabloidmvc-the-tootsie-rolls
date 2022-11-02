--update comment
--set 
--	CreateDateTime = '2022-08-01 00:00:00.000'
--where id = 4;

SELECT c.Id, c.PostId, c.UserProfileId, c.Subject, c.Content, c.CreateDateTime, Post.Title, u.DisplayName
FROM Comment c
join Post on c.PostId = Post.Id
join UserProfile u on c.UserProfileId = u.Id
WHERE PostId = 1;

select * from UserProfile;

--SET IDENTITY_INSERT Comment ON
--INSERT INTO Comment ([Id], PostId, UserProfileId, [Subject], Content, CreateDateTime)
--VALUES (1, 1, 1, 'test1','comment 1',05/29/2015), 
--	(2, 1, 1, 'test2', 'comment 2', 10/31/2022), 
--	(3, 1, 1, 'test3', 'comment 3', 10/30/2022),
--	(4, 1, 1, 'test4', 'comment 4', 10/29/2022),
--  (5, 2, 1, 'test5', 'comment 5', 10/29/2022);
--SET IDENTITY_INSERT Comment OFF