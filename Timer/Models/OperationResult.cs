using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Models
{
    public class OperationResult
    {
        #region Свойства

        /// <summary>
        /// Задать или получить результат выполнения операции (успешно или нет).
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// Задать или получить сообщение о деталях результата выполнения операции.
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Задать или получить искллючение, произошедшее в результате выполнения операции.
        /// </summary>
        public Exception Exception { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор.
        /// </summary>
        public OperationResult()
        {
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="result">Результат выполнения операции (успешно или нет).</param>
        public OperationResult(bool result)
        {
            Result = result;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="result">Результат выполнения операции (успешно или нет).</param>
        /// <param name="details">Сообщение о деталях результата выполнения операции.</param>
        public OperationResult(bool result, string details)
            : this(result)
        {
            Details = details;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="result">Результат выполнения операции (успешно или нет).</param>
        /// <param name="exception">Искллючение, произошедшее в результате выполнения операции.</param>
        public OperationResult(bool result, Exception exception)
            : this(result)
        {
            Exception = exception;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="result">Результат выполнения операции (успешно или нет).</param>
        /// <param name="details">Сообщение о деталях результата выполнения операции.</param>
        /// <param name="exception">Искллючение, произошедшее в результате выполнения операции.</param>
        public OperationResult(bool result, string details, Exception exception)
            : this(result, details)
        {
            Exception = exception;
        }

        #endregion
    }
}
