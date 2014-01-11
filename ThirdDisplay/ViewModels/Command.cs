using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ThirdDisplay.ViewModels
{
    public class Command : ICommand
    {
        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public Command(Action<object> action)
        {
            ExecuteDelegate = action;
        }

        #endregion


        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Predicate<object> CanExecuteDelegate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Action<object> ExecuteDelegate { get; set; }

        #endregion


        #region ICommand Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate != null)
            {
                return CanExecuteDelegate(parameter);
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (ExecuteDelegate != null)
            {
                ExecuteDelegate(parameter);
            }
        }

        #endregion
    }
}
