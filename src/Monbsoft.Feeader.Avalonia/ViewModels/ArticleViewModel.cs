using Humanizer;
using Monbsoft.Feeader.Avalonia.Models;

namespace Monbsoft.Feeader.Avalonia.ViewModels
{
    public class ArticleViewModel : ViewModelBase
    {
        public ArticleViewModel(Article article)
        {
            Title = article.Title;
            Date = article.Date.Humanize();
            Link = article.Link.AbsoluteUri;
            Summary = article.Summary ?? string.Empty;
        }

        /// <summary>
        /// Gets the humanized date of the article.
        /// </summary>
        public string Date { get; }
        /// <summary>
        /// Gets the url of the article.
        /// </summary>
        public string Link { get;  }
        /// <summary>
        /// Gets the summary of the article.
        /// </summary>
        public string Summary { get;  }
        /// <summary>
        /// Gets the title of the article.
        /// </summary>
        public string Title { get; }


    }
}
