using Ecycle.Models;
using Ecycle.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Ecycle.ViewModels
{
    class ArticleViewModel
    {
        private ObservableCollection<ArticleModel> _articles = new();
        public ObservableCollection<ArticleModel> Articles { get => _articles; private set { 
                _articles = value;
            } }

        public ICommand OpenLinkCommand { get; }

        public ArticleViewModel()
        {
            _ = FetchArticles();
            OpenLinkCommand = new RelayCommand<string>(OpenLink, null);
        }

        private async Task FetchArticles()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://ecycle-be-hnawbcbvhkfse3b3.southeastasia-01.azurewebsites.net/article");

            response.EnsureSuccessStatusCode();
            string? content = await response.Content.ReadAsStringAsync();
            if (content != null)
            {
                List<ArticleModel> list = JsonConvert.DeserializeObject<List<ArticleModel>>(content) ?? new();
                Articles.Clear();
                foreach (var item in list)
                {
                    Articles.Add(item);
                }
            }
        }

        private void OpenLink(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                    !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                {
                    url = "http://" + url;
                }

                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
        }
    }
}
