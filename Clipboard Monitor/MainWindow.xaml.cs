using System.Windows;
using System.Windows.Threading;
namespace Clipboard_Monitor;



public partial class MainWindow : Window
{
    private DispatcherTimer _clipboardMonitorTime;

    public MainWindow()
    {
        InitializeComponent();
        _clipboardMonitorTime = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };

        _clipboardMonitorTime.Tick += ClipboardMonitorTime_Tick;
        _clipboardMonitorTime.Start();
    }

    private void ClipboardMonitorTime_Tick(object? sender, EventArgs e)
    {
        try
        {
            if (Clipboard.ContainsText())
            {
                var currentText = Clipboard.GetText();

                if (!listBox.Items.Contains(currentText)
                    && !string.IsNullOrWhiteSpace(currentText))
                {
                    listBox.Items.Add(currentText);
                    Activate();
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void listBox_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (listBox.SelectedItem != null)
        {
            listBox.Items.Remove(listBox.SelectedItem);
            Clipboard.Clear();
        }
    }

    private void listBox_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (listBox.SelectedItem != null)
        {
            Clipboard.SetText(listBox.SelectedItem.ToString());
        }
    }
}