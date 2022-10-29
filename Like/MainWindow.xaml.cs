using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YoutubeExplode;

namespace Like
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Setting setting;
        private string pathLink;

        
        public MainWindow()
        {
            InitializeComponent();
            setting = File.Exists("setting.json") ? 
                JsonConvert.DeserializeObject<Setting>(File.ReadAllText("setting.json")) : 
                new Setting("#FFFFFF", "Resources/like_1.png", 24);

            LikeImage.Source = new BitmapImage(new Uri(setting.Image, UriKind.Relative));
            TextCountLikes.FontSize = setting.Size;
            TextCountLikes.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(setting.Color));
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {
            File.WriteAllText("setting.json", JsonConvert.SerializeObject(setting));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText("setting.json", JsonConvert.SerializeObject(setting));
            this.Close();
        }

        private void FontColorPink_Click(object sender, RoutedEventArgs e)
        {
            setting.Color = "#DA4167";
            TextCountLikes.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(setting.Color));
        }

        private void FontColorRed_Click(object sender, RoutedEventArgs e)
        {
            setting.Color = "#BF0603";
            TextCountLikes.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(setting.Color));
        }

        private void FontColorBlack_Click(object sender, RoutedEventArgs e)
        {
            setting.Color = "#0D0A0B";
            TextCountLikes.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(setting.Color));
        }

        private void FontColorWhite_Click(object sender, RoutedEventArgs e)
        {
            setting.Color = "#FFFFFF";
            TextCountLikes.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(setting.Color));
        }

        private void FontLowSize_Click(object sender, RoutedEventArgs e)
        {
            setting.Size = 24;
            TextCountLikes.FontSize = setting.Size;
        }

        private void FontMiddleSize_Click(object sender, RoutedEventArgs e)
        {
            setting.Size = 36;
            TextCountLikes.FontSize = setting.Size;
        }

        private void FontLargeSize_Click(object sender, RoutedEventArgs e)
        {
            setting.Size = 44;
            TextCountLikes.FontSize = setting.Size;
        }

        private void DarkLike_Click(object sender, RoutedEventArgs e)
        {
            setting.Image = "Resources/like_1.png";
            LikeImage.Source = new BitmapImage(new Uri(setting.Image, UriKind.Relative));
        }

        private void RedHeartLike_Click(object sender, RoutedEventArgs e)
        {
            setting.Image = "Resources/like_2.png";
            LikeImage.Source = new BitmapImage(new Uri(setting.Image, UriKind.Relative));
        }

        private void BlueLike_Click(object sender, RoutedEventArgs e)
        {
            setting.Image = "Resources/like_3.png";
            LikeImage.Source = new BitmapImage(new Uri(setting.Image, UriKind.Relative));
        }

        private void DarkHeartLike_Click(object sender, RoutedEventArgs e)
        {
            setting.Image = "Resources/like_4.png";
            LikeImage.Source = new BitmapImage(new Uri(setting.Image, UriKind.Relative));
        }

        private void PinkLike_Click(object sender, RoutedEventArgs e)
        {
            setting.Image = "Resources/like_5.png";
            LikeImage.Source = new BitmapImage(new Uri(setting.Image, UriKind.Relative));
        }

        ~MainWindow()
        {
            File.WriteAllText("setting.json", JsonConvert.SerializeObject(setting));
        }

        private async void MenuItem_Click_AddLink(object sender, RoutedEventArgs e)
        {
            RequestLink dialog = new RequestLink();

            if (dialog.ShowDialog() == true) {
                pathLink = dialog.textLink.ToString();

                var youtube = new YoutubeClient();

                try
                {
                    var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));

                    while (await timer.WaitForNextTickAsync())
                    {
                        var video = await youtube.Videos.GetAsync(pathLink);
                        TextCountLikes.Text = video.Engagement.LikeCount.ToString();
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }

    public class Setting
    {
        public string Color { get; set; }
        public string Image { get; set; }
        public int Size { get; set; }

        public Setting(string color, string image, int size)
        {
            Color = color;
            Image = image;
            Size = size;
        }
    }
}
