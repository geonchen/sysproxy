using System.Windows.Forms;

namespace sysproxy.Utils
{
    public  class MsgBox
    {
        private const string caption = "sysproxy";
        public static void Show(string text)
        {
            MessageBox.Show(I18N.GetString(text), caption);
        }

        public static void Error(string text)
        {
            MessageBox.Show(I18N.GetString(text), caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ErrorRes(string text)
        {
            return MessageBox.Show(I18N.GetString(text), caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Info(string text)
        {
            MessageBox.Show(I18N.GetString(text), caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Warning(string text)
        {
            MessageBox.Show(I18N.GetString(text), caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
