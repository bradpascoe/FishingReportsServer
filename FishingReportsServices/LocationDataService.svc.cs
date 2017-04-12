using System;
using System.Collections.Generic;

using FishingReports.DataAccess;
using FishingReports.Model;

namespace FishingReportsServices
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LocationDataService" in code, svc and config file together.
	public class LocationDataService : ILocationDataService
	{
		#region Constructors

		public LocationDataService()
		{
			mDataAccess = DALFactory.CreateLocationDAL();
		}

		#endregion Constructors

		#region LocationDataService Members

		public IEnumerable<AccessEntity> GetAccesses(Guid locationId)
		{
			return mDataAccess.QueryAccesses(locationId);
		}

		public IEnumerable<LocationEntity> GetAllLocations()
		{
			return mDataAccess.QueryLocations();
		}

		public IEnumerable<LocationEntity> GetLocations(Guid stateId)
		{
			return mDataAccess.QueryLocations(stateId);
		}

		public IEnumerable<StateEntity> GetStates()
		{
			return mDataAccess.QueryStates();
		}

		public void SaveAccess(Guid locationId, string newAccess)
		{
			mDataAccess.CreateAccess( new AccessEntity
			{
				LocationID = locationId,
				Access = newAccess,
				AccessID = Guid.NewGuid()
			} );
		}

		public void SaveLocation(Guid stateId, string newLocation)
		{
			mDataAccess.CreateLocation( new LocationEntity
			{
				LocationID = Guid.NewGuid(),
				StateID = stateId,
				Location = newLocation
			} );
		}

		public void SaveState(string newState)
		{
			mDataAccess.CreateState(
				new StateEntity
				{
					StateID = Guid.NewGuid(),
					State = newState
				} );
		}

		public IDictionary<string, string> ValidateNewAccess(AccessEntity access)
		{
			return mDataAccess.ValidateNewAccess(access);
		}

		public IDictionary<string, string> ValidateNewLocation(LocationEntity location)
		{
			return mDataAccess.ValidateNewLocation(location);
		}

		public IDictionary<string, string> ValidateNewState(StateEntity entity)
		{
			return mDataAccess.ValidateNewState(entity);
		}

		#endregion LocationDataService Members

		#region Fields

		private readonly ILocationDAL mDataAccess;

		#endregion Fields

	}
}
