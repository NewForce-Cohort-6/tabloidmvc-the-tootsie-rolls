SELECT Id, PostId, UserProfileId, Subject, Content, CreateDateTime
FROM Comment
WHERE PostId = 1;

--select * from post;

--SET IDENTITY_INSERT Comment ON
--INSERT INTO Comment ([Id], PostId, UserProfileId, [Subject], Content, CreateDateTime)
--VALUES (1, 1, 1, 'test1','comment 1',05/29/2015), 
--	(2, 1, 1, 'test2', 'comment 2', 10/31/2022), 
--	(3, 1, 1, 'test3', 'comment 3', 10/30/2022),
--	(4, 1, 1, 'test4', 'comment 4', 10/29/2022);
--SET IDENTITY_INSERT Comment OFF