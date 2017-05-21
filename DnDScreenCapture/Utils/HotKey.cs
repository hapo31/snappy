using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DnDScreenCapture.Utils
{
    class HotKey : IDisposable
    {
        public HotKey()
        {

        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private class HotKeyWrapper : Form
        {
            [DllImport("user32.dll")]
            extern static int RegisterHotKey(IntPtr HWnd, int ID, ModKey MOD_KEY, Keys KEY);

            [DllImport("user32.dll")]
            extern static int UnregisterHotKey(IntPtr HWnd, int ID);
            const int WM_HOTKEY = 0x0312;
            int id;
            ThreadStart proc;

            public HotKeyWrapper(ModKey modKey, Keys key, ThreadStart proc)
            {
                this.proc = proc;
                for (int i = 0x0000; i <= 0xbfff; i++)
                {
                    if (RegisterHotKey(this.Handle, i, modKey, key) != 0)
                    {
                        id = i;
                        break;
                    }
                }
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                if (m.Msg == WM_HOTKEY)
                {
                    if ((int)m.WParam == id)
                    {
                        proc();
                    }
                }
            }

            protected override void Dispose(bool disposing)
            {
                UnregisterHotKey(this.Handle, id);
                base.Dispose(disposing);
            }

        }

        public enum ModKey : int
        {
            ALT = 0x01,
            CTRL = 0x02,
            SHIFT = 0x04,
        }
    }
}
