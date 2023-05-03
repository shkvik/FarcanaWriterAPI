using Colorify;
using Colorify.UI;


static class ConsoleFormat
{
    public static Format _colorify = new Format(Theme.Dark);
    public static void Info(string msg)
    {
        _colorify.Write("info", Colors.txtSuccess);
        _colorify.Write(": ", Colors.txtDefault);
        _colorify.WriteLine(msg);
    }

    public static void Fail(string msg)
    {
        _colorify.Write("fail", Colors.txtDanger);
        _colorify.Write(": ", Colors.txtDefault);
        _colorify.WriteLine(msg);
    }

}
