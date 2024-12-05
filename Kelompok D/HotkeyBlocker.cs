using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class HotkeyBlocker
{
    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    private const int MOD_ALT = 0x0001;
    private const int VK_TAB = 0x09;
    private const int VK_F4 = 0x73;

    public void BlockHotkeys(Form form)
    {
        // Registrasi hotkey Alt+Tab
        if (!RegisterHotKey(form.Handle, 1, MOD_ALT, VK_TAB))
        {
            HandleRegisterHotkeyError("Alt+Tab");
        }

        // Registrasi hotkey Alt+F4
        if (!RegisterHotKey(form.Handle, 2, MOD_ALT, VK_F4))
        {
            HandleRegisterHotkeyError("Alt+F4");
        }

        form.FormClosing += Form_FormClosing;
    }

    private void Form_FormClosing(object sender, FormClosingEventArgs e)
    {
        Form form = (Form)sender;

        // Unregistrasi hotkey Alt+Tab
        if (!UnregisterHotKey(form.Handle, 1))
        {
            HandleUnregisterHotkeyError("Alt+Tab");
        }

        // Unregistrasi hotkey Alt+F4
        if (!UnregisterHotKey(form.Handle, 2))
        {
            HandleUnregisterHotkeyError("Alt+F4");
        }
    }

    private void HandleRegisterHotkeyError(string hotkey)
    {
        int errorCode = Marshal.GetLastWin32Error();
        MessageBox.Show($"Error registering {hotkey}: {errorCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void HandleUnregisterHotkeyError(string hotkey)
    {
        int errorCode = Marshal.GetLastWin32Error();
        MessageBox.Show($"Error unregistering {hotkey}: {errorCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}