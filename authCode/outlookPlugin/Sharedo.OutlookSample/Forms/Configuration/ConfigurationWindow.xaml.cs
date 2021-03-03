using System.Threading.Tasks;
using System.Windows;
using Sharedo.OutlookSample.Forms.OAuthLoginView;
using Sharedo.OutlookSample.Services;
using Sharedo.OutlookSample.Services.Models;
using Sharedo.OutlookSample.Util;

namespace Sharedo.OutlookSample.Forms.Configuration
{
    /// <summary>
    /// Interaction logic for ConfigurationWindow.xaml
    /// </summary>
    public partial class ConfigurationWindow : Window
    {
        /// <summary>
        /// Reference to the token manager UI controller which manages
        /// the state of tokens, their persistence and the configuration of
        /// the link to sharedo APIs.
        /// </summary>
        private readonly ITokenManagerUiController _tokens;

        public ConfigurationWindow(ITokenManagerUiController tokens)
        {
            _tokens = tokens;

            InitializeComponent();

            _btnChangeServer.Click += ChangeServerClicked;
            _btnConnect.Click += ConnectServerClicked;
            _btnLinkAccount.Click += LinkAccountClicked;
            _btnUnlinkAccount.Click += UnlinkAccountClicked;

            _sharedoServer.Text = _tokens.SharedoUrl;
            _identityServer.Text = _tokens.IdentityUrl;

            SetFormStates();

            this.Loaded += OnStartup;
        }

        private async void OnStartup(object sender, RoutedEventArgs e)
        {
            // Ensure we have an access token that is fresh etc. 
            await _tokens.GetAccessTokenAsync();

            // Refresh state
            SetFormStates();
        }

        private void SetFormStates()
        {
            if( ! _tokens.IsLinkConfigured )
            {
                _errorPanel.Visibility = Visibility.Visible;
                _errorText.Text = "Please enter your sharedo and identity urls to link your account";

                _sharedoServer.IsEnabled = true;
                _identityServer.IsEnabled = true;
                _btnConnect.IsEnabled = true;
                _btnChangeServer.Visibility = Visibility.Collapsed;

                _accountLinked.Visibility = Visibility.Collapsed;
                _noAccountLinked.Visibility = Visibility.Collapsed;

                return;
            }

            _errorPanel.Visibility = Visibility.Collapsed;

            _sharedoServer.IsEnabled = false;
            _identityServer.IsEnabled = false;
            _btnConnect.IsEnabled = false;
            _btnChangeServer.Visibility = Visibility.Visible;

            if (_tokens.State == TokenStatus.Success)
            {
                _noAccountLinked.Visibility = Visibility.Collapsed;
                _accountLinked.Visibility = Visibility.Visible;
                _identity.Text = _tokens.UserIdentity;
                _name.Text = _tokens.UserName;
            }

            if (_tokens.State == TokenStatus.RefreshTokenInvalid)
            {
                _errorPanel.Visibility = Visibility.Visible;
                _errorText.Text = "Your sharedo credentials have expired, please re-link your account";

                _noAccountLinked.Visibility = Visibility.Visible;
                _accountLinked.Visibility = Visibility.Collapsed;
            }

            if (_tokens.State == TokenStatus.NoTokens)
            {
                _errorPanel.Visibility = Visibility.Visible;
                _errorText.Text = "Please link your account";
                _noAccountLinked.Visibility = Visibility.Visible;
                _accountLinked.Visibility = Visibility.Collapsed;
            }
        }

        private async void ChangeServerClicked(object sender, RoutedEventArgs e)
        {
            // Sharedo link urls are already configured, but user wants to change
            // them. Revoke any tokens and clear the current link info.
            await UnlinkAccountAsync(false);
            _tokens.ConfigureLink(null, null);
            SetFormStates();
        }

        private async void ConnectServerClicked(object sender, RoutedEventArgs e)
        {
            // User has set the urls and now wants to connect/setup their link
            if (!_sharedoServer.Text.IsValidUrl())
            {
                Error("Sharedo URL does not appear to be a valid url", "Please enter a valid URL");
                return;
            }

            if (!_identityServer.Text.IsValidUrl())
            {
                Error("Identity URL does not appear to be a valid url", "Please enter a valid URL");
                return;
            }

            _tokens.ConfigureLink(_sharedoServer.Text, _identityServer.Text);
            await LinkAccountAsync();
        }

        private async void LinkAccountClicked(object sender, RoutedEventArgs e)
        {
            await LinkAccountAsync();
        }

        private async void UnlinkAccountClicked(object sender, RoutedEventArgs e)
        {
            await UnlinkAccountAsync();
        }

        private async Task LinkAccountAsync()
        {
            if (!_tokens.IsLinkConfigured) return;

            // Ensure we're fully unlinked
            await UnlinkAccountAsync(false);

            // Start the link account conversation
            var loginView = new LoginWebView(_tokens) {Owner = this};
            loginView.Start();

            SetFormStates();
        }

        private async Task UnlinkAccountAsync(bool updateForm = true)
        {
            await _tokens.RevokeTokensAsync();

            if (updateForm)
                SetFormStates();
        }

        private void Error(string message, string caption)
        {
            MessageBox.Show(this, message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
