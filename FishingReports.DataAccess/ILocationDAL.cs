using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FishingReports.Model;

namespace FishingReports.DataAccess
{
    public interface ILocationDAL
    {
        IEnumerable<LocationEntity> QueryLocations();

        IEnumerable<LocationEntity> QueryLocations(Guid statiID);

        IEnumerable<StateEntity> QueryStates();

        StateEntity QueryStates(string state);

        IEnumerable<AccessEntity> QueryAccesses(Guid locationID);

        void CreateState(StateEntity state);

        void CreateLocation(LocationEntity location);

        LocationEntity QueryLocations(string location, Guid stateID);

        void CreateAccess(AccessEntity access);

        AccessEntity QueryAccesses(string access, Guid locationID);

        IDictionary<string, string> ValidateNewState(StateEntity state);

        IDictionary<string, string> ValidateNewLocation(LocationEntity state);

        IDictionary<string, string> ValidateNewAccess(AccessEntity state);
    }
}
