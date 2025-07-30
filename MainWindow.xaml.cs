using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace IELTSTestApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>

public partial class MainWindow : Window
{
    // Timer related fields
    private DispatcherTimer? _timer;
    private TimeSpan _timeLeft;
    private bool _isTimerRunning = false;

    // Full screen related fields
    private bool isFullscreen = false;
    private WindowState previousWindowState;
    private WindowStyle previousWindowStyle;
    private ResizeMode previousResizeMode;

    // Theme toggle related fields
    private bool isDarkMode = false;

    public MainWindow()
    {
        InitializeComponent();

        TypingBox.TextChanged += TypingBoxTextChanged;

        FontSizeSlider.ValueChanged += FontSizeSliderValueChanged;
    }

    // Count words in the TextBox
    private void TypingBoxTextChanged(object sender, TextChangedEventArgs e)
    {
        // Check if the text contains Vietnamese characters
        string currentText = TypingBox.Text;
        if (ContainsVietnameseChars(currentText))
        {
            // Remove Vietnamese characters
            string filtered = RemoveVietnameseChars(currentText);
            TypingBox.Text = filtered;

            // Set the caret position to the end of the text
            TypingBox.CaretIndex = filtered.Length;
        }

        string text = TypingBox.Text;

        int wordCount = text.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

        WordCountLabel.Text = wordCount.ToString();
    }
    // Change font size of the TextBox based on the slider value
    private void FontSizeSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        int fontSize = (int)e.NewValue;

        // Change the font size of the TextBox
        TypingBox.FontSize = fontSize;

        // Update the label
        FontSizeLabel.Text = fontSize.ToString();
    }
    
    // Check if the text contains Vietnamese characters
    private bool ContainsVietnameseChars(string text)
    {
        // Danh sách các ký tự tiếng Việt có dấu
        string vietnameseChars = "ăâđêôơưáàảãạấầẩẫậắằẳẵặéèẻẽẹếềểễệíìỉĩịóòỏõọốồổỗộớờởỡợúùủũụứừửữựýỳỷỹỵ" +
                                  "ĂÂĐÊÔƠƯÁÀẢÃẠẤẦẨẪẬẮẰẲẴẶÉÈẺẼẸẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌỐỒỔỖỘỚỜỞỠỢÚÙỦŨỤỨỪỬỮỰÝỲỶỸỴ";

        return text.Any(c => vietnameseChars.Contains(c));
    }

    // Remove Vietnamese characters from the input text
    private string RemoveVietnameseChars(string input)
    {
        string[] vietnameseSigns = new string[]
        {
        "aAeEoOuUiIdDyY",
        "áàạảãâấầậẩẫăắằặẳẵ",
        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
        "éèẹẻẽêếềệểễ",
        "ÉÈẸẺẼÊẾỀỆỂỄ",
        "óòọỏõôốồộổỗơớờợởỡ",
        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
        "úùụủũưứừựửữ",
        "ÚÙỤỦŨƯỨỪỰỬỮ",
        "íìịỉĩ",
        "ÍÌỊỈĨ",
        "đ",
        "Đ",
        "ýỳỵỷỹ",
        "ÝỲỴỶỸ"
        };

        for (int i = 1; i < vietnameseSigns.Length; i++)
        {
            for (int j = 0; j < vietnameseSigns[i].Length; j++)
            {
                input = input.Replace(vietnameseSigns[i][j], vietnameseSigns[0][i - 1]);
            }
        }
        return input;
    }

    // Start the timer when the button is clicked
    private void StartButtonClick(object sender, RoutedEventArgs e)
    {
        if (!_isTimerRunning)
        {
            _timeLeft = TimeSpan.FromMinutes(60);
            TimerLabel.Text = _timeLeft.ToString(@"mm\:ss");

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += TimerTick;
            _timer.Start();
            _isTimerRunning = true;

            TypingBox.IsReadOnly = false;
        }
    }

    private void TimerTick(object? sender, EventArgs e)
    {
        if (_timeLeft.TotalSeconds > 0)
        {
            _timeLeft = _timeLeft.Subtract(TimeSpan.FromSeconds(1));
            TimerLabel.Text = _timeLeft.ToString(@"mm\:ss");
        }
        else
        {
            _timer?.Stop();
            TypingBox.IsReadOnly = true;
            TimerLabel.Text = "Time's up!";
            _isTimerRunning = false;
        }
    }

    // Reset the timer when the button is clicked
    private void ResetButtonClick(object sender, RoutedEventArgs e)
    {
        if (_timer != null)
        {
            _timer.Stop();
        }

        _isTimerRunning = false;
        _timeLeft = TimeSpan.Zero;
        TimerLabel.Text = "60:00";
        TypingBox.Text = string.Empty;
        TypingBox.IsReadOnly = false;
    }

    // Toggle fullscreen mode when the button is clicked
    private void ToggleFullscreenClick(object sender, RoutedEventArgs e)
    {
        if (!isFullscreen)
        {
            previousWindowState = this.WindowState;
            previousWindowStyle = this.WindowStyle;
            previousResizeMode = this.ResizeMode;

            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowState = WindowState.Maximized;

            isFullscreen = true;
        }
        else
        {
            this.WindowStyle = previousWindowStyle;
            this.ResizeMode = previousResizeMode;
            this.WindowState = previousWindowState;

            isFullscreen = false;
        }
    }
    private void WindowKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.F11)
        {
            ToggleFullscreenClick(this, new RoutedEventArgs());
        }
    }

    // Toggle dark/light mode when the button is clicked
    private void ToggleThemeClick(object sender, RoutedEventArgs e)
    {
        if (!isDarkMode)
        {
            Application.Current.Resources["BackgroundBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1E1E1E"));
            Application.Current.Resources["ForegroundBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            isDarkMode = true;
        }
        else
        {
            Application.Current.Resources["BackgroundBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            Application.Current.Resources["ForegroundBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF000000"));
            isDarkMode = false;
        }
    }

    // Check if the text contains Vietnamese characters

}