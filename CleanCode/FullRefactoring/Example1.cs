using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;

namespace Project.UserControls
{
    public class PostControl : System.Web.UI.UserControl
    {
        private readonly PostDbContext _dbContext;
        private readonly Post _post;
        public Label PostBody { get; set; }
        public Label PostTitle { get; set; }
        public int? PostId { get; set; }

        public PostControl()
        {
            _dbContext = new PostDbContext();
            _post = new Post(
                Convert.ToInt32(PostId.Value),
                PostTitle.Text.Trim(),
                PostBody.Text.Trim()
            );
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                HandlePost();
            else
                DisplayPostForm();
        }

        private void HandlePost()
        {
            var results = _post.ValidatePost();

            if(results.IsValid)
                SavePostOnDb(_post);
            else
                DisplayPostErrorsList(results);
        }

        private void DisplayPostErrorsList(ValidationResult results)
        {
            BulletedList summary = (BulletedList)FindControl("ErrorSummary");

            foreach(var failure in results.Errors)
            {
                Label errorMessage = FindControl(failure.PropertyName + "Error") as Label;

                if(errorMessage == null)
                    summary.Items.Add(new ListItem(failure.ErrorMessage));
                else
                    errorMessage.Text = failure.ErrorMessage;
            }
        }

        private void DisplayPostForm()
        {
            Post entity = _dbContext.Posts.SingleOrDefault(p => p.Id == Convert.ToInt32(Request.QueryString["id"]));
            PostBody.Text = entity.Body;
            PostTitle.Text = entity.Title;
        }

        private void SavePostOnDb(Post entity)
        {
            _dbContext.Posts.Add(entity);
            _dbContext.SaveChanges();
        }
    }

    #region helpers

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public IEnumerable<ValidationError> Errors { get; set; }
    }

    public class ValidationError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public Post(int id, string title, string body)
        {
            Id = id;
            Title = title;
            Body = body;
        }
        
        public ValidationResult ValidatePost()
        {
            throw new NotImplementedException();
        }
    }

    public class DbSet<T> : IQueryable<T>
    {
        public void Add(T entity)
        {
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Expression Expression
        {
            get { throw new NotImplementedException(); }
        }

        public Type ElementType
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryProvider Provider
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class PostDbContext
    {
        public DbSet<Post> Posts { get; set; }

        public void SaveChanges()
        {
        }
    }
    #endregion

}