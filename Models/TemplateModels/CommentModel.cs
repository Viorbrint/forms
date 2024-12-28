namespace Forms.Data.Entities;

public class CommentModel
{
    public string Text { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;

    public static CommentModel MapComment(Comment comment)
    {
        return new() { Text = comment.CommentText, AuthorName = comment.Author.UserName! };
    }
}
