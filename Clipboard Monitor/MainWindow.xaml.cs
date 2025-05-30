using System.Windows;
using System.Windows.Threading;
namespace Clipboard_Monitor;

public partial class MainWindow : Window
{
    private DispatcherTimer _clipboardMonitorTime;
    private const string ClipBoardBusy = "Clipboard is busy, please try again.";

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
                    itemCount.Text = listBox.Items.Count.ToString();
                    Activate();
                }
            }
        }
        catch (System.Runtime.InteropServices.COMException)
        {
            itemCount.Text = ClipBoardBusy;
        }
        catch (Exception ex)
        {
            itemCount.Text = ex.Message;
        }
    }

    private void listBox_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        try
        {
            if (listBox.SelectedItem != null)
            {
                if (Clipboard.GetText().Equals(listBox.SelectedItem.ToString()))
                    Clipboard.Clear();
                listBox.Items.Remove(listBox.SelectedItem);
                itemCount.Text = listBox.Items.Count.ToString();
            }
        }
        catch (System.Runtime.InteropServices.COMException)
        {
            itemCount.Text = ClipBoardBusy;
        }
        catch (Exception ex)
        {
            itemCount.Text = ex.Message;
        }

    }

    private void listBox_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        try
        {
            if (listBox.SelectedItem != null)
                Clipboard.SetText(listBox.SelectedItem.ToString());
        }
        catch (System.Runtime.InteropServices.COMException)
        {
            itemCount.Text = ClipBoardBusy;
        }
        catch (Exception ex)
        {
            itemCount.Text = ex.Message;
        }
    }

    private void ClearListMenuItem_Click(object sender, RoutedEventArgs e)
    {
        listBox.Items.Clear();
        itemCount.Text = "0";
        Clipboard.Clear();
    }
}