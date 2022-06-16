using Avalonia.Media.Imaging;
using Humanizer;
using Monbsoft.Feeader.Avalonia.Models;
using Monbsoft.Feeader.Avalonia.Services;
using ReactiveUI;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Monbsoft.Feeader.Avalonia.ViewModels
{
    public class ArticleViewModel : ViewModelBase
    {
        private Bitmap? _picture;
        private Uri? _pictureUri;

        public ArticleViewModel(Article article)
        {
            Title = article.Title;
            Date = article.Date.Humanize();
            Link = article.Link.AbsoluteUri;
            _pictureUri = article.PictureUri;
            Summary = article.Summary ?? string.Empty;
            _pictureUri = article.PictureUri;
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
        /// Gets the picture of the article.
        /// </summary>
        public Bitmap? Picture
        {
            get => _picture;
            private set => this.RaiseAndSetIfChanged(ref _picture, value);
        }
        /// <summary>
        /// Gets the summary of the article.
        /// </summary>
        public string Summary { get;  }
        /// <summary>
        /// Gets the title of the article.
        /// </summary>
        public string Title { get; }

        public async  Task LoadPictureAsync(CancellationToken cancellationToken)
        {
            if (_pictureUri != null)
            {
                Picture = await PictureService.LoadPictureBitmapAsync(_pictureUri, cancellationToken);
            }            
        }
    }
}
