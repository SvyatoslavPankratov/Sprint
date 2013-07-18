using System.Windows.Forms;

using Sprint.Interfaces;
using Sprint.Models;
using Sprint.Presenters;

namespace Sprint.Views
{
    public partial class RegenerationDialogView : Form, IRegenerationDialog
    {
        #region Реализация интерфейса IRegenerationDialogView

        /// <summary>
        /// Получить тип восстановления состояния приложения.
        /// </summary>
        public AppRegenerationTypesEnum SelectedAppRegenerationType
        {
            get 
            {
                if (all_rb.Checked)
                {
                    return AppRegenerationTypesEnum.AllLapReRun;
                }

                if (null_rb.Checked)
                {
                    return AppRegenerationTypesEnum.LastLapReRun;
                }

                return AppRegenerationTypesEnum.LoadData;
            }
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Задать или получить презентер окна восстановления состояния приложения.
        /// </summary>
        private RegenerationDialogPresenter RegenerationDialogPresenter { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public RegenerationDialogView()
        {
            InitializeComponent();

            RegenerationDialogPresenter = new RegenerationDialogPresenter(this);
        } 

        #endregion

        #region Методы

        /// <summary>
        /// Действия при нажатии пользователем на кнопку восстановления состояния приложения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
