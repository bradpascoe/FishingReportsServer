using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FishingReports.Model;

namespace FishingReports.DataAccess
{
    public class LocationDAL : ILocationDAL
    {
        internal LocationDAL()
            : this(new ConnectionStringProvider())
        {
        }

        internal LocationDAL(ConnectionStringProvider provider)
        {
            mProvider = provider;
        }

        private ConnectionStringProvider mProvider;

        public LocationEntity QueryLocations(string location, Guid stateID)
        {
            LocationEntity locationEntity = null;

            using (FishingReportsDBDataContext context = new FishingReportsDBDataContext(
                mProvider.GetConnectionString()))
            {
                var loc = (from locationTable in context.Locations
                                where locationTable.Description == location && locationTable.StateID == stateID
                                select locationTable).SingleOrDefault();

                if (loc != null)
                {
                    locationEntity = _CreateLocation(loc);
                }
            }

            return locationEntity;
        }

        public AccessEntity QueryAccesses(string access, Guid locationID)
        {
            AccessEntity accessEntity = null;

            using (FishingReportsDBDataContext context = new FishingReportsDBDataContext(
                mProvider.GetConnectionString()))
            {
                var acc = (from accessTable in context.Accesses
                           where accessTable.Description == access && 
                                accessTable.LocationID == locationID
                           select accessTable).SingleOrDefault();

                if (acc != null)
                {
                    accessEntity = new AccessEntity()
                    {
                        Access = acc.Description,
                        AccessID = acc.AccessID,
                        LocationID = acc.LocationID
                    };
                }
            }

            return accessEntity;
        }

        public IEnumerable<LocationEntity> QueryLocations()
        {
            List<LocationEntity> entities = new List<LocationEntity>();

            using (FishingReportsDBDataContext context = new FishingReportsDBDataContext(
                mProvider.GetConnectionString()))
            {
                var locations = from locationTable in context.Locations
                                orderby locationTable.Description ascending
                                select locationTable;

                foreach (Location loc in locations)
                {
                    entities.Add(_CreateLocation(loc));
                }
            }

            return entities.ToArray();
        }

        public IEnumerable<StateEntity> QueryStates()
        {
            List<StateEntity> entities = new List<StateEntity>();

            using (FishingReportsDBDataContext context = new FishingReportsDBDataContext(
                mProvider.GetConnectionString()))
            {
                var states = from stateTable in context.States
                                orderby stateTable.State1 ascending
                             select stateTable;

                foreach (State state in states)
                {
                    entities.Add(new StateEntity
                    {
                        State = state.State1,
                        StateID = state.StateID
                    });
                }
            }

            return entities.ToArray();
        }

        public IEnumerable<LocationEntity> QueryLocations(Guid stateID)
        {
            List<LocationEntity> entities = new List<LocationEntity>();

            using (FishingReportsDBDataContext context = new FishingReportsDBDataContext(
                mProvider.GetConnectionString()))
            {
                var locations = from locationTable in context.Locations
                                where locationTable.StateID == stateID
                                orderby locationTable.Description ascending
                                select locationTable;

                foreach (Location loc in locations)
                {
                    entities.Add(_CreateLocation(loc));
                }
            }

            return entities.ToArray();
        }

        public IEnumerable<AccessEntity> QueryAccesses(Guid locationID)
        {
            List<AccessEntity> entities = new List<AccessEntity>();

            using (FishingReportsDBDataContext context = new FishingReportsDBDataContext(
                mProvider.GetConnectionString()))
            {
                var accesses = from accessTable in context.Accesses
                               where accessTable.LocationID == locationID
                               orderby accessTable.Description ascending
                               select accessTable;

                foreach (Access access in accesses)
                {
                    entities.Add(new AccessEntity
                    {
                        Access = access.Description,
                        AccessID = access.AccessID,
                        LocationID = access.LocationID
                    });
                }
            }

            return entities.ToArray();
        }

        public void CreateState(StateEntity entity)
        {
	        using( FishingReportsDBDataContext context = new FishingReportsDBDataContext(
		        mProvider.GetConnectionString() ) )
	        {
		        if( !context.States.Any( state => state.State1 == entity.State ) )
		        {
			        State state = new State();
			        state.State1 = entity.State;
			        state.StateID = entity.StateID;

			        context.States.InsertOnSubmit( state );
			        context.SubmitChanges();
		        }
	        }
        }

        public void CreateLocation(LocationEntity entity)
        {
	        using( FishingReportsDBDataContext context = new FishingReportsDBDataContext(
		        mProvider.GetConnectionString() ) )
	        {
		        if( !context.Locations.Any( loc => loc.StateID == entity.StateID && loc.Description == entity.Location ) )
		        {
			        Location location = new Location();
			        location.Description = entity.Location;
			        location.LocationID = entity.LocationID;
			        location.StateID = entity.StateID;

			        context.Locations.InsertOnSubmit( location );
			        context.SubmitChanges();
		        }
	        }
        }

        public void CreateAccess(AccessEntity entity)
        {
	        using( FishingReportsDBDataContext context = new FishingReportsDBDataContext(
		        mProvider.GetConnectionString() ) )
	        {
		        if( !context.Accesses.Any( access => access.LocationID == entity.LocationID && access.Description == entity.Access ) )
		        {
			        Access access = new Access();
			        access.Description = entity.Access;
			        access.LocationID = entity.LocationID;
			        access.AccessID = entity.AccessID;

			        context.Accesses.InsertOnSubmit( access );
			        context.SubmitChanges();
		        }
	        }
        }

        public IDictionary<string, string> ValidateNewState(StateEntity state)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            if (state.State.Length != 2)
            {
                errors.Add("State", "State must be 2 characters long.");
            }

            if (errors.Count == 0 &&
                QueryStates(state.State) != null)
            {
                errors.Add("State", "State already exists.");
            }

            return errors;
        }

        public IDictionary<string, string> ValidateNewLocation(LocationEntity location)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(location.Location))
            {
                errors.Add("Location", "Location was not provided.");
            }

            if (QueryLocations(location.Location, location.StateID) != null)
            {
                errors.Add("Location", "Location already exists for this state.");
            }

            return errors;
        }

        public IDictionary<string, string> ValidateNewAccess(AccessEntity access)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(access.Access))
            {
                errors.Add("Access", "Access was not provided.");
            }

            if (QueryAccesses(access.Access, access.LocationID) != null)
            {
                errors.Add("Access", "Access already exists for this location.");
            }

            return errors;
        }

        public StateEntity QueryStates(string state)
        {
            StateEntity stateEntity = null;

            using (FishingReportsDBDataContext context = new FishingReportsDBDataContext(
               mProvider.GetConnectionString()))
            {
                var stateDB = (from states in context.States
                               where states.State1 == state
                               select states).FirstOrDefault();

                if (stateDB != null)
                {
                    stateEntity = new StateEntity
                    {
                        State = stateDB.State1,
                        StateID = stateDB.StateID
                    };
                }
            }

            return stateEntity;
        }

        private LocationEntity _CreateLocation(Location location)
        {
            return new LocationEntity
                    {
                        Location = location.Description,
                        LocationID = location.LocationID,
                        StateID = location.StateID
                    };
        }
    }
}