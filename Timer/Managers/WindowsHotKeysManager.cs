using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using NLog;

using Sprint.Exceptions;
using Sprint.Models;

namespace Sprint.Managers
{
    /// <summary>
    /// Класс для управления системными горячими кнопками.
    /// </summary>
    class WindowsHotKeysManager
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

        public const int WM_HOTKEY = 0x0312;

        #endregion

        #region Свойства

        /// <summary>
        /// Задать или получить форму, в которую направяется назначенная глобальная горячая клавиша.
        /// </summary>
        private Form Form { get; set; }

        /// <summary>
        /// Задать или получить глобальную горячую клавишу.
        /// </summary>
        private Keys Key { get; set; }

        #endregion

        #region Конструкторы и деструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public WindowsHotKeysManager()
        {

        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="f">Форма, в которую направяется назначенная глобальная горячая клавиша.</param>
        /// <param name="k">Глобальная горячую клавишу.</param>
        public WindowsHotKeysManager(Form f, Keys k)
        {
            if (f == null)
            {
                var exception = new SprintSystemException("Не указана форма, в которую будет направляться назначенная глобальная горячая клавиша.",
                                                          "Sprint.Managers.WindowsHotKeysManager.WindowsHotKeysManager(Form f, Keys key)");
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }

            Form = f;
            Key = k;

            RegisterHotKey(Form, Key);
        }

        /// <summary>
        /// Деструктор.
        /// </summary>
        ~WindowsHotKeysManager()
        {
            UnregisterHotKey(Form, Key);
        }

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
                                                                "Sprint.Managers.WindowsHotKeysManager.RegisterHotKey(Form f, Keys key)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return new OperationResult(false, exception.Message, exception);
                }

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintSystemException("Не удалось зарегистрировать в системе горячую клавишу.",
                                                        "Sprint.Managers.WindowsHotKeysManager.RegisterHotKey(Form f, Keys key)", ex);
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
                                                                "Sprint.Managers.WindowsHotKeysManager.UnregisterHotKey(Form f, Keys key)");
                    logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                    return new OperationResult(false, exception.Message, exception);
                }

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintSystemException("Не удалось получить список участников.",
                                                        "Sprint.Managers.WindowsHotKeysManager.UnregisterHotKey(Form f, Keys key)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        #endregion
    }
}
