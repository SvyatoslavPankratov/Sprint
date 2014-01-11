using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint.Models
{
	public class AppOptionsModel
	{
		#region Properties

		/// <summary>
		/// Задать или получить опции гонок по классам автомобилей.
		/// </summary>
		public List<RaceOptionsModel> RaceOptions { get; set; }
		
		/// <summary>
		/// Задать или получить время задержки которая активизируется 
		/// после защитаной отсечки, но после которой в течении заданного 
		/// времени все следующие отсечки игнорируются. Этот параметр нужен 
		/// для того, чтобы решить проблему с ложными отсечками, происходящими 
		/// сразу после корректной отсечки.
		/// </summary>
		public double DelayTime { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor.
		/// </summary>
		public AppOptionsModel ()
		{

		}

		#endregion

		#region Methods



		#endregion
	}
}
