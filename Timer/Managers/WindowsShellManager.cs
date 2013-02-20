using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Sprint.Managers
{
    class WindowsShellManager
    {
        #region Dll Imports

        /// <summary>
        /// Регистрация горячей кнопки.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="id"></param>
        /// <param name="fsModifiers"></param>
        /// <param name="vlc"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        /// <summary>
        /// Дерегистрация горячей кнопки.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        #endregion

        #region Constants

        public const int WM_HOTKEY = 0x312;

        #endregion

        #region Methods

        /// <summary>
        /// Регистрация глобальной горячей кнопки к форме.
        /// </summary>
        /// <param name="f">Форма к которой будет превязана горячая кнопка.</param>
        /// <param name="key">Привязываемая горячая кнопка.</param>
        public static void RegisterHotKey(Form f, Keys key)
        {
            try
            {
                RegisterHotKey((IntPtr)f.Handle, f.GetHashCode(), 0, (int)key);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Дерегистрация глобальной горячей кнопки от формы.
        /// </summary>
        /// <param name="f">Форма от которой будет отвязана горячая кнопка.</param>
        /// <param name="key">Отвязываемая горячая кнопка.</param>
        public static void UnregisterHotKey(Form f, Keys key)
        {
            try
            {
                UnregisterHotKey(f.Handle, (int)key);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion
    }
}
