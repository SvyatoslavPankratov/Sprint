using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Sprint.Interfaces;

namespace Sprint.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SprintWcfService" in both code and config file together.
    public class SprintWcfService : ISprintWcfService
    {
        public void DoWork()
        {
        }

        public void GetRacerListWithResults(Models.CarClassesEnum car_class)
        {
            throw new NotImplementedException();
        }
    }
}
