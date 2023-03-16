namespace MalawiChatFirstVersion;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		Navigation.PushAsync(new AccountRegistration());
    }
}