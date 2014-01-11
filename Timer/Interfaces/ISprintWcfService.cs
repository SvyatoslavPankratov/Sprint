using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Sprint.Models;

namespace Sprint.Interfaces
{
    /// <summary>
    /// WCF service for get 
    /// </summary>
    [ServiceContract]
    public interface ISprintWcfService
    {
        [OperationContract]
        void GetRacerListWithResults(CarClassesEnum car_class);
    }
}
