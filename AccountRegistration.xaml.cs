using SQLite;

namespace MalawiChatFirstVersion
{
    public partial class AccountRegistration : ContentPage
    {
        
        public AccountRegistration()
        {
            InitializeComponent();
            Picture.Items.Add("Take a Picture");
            Picture.Items.Add("Upload a Picture");

            Gender.Items.Add("Male");
            Gender.Items.Add("Female");
            Gender.Items.Add("Other");
        }
        private byte[] GetImageBytes(Stream stream)
        {
            byte[] ImageBytes;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                stream.CopyTo(memoryStream);
                ImageBytes = memoryStream.ToArray();
            }
            return ImageBytes;
        }
        string PictureInByte;
        private async void Picture_SelectedIndexChanged(object sender, EventArgs e)
        {
           string choice= Picture.Items[Picture.SelectedIndex].ToString();

            if (choice== "Take a Picture")
            {
                //if (MediaPicker.Default.IsCaptureSupported)
                //{
                //    FileResult photo =await MediaPicker.Default.CapturePhotoAsync();

                //    if (photo != null)
                //    {
                //        // save the file into local storage
                //        string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                //        using Stream sourceStream = await photo.OpenReadAsync();
                //        using FileStream localFileStream = File.OpenWrite(localFilePath);

                //        await sourceStream.CopyToAsync(localFileStream);
                //    }
                //}
            }
            else if(choice== "Upload a Picture")
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle="Pictures",
                    FileTypes = FilePickerFileType.Images
                }) ;
                var streams = await result.OpenReadAsync();

                var result1 = GetImageBytes(streams);
                PictureInByte = Convert.ToBase64String(result1);
            
            }
            else
            {

            }
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

      

      
        //Sqlite table creation
        static string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ChatDatabase.db3");
        public SQLiteAsyncConnection _database = new SQLiteAsyncConnection(dbPath);
        //Add Data an account Method
        public void AddAccount()
        {
             _database.CreateTableAsync<AccountTable>();

            var account = new AccountTable
            {
                UserName = UserName.Text,
                Age=int.Parse(Age.Text),
                Gender= gender,
                Biography=Bio.Text,
                Picture= PictureInByte,
                Password=password.Text
            };
            _database.InsertAsync(account);
             DisplayAlert("Info", "Saved" + " ", "Ok");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            AddAccount();
        }
        string gender;
        private void Gender_SelectedIndexChanged(object sender, EventArgs e)
        {
            gender = Gender.Items[Gender.SelectedIndex].ToString();
        }
        public Task<List<AccountTable>> ReadData()
        {
            return _database.Table<AccountTable>().ToListAsync();
        }
        //Delete Data from Check In and Out Table
        public Task<int> DeleteData(AccountTable delete)
        {
            return _database.DeleteAsync(delete);
        }
        //Search Data from Check in and out Table
        public Task<List<AccountTable>> Search(string search)
        {
            return _database.Table<AccountTable>().Where(p => p.UserName.Contains(search)).ToListAsync();
        }
        //Update Data from Check In and Out Table
        public Task<int> UpdateData(AccountTable update)
        {
            return _database.UpdateAsync(update);
        }
    }
    public class AccountTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Biography { get; set; }
        public string Password { get; set; }
        public ImageSource Picture { get; set; }

    }
}