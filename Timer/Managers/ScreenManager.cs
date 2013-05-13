using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sprint.Managers
{
    /// <summary>
    /// Менеджер по работе с экранами (мониторами).
    /// </summary>
    public class ScreenManager
    {
        #region Свойства

        /// <summary>
        /// Получить количество мониторов, подключенных к компьютеру.
        /// </summary>
        public int MonitorCount { get; private set; }

        /// <summary>
        /// Получить координаты верхнего левого угла мониторов 
        /// (первый монитор в списке является основным в системе).
        /// </summary>
        public List<Point> ScreenPoints { get; private set; }

        /// <summary>
        /// Получить список с параметрами подключенных к ПК экранов.
        /// </summary>
        public List<Screen> Screens { get; private set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public ScreenManager()
        {
            MonitorCount = System.Windows.Forms.Screen.AllScreens.Count();

            ScreenPoints = new List<Point>(MonitorCount);
            Screens = new List<Screen>(MonitorCount);

            var i = 0;

            ScreenPoints.Add(new Point
                                    {
                                        X = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.X,
                                        Y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Y
                                    });
            Screens.Add(System.Windows.Forms.Screen.PrimaryScreen);
            i++;

            foreach (var screen in System.Windows.Forms.Screen.AllScreens)
            {
                if (screen.GetHashCode() != System.Windows.Forms.Screen.PrimaryScreen.GetHashCode())
                {
                    ScreenPoints.Add(new Point
                                            {
                                                X = screen.WorkingArea.X,
                                                Y = screen.WorkingArea.Y
                                            });
                    Screens.Add(screen);
                    i++;
                }
            }
        }

        #endregion
    }
}
