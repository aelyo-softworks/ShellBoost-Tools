using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ShellBoost.Core;
using ShellBoost.Core.WindowsShell;

namespace DragDropViewer
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            AllowDrop = true;
            labelHint.BackColor = textBoxDragZone.BackColor;
        }

        protected override void OnResize(EventArgs e)
        {
            labelHint.Left = (Width - labelHint.Width) / 2;
            labelHint.Top = (Height - labelHint.Height) / 2;
            base.OnResize(e);
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            labelHint.Visible = false;
            drgevent.Effect = DragDropEffects.None;
            var sb = new StringBuilder();
            foreach (var format in drgevent.Data.GetFormats())
            {
                sb.AppendLine(format);

                var data = drgevent.Data.GetData(format);
                if (ShellDataObjectFormat.TryParseDataUsingKnownFormats(format, data, out var obj))
                {
                    if (obj is IReadOnlyList<ShellItemIdList> idls)
                    {
                        for (var i = 0; i < idls.Count; i++)
                        {
                            var idl = idls[i];
                            sb.AppendLine(" idl#" + i + ": " + idl.GetPath());
                        }
                    }
                    else if (obj is IReadOnlyList<FILEDESCRIPTOR> descs)
                    {
                        for (var i = 0; i < descs.Count; i++)
                        {
                            var desc = descs[i];
                            sb.AppendLine(" desc#" + i + ": " + desc);
                        }
                    }
                    else
                    {
                        sb.AppendLine(" " + obj + " (" + (obj?.GetType().Name) + ")");
                    }
                }
                else
                {
                    if (data is MemoryStream ms && ms.Length == 4)
                    {
                        var value = BitConverter.ToInt32(ms.ToArray(), 0);
                        sb.AppendLine(" " + GetDataString(data) + " (" + (data?.GetType().Name) + "): " + value + " 0x" + value.ToString("X8"));
                    }
                    else
                    {
                        sb.AppendLine(" " + GetDataString(data) + " (" + (data?.GetType().Name) + ")");
                    }
                }
                sb.AppendLine();
            }
            textBoxDragZone.Text = sb.ToString();
        }

        private static string GetDataString(object data)
        {
            if (data == null)
                return "<null>";

            if (data is string[] strings)
                return string.Join("| ", strings);

            if (data is MemoryStream ms)
                return "len:" + ms.Length;

            return string.Format("{0}", data);
        }
    }
}
