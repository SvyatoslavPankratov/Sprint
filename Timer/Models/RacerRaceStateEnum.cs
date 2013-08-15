using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Models
{
    /// <summary>
    /// Перечисление описывающее состояние заезда у участника.
    /// </summary>
    public enum RacerRaceStateEnum
    {
        /// <summary>
        /// Гонщик в процессе участия в гонках.
        /// Все проходит в штатном режиме.
        /// </summary>
        Run,

        /// <summary>
        /// Гонщик финишировал (Пока не используется).
        /// </summary>
        Finished,

        /// <summary>
        /// Гонщик перезаезжает текущий круг вместе с незакрытым заездом.
        /// </summary>
        Rerun,

        /// <summary>
        /// Гонщик перезаезжает текущий заезд с нуля.
        /// </summary>
        Restart,

        /// <summary>
        /// Гонщик снимается с участия из-за поломки.
        /// </summary>
        Break,

        /// <summary>
        /// Участник  (Пока не используется).
        /// </summary>
        Disqualification
    }
}
