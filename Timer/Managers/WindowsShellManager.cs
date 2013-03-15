using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using NLog;

using Sprint.Exceptions;
using Sprint.Models;

namespace Sprint.Managers
{
    /// <summary>
    /// Класс для управления системным окружением.
    /// </summary>
    static class WindowsShellManager
    {
        #region Поля только для чтения
        
        /// <summary>
        /// Логгер.
        /// </summary>
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Импортированные из Dll функции

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

        #region Константы

        public const int WM_HOTKEY = 0x312;

        #endregion

        #region Методы

        /// <summary>
        /// Регистрация глобальной горячей кнопки к форме.
        /// </summary>
        /// <param name="f">Форма к которой будет превязана горячая кнопка.</param>
        /// <param name="key">Привязываемая горячая кнопка.</param>
        public static OperationResult RegisterHotKey(Form f, Keys key)
        {
            try
            {
                if (!RegisterHotKey(f.Handle, f.GetHashCode() + (int)key, 0, (int)key))
                {
                    var exception = new SprintSystemException("Не удалось зарегистрировать в системе горячую клавишу.",
                                                                "Sprint.Managers.WindowsShellManager.RegisterHotKey(Form f, Keys key)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return new OperationResult(false, exception.Message, exception);
                }

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintSystemException("Не удалось зарегистрировать в системе горячую клавишу.",
                                                        "Sprint.Managers.WindowsShellManager.RegisterHotKey(Form f, Keys key)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        /// <summary>
        /// Дерегистрация глобальной горячей кнопки от формы.
        /// </summary>
        /// <param name="f">Форма от которой будет отвязана горячая кнопка.</param>
        /// <param name="key">Отвязываемая горячая кнопка.</param>
        public static OperationResult UnregisterHotKey(Form f, Keys key)
        {
            try
            {
                if(!UnregisterHotKey(f.Handle, f.GetHashCode() + (int)key))
                {
                    var exception = new SprintSystemException("Не удалось снять регистрацию горячей клавиши в системе.",
                                                                "Sprint.Managers.WindowsShellManager.UnregisterHotKey(Form f, Keys key)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return new OperationResult(false, exception.Message, exception);
                }

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintSystemException("Не удалось получить список участников.",
                                                        "Sprint.Managers.WindowsShellManager.UnregisterHotKey(Form f, Keys key)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        #endregion
    }
}
