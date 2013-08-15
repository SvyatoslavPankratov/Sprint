using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Models
{
    /// <summary>
    /// Состояние готовности следующего участника.
    /// </summary>
    public enum NextRacerState
    {
        /// <summary>
        /// Участник ждет команды выезда на трек.
        /// </summary>
        Stop,

        /// <summary>
        /// Участник может выезжать на трек.
        /// </summary>
        Start
    }
}
