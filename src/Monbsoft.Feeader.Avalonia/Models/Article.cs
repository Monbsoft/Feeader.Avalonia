using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Monbsoft.Feeader.Avalonia.Models;

public class Article
{
    public Article(string id, string title, DateTime date, Uri link)
    {
        Id = id;
        Date = date;
        Title = title.Trim();
        Link = link;
    }

    /// <summary>
    /// Gets the published date of the articles.
    /// </summary>
    public DateTime Date { get; }
    /// <summary>
    /// Gets the summary of the article.
    /// </summary>
    public string? Summary { get; private set; }
    /// <summary>
    /// Gets the id of the article.
    /// </summary>
    public string Id { get; }
    /// <summary>
    /// Gets the picture uri of the article.
    /// </summary>
    public Uri? PictureUri { get; private set; }
    /// <summary>
    /// Gets the title of the article.
    /// </summary>
    public string Title { get; }
    /// <summary>
    /// Gets the URI of the article.
    /// </summary>
    public Uri Link { get; }

    public override bool Equals(object? obj)
    {
        Article? other = obj as Article;
        if(ReferenceEquals(null, other)) 
            return false;
        if(ReferenceEquals (this, other))
            return true;
        return Link == other.Link;
    }
    public override int GetHashCode()
    {
        //return Link.GetComponents(UriComponents.Host | UriComponents.Path, UriFormat.Unescaped).GetHashCode();
        return Id.GetHashCode();
    }
    public override string ToString()
    {
        return Title;
    }
    public Article WithPicture(Uri? picture)
    {
        PictureUri = picture;
        return this;
    }
    public Article WithSummary(string? summary)
    {
        Summary = summary;
        return this;
    }
}
