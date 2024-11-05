using System.Windows.Controls;
using Microsoft.Web.WebView2.Core;
using RevitWebView2.Browser.Services;
using RevitWebView2.Browser.ViewModels;
using Visibility = System.Windows.Visibility;

namespace RevitWebView2.Browser.Views;

public sealed partial class BrowserView
{
    private const string HostUrl = "https://localhost:7096/";
    private readonly WebMessageService _service;
    private readonly IServiceProvider _serviceProvider;

    public BrowserView(BrowserViewModel viewModel, WebMessageService service, IServiceProvider serviceProvider)
    {
        DataContext = viewModel;
        viewModel.UpdateRequest += (_, _) => UpdatePageAsync();

        _serviceProvider = serviceProvider;

        InitializeComponent();
        _service = service;
        _service.WebView = WebView;
        
        InitializeWebView();
    }

    private async void InitializeWebView()
    {
        Navigate(typeof(LoadingPage));
        await WebView.EnsureCoreWebView2Async();
        WebView.CoreWebView2.Navigate(HostUrl);
        WebView.CoreWebView2.NavigationCompleted += OnNavigationCompleted;
        WebView.CoreWebView2.WebMessageReceived += OnWebMessageReceived;
    }

    private void OnNavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
    {
        if (args.IsSuccess)
        {
            ShowWebView();
        }
        else
        {
            Navigate(typeof(ErrorPage));
        }
    }

    private async void OnWebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs args)
    {
        var message = args.TryGetWebMessageAsString();
        await _service.HandleMessageAsync(message);
    }

    private async void UpdatePageAsync()
    {
        Navigate(typeof(LoadingPage));
        await WebView.EnsureCoreWebView2Async();
        WebView.CoreWebView2.Navigate(HostUrl);
    }

    private void Navigate(Type pageType)
    {
        var page = (Page)_serviceProvider.GetService(pageType);
        if (page is null) throw new InvalidOperationException("No page registered");

        page.DataContext = DataContext;
        WebView.Visibility = Visibility.Collapsed;
        NavigationPresenter.Visibility = Visibility.Visible;
        NavigationPresenter.Navigate(page);
    }

    private void ShowWebView()
    {
        WebView.Visibility = Visibility.Visible;
        NavigationPresenter.Visibility = Visibility.Collapsed;
    }
}