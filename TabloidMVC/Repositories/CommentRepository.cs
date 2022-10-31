using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using TabloidMVC.Models;
using TabloidMVC.Utils;

namespace TabloidMVC.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration config) : base(config) { }
        public List<Comment> GetPostsComments(int postId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT c.Id, c.PostId, c.UserProfileId, c.Subject, c.Content, c.CreateDateTime, Post.Title, u.DisplayName
                        FROM Comment c
                        join Post on c.PostId = Post.Id
                        join UserProfile u on c.UserProfileId = u.Id
                    WHERE PostId = @postId";
                    cmd.Parameters.AddWithValue("@postId", postId);

                    var reader = cmd.ExecuteReader();

                    var comments = new List<Comment>();

                    while (reader.Read())
                    {
                        comments.Add(NewCommentFromReader(reader));
                    }

                    reader.Close();

                    return comments;
                }
            }
        }

        public void Add(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Comment (
                        PostId, UserProfileId, Subject, Content, CreateDateTime )
                    OUTPUT INSERTED.ID
                    VALUES (
                        @PostId, @UserProfileId, @Subject, @Content, @CreateDateTime )";
                    cmd.Parameters.AddWithValue("@PostId", comment.PostId);
                    cmd.Parameters.AddWithValue("@UserProfileId", comment.UserProfileId);
                    cmd.Parameters.AddWithValue("@CreateDateTime", comment.CreateDateTime);
                    cmd.Parameters.AddWithValue("@Subject", comment.Subject);
                    cmd.Parameters.AddWithValue("@Content", comment.Content);

                    comment.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        private Comment NewCommentFromReader(SqlDataReader reader)
        {
            Comment comment = new Comment()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                Subject = reader.GetString(reader.GetOrdinal("Subject")),
                Content = reader.GetString(reader.GetOrdinal("Content")),
                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                Post = new Post(),
                UserDisplayName = reader.GetString(reader.GetOrdinal("DisplayName"))
            };

            comment.Post.Title = reader.GetString(reader.GetOrdinal("Title"));

            return comment;
        }
    }
}
