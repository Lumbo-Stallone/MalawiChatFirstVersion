namespace MalawiChatFirstVersion;

public partial class ListOfAccount : ContentPage
{
	public ListOfAccount()
	{
		InitializeComponent();
      
       
        NavigationPage.SetHasNavigationBar(this, false);
    }
    AccountRegistration obj = new AccountRegistration();
    protected override async void OnAppearing()
    {
        try
        {
            contactsList.ItemsSource = await obj.ReadData();
        }
        catch (Exception)
        {
            _ = DisplayAlert("Info", "Empty List", "Ok");
        }
    }



}