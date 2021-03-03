using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using mshtml;
using Sharedo.OutlookSample.Services;

namespace Sharedo.OutlookSample.Forms.OAuthLoginView
{
    public partial class LoginWebView : Window
    {
        private readonly ITokenManagerUiController _tokens;

        public LoginWebView(ITokenManagerUiController tokens)
        {
            _tokens = tokens;
    
            InitializeComponent();

            _webView.Navigating += WebViewNavigating;
        }

        public void Start()
        {
            var startUrl = _tokens.CreateAuthorisationCodeUrl();
            _webView.Navigate(startUrl);
            ShowDialog();
        }

        private void StartLogin()
        {
            var startUrl = _tokens.CreateAuthorisationCodeUrl();
            _webView.Navigate(startUrl);
        }

        private async void WebViewNavigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            // Determine if we're navigating to our known redirect uri, if so, grab and process the data
            if (e.Uri.ToString().StartsWith(App.ClientRedirectUrl))
            {
                e.Cancel = true;

                var responseValues = GetFormInputsFromBrowser(sender as WebBrowser);
                var response = _tokens.ParseAuthorisationResponse(responseValues);

                if (response.IsError)
                {
                    Error("Could not link account - authorisation failed", "Error linking account");
                    StartLogin();
                    return;
                }

                var result = await _tokens.GetTokensFromAuthorisationCodeResponseAsync(response);
                if (!result)
                {
                    Error("Could not link account - failed to obtain access/refresh tokens", "Error linking account");
                    StartLogin();
                    return;
                }

                Close();
            }
        }

        private Dictionary<string, string> GetFormInputsFromBrowser(WebBrowser sender)
        {
            var result = new Dictionary<string, string>();
            if (sender == null) return result;

            var document = (IHTMLDocument3)sender.Document;
            if (document == null) return result;

            var inputs = document.getElementsByTagName("INPUT").OfType<IHTMLElement>();
            foreach (var input in inputs)
            {
                var key = input.getAttribute("name");
                var value = input.getAttribute("value");
                result.Add(key, value);
            }

            return result;
        }

        private void Error(string message, string caption)
        {
            MessageBox.Show(this, message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
