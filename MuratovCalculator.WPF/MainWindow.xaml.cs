using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using MuratovCalculator.Core;

namespace MuratovCalculator.WPF;

public partial class MainWindow : Window
{
    private readonly Calculator _calc = new();

    private double _firstOperand;
    private string _pendingOp = string.Empty;
    private bool _newEntry = true;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Digit_Click(object sender, RoutedEventArgs e)
    {
        string digit = ((Button)sender).Content.ToString()!;

        if (_newEntry)
        {
            Display.Text = digit;
            _newEntry = false;
        }
        else
        {
            Display.Text = Display.Text == "0" ? digit : Display.Text + digit;
        }
    }

    private void Dot_Click(object sender, RoutedEventArgs e)
    {
        if (_newEntry)
        {
            Display.Text = "0.";
            _newEntry = false;
            return;
        }

        if (!Display.Text.Contains('.'))
            Display.Text += ".";
    }

    private void Sign_Click(object sender, RoutedEventArgs e)
    {
        if (Display.Text.StartsWith('-'))
            Display.Text = Display.Text[1..];
        else if (Display.Text != "0")
            Display.Text = "-" + Display.Text;
    }

    private void Backspace_Click(object sender, RoutedEventArgs e)
    {
        if (Display.Text.Length > 1)
            Display.Text = Display.Text[..^1];
        else
            Display.Text = "0";
    }

    private void Clear_Click(object sender, RoutedEventArgs e)
    {
        Display.Text = "0";
        _firstOperand = 0;
        _pendingOp = string.Empty;
        _newEntry = true;
    }

    private void Operator_Click(object sender, RoutedEventArgs e)
    {
        if (!TryParseDisplay(out double value))
            return;

        _firstOperand = value;
        _pendingOp = ((Button)sender).Content.ToString()!;
        _newEntry = true;
    }

    private void Equals_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(_pendingOp))
            return;

        if (!TryParseDisplay(out double second))
            return;

        try
        {
            double result = _calc.Calculate(_firstOperand, second, _pendingOp);
            Display.Text = result.ToString("G15", CultureInfo.InvariantCulture);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            Display.Text = "0";
        }
        finally
        {
            _pendingOp = string.Empty;
            _newEntry = true;
        }
    }

    private bool TryParseDisplay(out double value)
    {
        if (double.TryParse(Display.Text,
                            NumberStyles.Float,
                            CultureInfo.InvariantCulture,
                            out value))
            return true;

        MessageBox.Show("Некорректное число.", "Ошибка",
            MessageBoxButton.OK, MessageBoxImage.Warning);
        Display.Text = "0";
        _newEntry = true;
        return false;
    }
}